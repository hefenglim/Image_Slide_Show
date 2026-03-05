using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Reflection;

using PayPalDonate;
using IniRW;

namespace Image_SlideShow
{
    public partial class Form1 : Form
    {
        private IniFile ini;
        private List<String> fileList;
        private List<UInt64> fileHitSeq;    // random selected item to record current sequence number.
        private UInt64 RandSeq;             // random count sequence number.
        private UInt64 RandGap;             // gap value range for reduce hit same item again.
        private string[] inFiles;
        private int durationSec;
        private int loopInx;
        private bool dragMove;
        private bool haveDrag;
        private Point dragMouseOffset;
        private bool dragMouseOffsetUpdate;
        private int titleHeight;
        private int resizeMode;
        private bool shufflePlay;
        private bool betaRelease;
        Random rand;
        AboutSoft aboutForm;

        // a struct containing important information about the state to restore to
        struct clientRect
        {
            public Point location;
            public int width;
            public int height;
            public bool topMost;
        };
        // this should be in the scope your class
        private clientRect restore;
        private bool fullscreen;
        private bool fullscreenActive;
        private clientRect oriForm;

        public Form1()
        {
            InitializeComponent();
            betaRelease = false;

            if(betaRelease)
            {
                this.Text = Application.ProductName + " v" + Application.ProductVersion + " (BETA)";
            } else {
                this.Text = Application.ProductName + " v" + Application.ProductVersion;
            }

            Control.CheckForIllegalCrossThreadCalls = false;

            this.Height = 353;
            this.Width = 415;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image_SlideShow.Properties.Resources.initialBack;

            ini = new IniFile(Application.StartupPath + "\\config.ini");
            fileList = new List<string>();
            fileHitSeq = new List<UInt64>();
            ClearFileList();
            updateStatus();
            durationSec = 3;
            loopInx = 0;
            dragMove = false;
            haveDrag = false;
            dragMouseOffset = new Point();
            titleHeight = this.Height - this.ClientSize.Height;
            resizeMode = 0;
            shufflePlay = false;
            rand = new Random((Int32)DateTime.Now.Ticks);
            aboutForm = null;

            fullscreen = false;
            fullscreenActive = false;

            //Read config and apply it.
            String read = "";
            Int32 readInt = 0;
            read = ini.ReadValue("SETTING", "Duration");
            Int32.TryParse(read, out readInt);
            if (readInt > 0)
                setDuration(readInt);

            read = ini.ReadValue("SETTING", "AlwaysTop");
            Int32.TryParse(read, out readInt);
            if (readInt > 0)
                alwaysTopOnToolStripMenuItem_Click(null, null);

            read = ini.ReadValue("SETTING", "FullScreen");
            Int32.TryParse(read, out readInt);
            if (readInt > 0)
                fullScreanClickToONToolStripMenuItem_Click(null, null);

            read = ini.ReadValue("SETTING", "ShowTaskbar");
            Int32.TryParse(read, out readInt);
            if (readInt > 0)
                showTaskbarToolStripMenuItem_Click(null, null);

            read = ini.ReadValue("SETTING", "Shuffle");
            Int32.TryParse(read, out readInt);
            if (readInt > 0)
                shuffleClickToONToolStripMenuItem_Click(null, null);

        }

        /// <summary>
        /// Makes the form either fullscreen, or restores it to it's original size/location
        /// </summary>
        void Fullscreen()
        {
            if (fullscreen)
            {
                fullscreenActive = true;
                this.restore.location = this.Location;
                this.restore.width = this.Width;
                this.restore.height = this.Height;
                this.restore.topMost = this.TopMost;
                this.TopMost = true;
                this.Location = new Point(0, 0);
                this.FormBorderStyle = FormBorderStyle.None;
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Height = Screen.PrimaryScreen.Bounds.Height;
            }
            else
            {
                this.TopMost = this.restore.topMost;
                this.Location = this.restore.location;
                this.Width = this.restore.width;
                this.Height = this.restore.height;
                // these are the two variables you may wish to change, depending
                // on the design of your form (WindowState and FormBorderStyle)
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                fullscreenActive = false;
            }
        }

        /// <summary>
        /// Clear file list & re-initial random sequence no
        /// </summary>
        private void ClearFileList()
        {
            fileList.Clear();
            fileHitSeq.Clear();
            RandSeq = 0x1;
        }

        /// <summary>
        /// Returns true if the given file path is a folder.
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>True if a folder</returns>
        public Boolean IsFolder(String path)
        {
            return ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory);
        }

        private void updateStatus()
        {
            statusLabel.Text = fileList.Count + " Images.";
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            inFiles = (String[])e.Data.GetData(DataFormats.FileDrop);

            Thread thread = new Thread(fetchFileList);
            thread.Start();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            // make sure they're actually dropping files (not text or anything else)
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                // allow them to continue
                // (without this, the cursor stays a "NO" symbol
                e.Effect = DragDropEffects.All;
        }

        private void fetchFileList()
        {
            long fc = 0;
            long count = inFiles.LongLength;

            //ClearFileList();
            for (int i = 0; i < count; i++)
            {
                if (IsFolder(inFiles[i]))
                {
                    try
                    {
                        String[] allPath = Directory.GetFiles(inFiles[i], "*.*", SearchOption.AllDirectories);
                        foreach (String path in allPath)
                        {
                            switch (Path.GetExtension(path).ToLower())
                            {
                                case ".ini":
                                    fetchIniFileList(path);
                                    break;
                                case ".jpg":
                                case ".jpeg":
                                case ".gif":
                                case ".bmp":
                                case ".png":
                                case ".tif":
                                    if (fileList.IndexOf(path) == -1)
                                    {
                                        fileList.Add(path);
                                        fileHitSeq.Add(0);
                                    }
                                    fc++;
                                    if (fc % 50 == 0 || fc < 500)
                                    {
                                        updateStatus();
                                    }
                                    break;
                                default:
                                    break;
                            };
                        }
                    }
                    catch
                    { }
                }
                else
                {
                    switch (Path.GetExtension(inFiles[i]).ToLower())
                    {
                        case ".ini":
                            fetchIniFileList(inFiles[i]);
                            break;
                        case ".jpg":
                        case ".jpeg":
                        case ".gif":
                        case ".bmp":
                        case ".png":
                        case ".tif":
                            if (fileList.IndexOf(inFiles[i]) == -1)
                            {
                                fileList.Add(inFiles[i]);
                                fileHitSeq.Add(0);
                            }
                            fc++;
                            if (fc % 50 == 0 || fc < 500)
                            {
                                updateStatus();
                            }
                            break;
                        default:
                            break;
                    };
                }
            }

            updateStatus();
            statusLabel.Text += " (DONE)";

            if (fileList.Count == 0)
            {
                pictureBox1.ImageLocation = "";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image_SlideShow.Properties.Resources.initialBack;
            }
            else
            {
                RandGap = (ulong)(fileList.Count * 0.8);
            }
        }

        private void fetchIniFileList(String path)
        {
            IniFile file = new IniFile(path);
            String read = file.ReadValue("IMAGE_SLIDESHOW_LIST", "Count");
            int cnt = 0;
            int.TryParse(read, out cnt);

            if (cnt > 0)
            {
                for (int i = 0; i < cnt; i++)
                {
                    String fp = file.ReadValue("PATH", i.ToString());
                    switch (Path.GetExtension(fp).ToLower())
                    {
                        case ".jpg":
                        case ".jpeg":
                        case ".gif":
                        case ".bmp":
                        case ".png":
                        case ".tif":
                            if (fileList.IndexOf(fp) == -1)
                            {
                                fileList.Add(fp);
                                fileHitSeq.Add(0);
                            }
                            if (i % 50 == 0 || i < 500)
                            {
                                updateStatus();
                            }
                            break;
                        default:
                            break;
                    };
                }
            }

            file = null;
        }

        private void alwaysTopOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TopMost)
            {
                this.TopMost = false;
                alwaysTopOnToolStripMenuItem.Text = "Always &Top (Click to ON)";
                ini.WriteValue("SETTING", "AlwaysTop", "0");
            }
            else
            {
                this.TopMost = true;
                alwaysTopOnToolStripMenuItem.Text = "Always &Top (Click to OFF)";
                ini.WriteValue("SETTING", "AlwaysTop", "1");
            }
        }

        private void fullScreanClickToONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fullscreen)
            {
                fullscreen = false;
                fullScreanClickToONToolStripMenuItem.Text = "&Fullscreen (Click to ON)";
                ini.WriteValue("SETTING", "FullScreen", "0");
            }
            else
            {
                fullscreen = true;
                fullScreanClickToONToolStripMenuItem.Text = "&Fullscreen (Click to OFF)";
                ini.WriteValue("SETTING", "FullScreen", "1");
            }
        }


        private void showTaskbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ShowInTaskbar)
            {
                this.ShowInTaskbar = false;
                showTaskbarToolStripMenuItem.Text = "Show Task&bar (Click to ON)";
                ini.WriteValue("SETTING", "ShowTaskbar", "0");
            }
            else
            {
                this.ShowInTaskbar = true;
                showTaskbarToolStripMenuItem.Text = "Show Task&bar (Click to OFF)";
                ini.WriteValue("SETTING", "ShowTaskbar", "1");
            }
        }


        private void shuffleClickToONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shufflePlay)
            {
                shufflePlay = false;
                shuffleClickToONToolStripMenuItem.Text = "S&huffle (Click to ON)";
                ini.WriteValue("SETTING", "Shuffle", "0");
            }
            else
            {
                shufflePlay = true;
                shuffleClickToONToolStripMenuItem.Text = "S&huffle (Click to OFF)";
                ini.WriteValue("SETTING", "Shuffle", "1");
            }
        }

        private void clearAllImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image_SlideShow.Properties.Resources.initialBack;
            ClearFileList();
            updateStatus();
        }
        
        public void setDuration(int sec)
        {
            durationSec = sec;
            durationToolStripMenuItem.Text = "Set Duration (" + durationSec + " Sec)";
            ini.WriteValue("SETTING", "Duration", durationSec.ToString());
        }

        private void durationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDuration form = new SetDuration(this);
            form.Show(durationSec);
        }

        private int randomPickInx()
        {
            ulong limitRand = (RandGap * 2);
            int inx = (fileList.Count + 1);

            if (limitRand > 1000) { limitRand = 1000; }
            bool reGen = false;

            do
            {
                while (inx >= fileList.Count)
                {
                    inx = rand.Next(0, fileList.Count);
                }

                if (fileHitSeq[inx] != 0)
                {
                    // check is less then seq gap. 
                    if ((RandSeq - fileHitSeq[inx]) < RandGap)
                    {
                        if (--limitRand <= 0)
                        {
                            // over limit random, we clear the weight sequence.
                            for (int i = 0; i < fileHitSeq.Count; i++)
                            {
                                fileHitSeq[i] = 0;
                            }
                            RandSeq = 1;
                        }

                        reGen = true;

                        // re-random again.
                        inx = (fileList.Count + 1);
                    }
                    else
                    {
                        // random gap over, re-selected again.
                        fileHitSeq[inx] = RandSeq++;

                        if (reGen)
                        {
                            reGen = false;
                        }
                    }
                }
                else
                {
                    // first time selected.
                    fileHitSeq[inx] = RandSeq++;

                    if (reGen)
                    {
                        reGen = false;
                    }
                }
            } while (inx == (fileList.Count + 1));

            return inx;
        }

        private void timerLoop_Tick(object sender, EventArgs e)
        {
            timerLoop.Interval = durationSec * 1000;

            if (fileList.Count == 0)
            {
                pictureBox1.ImageLocation = "";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image_SlideShow.Properties.Resources.initialBack;
                return;
            }

            Int32 read = 0;
            Int32.TryParse(ini.ReadValue("LINK", "Pause"), out read);
            if (read > 0)
            {
                //Detect pause mode active.
                pictureBox1.BorderStyle = BorderStyle.Fixed3D;
                timerLoop.Interval = 500;
                return;
            }
            else
            {
                //Detect play mode.
                pictureBox1.BorderStyle = BorderStyle.None;
                timerLoop.Interval = durationSec * 1000;
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            if (shufflePlay)
            {
                loopInx = randomPickInx();
                pictureBox1.ImageLocation = fileList[loopInx];
            }
            else
            {
                loopInx++;
                if (loopInx >= fileList.Count)
                    loopInx = 0;
                fileHitSeq[loopInx] = RandSeq++; // record played sequence when not random case.
                pictureBox1.ImageLocation = fileList[loopInx];
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Cursor == Cursors.Default)
            {
                dragMove = true;
                dragMouseOffsetUpdate = true;
            }
            else
            {
                haveDrag = true;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dragMove = false;
            haveDrag = false;
            resizeMode = 0;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            const int DETECT = 8;
            
            if (haveDrag)
            {
                /* Drag active && detect resize mode */
                if (resizeMode == 10)   //Right-down corner.
                {
                    Point form_pt = this.Location;
                    Point mouse_pt = Cursor.Position;
                    Size size = new Size();
                    size.Height = mouse_pt.Y - form_pt.Y;
                    size.Width = mouse_pt.X - form_pt.X;
                    this.Size = size;
                    oriForm.height = size.Height;
                    oriForm.width = size.Width;
                    return;
                }
                if (resizeMode == 1)    //Left-up corner.
                {
                    Point form_pt = this.Location;
                    Point mouse_pt = Cursor.Position;
                    Size size = this.Size;
                    size.Width += form_pt.X - mouse_pt.X;
                    size.Height += form_pt.Y - mouse_pt.Y;
                    this.Location = mouse_pt;
                    this.Size = size;
                    oriForm.height = size.Height;
                    oriForm.width = size.Width;
                    return;
                }
                if (resizeMode == 20)   //Left-down corner.
                {
                    Point form_pt = this.Location;
                    Point mouse_pt = Cursor.Position;
                    Size size = this.Size;
                    size.Width += form_pt.X - mouse_pt.X;
                    size.Height = mouse_pt.Y - form_pt.Y;
                    mouse_pt.Y = form_pt.Y;
                    this.Location = mouse_pt;
                    this.Size = size;
                    oriForm.height = size.Height;
                    oriForm.width = size.Width;
                    return;
                }
                if (resizeMode == 2)    //Right-up corner.
                {
                    Point form_pt = this.Location;
                    Point mouse_pt = Cursor.Position;
                    Size size = this.Size;
                    size.Width = mouse_pt.X - form_pt.X;
                    size.Height += form_pt.Y - mouse_pt.Y;
                    mouse_pt.X = form_pt.X;
                    this.Location = mouse_pt;
                    this.Size = size;
                    oriForm.height = size.Height;
                    oriForm.width = size.Width;
                    return;
                }
                if (resizeMode == 30)   //Right.
                {
                    Point form_pt = this.Location;
                    Point mouse_pt = Cursor.Position;
                    Size size = new Size();
                    size.Height = this.Height;
                    size.Width = mouse_pt.X - form_pt.X;
                    this.Size = size;
                    oriForm.height = size.Height;
                    oriForm.width = size.Width;
                    return;
                }
                if (resizeMode == 3)    //Left.
                {
                    Point form_pt = this.Location;
                    Point mouse_pt = Cursor.Position;
                    Size size = this.Size;
                    size.Width += form_pt.X - mouse_pt.X;
                    mouse_pt.Y = form_pt.Y;
                    this.Location = mouse_pt;
                    this.Size = size;
                    oriForm.height = size.Height;
                    oriForm.width = size.Width;
                    return;
                }
                if (resizeMode == 40)   //Down.
                {
                    Point form_pt = this.Location;
                    Point mouse_pt = Cursor.Position;
                    Size size = new Size();
                    size.Height = mouse_pt.Y - form_pt.Y;
                    size.Width = this.Width;
                    this.Size = size;
                    oriForm.height = size.Height;
                    oriForm.width = size.Width;
                    return;
                }
                if (resizeMode == 4)    //Top.
                {
                    Point form_pt = this.Location;
                    Point mouse_pt = Cursor.Position;
                    Size size = this.Size;
                    size.Height += form_pt.Y - mouse_pt.Y;
                    mouse_pt.X = form_pt.X;
                    this.Location = mouse_pt;
                    this.Size = size;
                    oriForm.height = size.Height;
                    oriForm.width = size.Width;
                    return;
                }
            }

            resizeMode = 0;

            if ((e.X <= DETECT && e.Y <= DETECT) || (e.X + DETECT >= this.Width && e.Y + DETECT >= this.Height))
            {
                if (this.FormBorderStyle == FormBorderStyle.None)
                {
                    this.Cursor = Cursors.SizeNWSE;
                    if ((e.X <= DETECT && e.Y <= DETECT))
                        resizeMode = 1;     //Left-up corner.
                    else
                        resizeMode = 10;    //Right-down corner.
                }
            }
            else if ((e.X + DETECT >= this.Width && e.Y <= DETECT) || (e.X <= DETECT && e.Y + DETECT >= this.Height))
            {
                if (this.FormBorderStyle == FormBorderStyle.None)
                {
                    this.Cursor = Cursors.SizeNESW;
                    if ((e.X + DETECT >= this.Width && e.Y <= DETECT))
                        resizeMode = 2;     //Right-up corner.
                    else
                        resizeMode = 20;    //Left-down corner.
                }
            }
            else if (e.X <= DETECT || e.X + DETECT >= this.Width)
            {
                if (this.FormBorderStyle == FormBorderStyle.None)
                {
                    this.Cursor = Cursors.SizeWE;
                    if (e.X <= DETECT)
                        resizeMode = 3;     //Left.
                    else
                        resizeMode = 30;    //Right.
                }
            }
            else if (e.Y <= DETECT || e.Y + DETECT >= this.Height)
            {
                if (this.FormBorderStyle == FormBorderStyle.None)
                {
                    this.Cursor = Cursors.SizeNS;
                    if (e.Y <= DETECT)
                        resizeMode = 4;     //Top.
                    else
                        resizeMode = 40;    //Down.
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
                if (dragMove && fullscreenActive == false)
                {
                    Point pt = Cursor.Position;
                    if (dragMouseOffsetUpdate)
                    {
                        dragMouseOffset.X = e.X;
                        dragMouseOffset.Y = e.Y;

                        if (this.FormBorderStyle == FormBorderStyle.Sizable)
                        {
                            dragMouseOffset.Y += (titleHeight - SystemInformation.Border3DSize.Height);
                            dragMouseOffset.X += SystemInformation.Border3DSize.Width;
                        }

                        dragMouseOffsetUpdate = false;
                    }
                    pt.X -= dragMouseOffset.X;
                    pt.Y -= dragMouseOffset.Y;

                    this.Location = pt;
                    haveDrag = true;
                }
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            oriForm.height = this.Height;
            oriForm.width = this.Width;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (fileList.Count == 0 || haveDrag == true)
                return;

            if (statusStrip1.Visible == true)
            {
                if (fullscreen == true)
                {
                    Fullscreen();
                }
                oriForm.height = this.Height;
                oriForm.width = this.Width;
                statusStrip1.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.Black;
                pictureBox1.BackColor = Color.Black;
                this.Height = oriForm.height;
                this.Width = oriForm.width;
                //loopInx = 0;
                timerLoop.Interval = durationSec * 1000;
                timerLoop.Enabled = true;
                this.Focus();
            }
            else
            {
                //timerLoop.Enabled = false;    //Cancel auto non-stop loop, enable this code line.
                statusStrip1.Visible = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.BackColor = Color.PapayaWhip;
                pictureBox1.BackColor = Color.PapayaWhip;
                this.Height = oriForm.height;
                this.Width = oriForm.width;

                if (fullscreen == true)
                {
                    fullscreen = false;
                    Fullscreen();
                    fullscreen = true;
                }

            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (fileList.Count == 0)
                return;

            updateStatus();

            if (e.KeyChar == ' ')
            {
                if (shufflePlay)
                {
                    statusLabel.Text += " (Shuffle Off)";
                    shufflePlay = false;
                    shuffleClickToONToolStripMenuItem.Text = "S&huffle (Click to ON)";
                    ini.WriteValue("SETTING", "Shuffle", "0");
                }
                else
                {
                    statusLabel.Text += " (Shuffle On)";
                    shufflePlay = true;
                    shuffleClickToONToolStripMenuItem.Text = "S&huffle (Click to OFF)";
                    ini.WriteValue("SETTING", "Shuffle", "1");
                }
            }
            else
            {
                Int32 read = 0;
                Int32.TryParse(ini.ReadValue("LINK", "Pause"), out read);
                if (read == 0)
                    pictureBox1.BorderStyle = BorderStyle.None;
                else
                    pictureBox1.BorderStyle = BorderStyle.Fixed3D;

                if (pictureBox1.BorderStyle == BorderStyle.None)
                {
                    //Goes to pause mode.
                    statusLabel.Text += " (Pause)";

                    ini.WriteValue("LINK", "Pause", "1");
                    timerLoop.Interval = 1000;
                    pictureBox1.BorderStyle = BorderStyle.Fixed3D;
                    this.Focus();
                }
                else
                {
                    //Continue slideshow.
                    statusLabel.Text += " (Play)";

                    ini.WriteValue("LINK", "Pause", "0");
                    pictureBox1.BorderStyle = BorderStyle.None;
                    timerLoop.Interval = 1;
                    timerLoop.Enabled = true;
                    this.Focus();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aboutForm == null)
                aboutForm = new AboutSoft(this);

            aboutForm.Show();
            aboutForm.Focus();
        }

        public void clearAboutForm()
        {
            aboutForm.Dispose();
            aboutForm = null;
        }

        private void saveImagesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileList.Count <= 0)
                return;

            Thread thread = new Thread(saveImageList);
            thread.Priority = ThreadPriority.AboveNormal;
            thread.Start();
        }

        private void saveImageList()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.OverwritePrompt = true;
            saveDialog.Filter = "Slideshow Image List|*.ini";
            saveDialog.DefaultExt = "ini";
            saveDialog.AddExtension = true;
            saveDialog.RestoreDirectory = true;
            saveDialog.Title = "Save image list file.";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                statusLabel.Text = "Saving lists, please wait... ( 0% )";

                if (File.Exists(saveDialog.FileName))
                    File.Delete(saveDialog.FileName);

                IniFile file = new IniFile(saveDialog.FileName);
                file.WriteValue("IMAGE_SLIDESHOW_LIST", "Count", fileList.Count.ToString());

                for (int i = 0; i < fileList.Count; i++)
                {
                    file.WriteValue("PATH", i.ToString(), fileList[i]);

                    if (i < 500 || i % 100 == 0)
                    {
                        int percent = (int)(((float)i / (float)fileList.Count) * 100.00);
                        statusLabel.Text = "Saving lists, please wait... ( " + percent.ToString() + "% )";
                    }
                }

                statusLabel.Text = fileList.Count + " Images. (SAVED DONE)";
                file = null;
            }

            saveDialog.Dispose();
            saveDialog = null;

            Thread.CurrentThread.Abort();
            return;
        }



    }
}
