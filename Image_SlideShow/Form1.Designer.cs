namespace Image_SlideShow
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.durationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTaskbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shuffleClickToONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreanClickToONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysTopOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerLoop = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveImagesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusButton,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 275);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(332, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // statusButton
            // 
            this.statusButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.durationToolStripMenuItem,
            this.clearAllImagesToolStripMenuItem,
            this.saveImagesListToolStripMenuItem,
            this.showTaskbarToolStripMenuItem,
            this.shuffleClickToONToolStripMenuItem,
            this.fullScreanClickToONToolStripMenuItem,
            this.alwaysTopOnToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.statusButton.Image = ((System.Drawing.Image)(resources.GetObject("statusButton.Image")));
            this.statusButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statusButton.Name = "statusButton";
            this.statusButton.Size = new System.Drawing.Size(50, 20);
            this.statusButton.Text = "Option";
            // 
            // durationToolStripMenuItem
            // 
            this.durationToolStripMenuItem.Name = "durationToolStripMenuItem";
            this.durationToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.durationToolStripMenuItem.Text = "&Set Duration (3 Sec)";
            this.durationToolStripMenuItem.Click += new System.EventHandler(this.durationToolStripMenuItem_Click);
            // 
            // clearAllImagesToolStripMenuItem
            // 
            this.clearAllImagesToolStripMenuItem.Name = "clearAllImagesToolStripMenuItem";
            this.clearAllImagesToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.clearAllImagesToolStripMenuItem.Text = "&Clear All Images";
            this.clearAllImagesToolStripMenuItem.Click += new System.EventHandler(this.clearAllImagesToolStripMenuItem_Click);
            // 
            // showTaskbarToolStripMenuItem
            // 
            this.showTaskbarToolStripMenuItem.Name = "showTaskbarToolStripMenuItem";
            this.showTaskbarToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.showTaskbarToolStripMenuItem.Text = "Show Task&bar (Click to ON)";
            this.showTaskbarToolStripMenuItem.Click += new System.EventHandler(this.showTaskbarToolStripMenuItem_Click);
            // 
            // shuffleClickToONToolStripMenuItem
            // 
            this.shuffleClickToONToolStripMenuItem.Name = "shuffleClickToONToolStripMenuItem";
            this.shuffleClickToONToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.shuffleClickToONToolStripMenuItem.Text = "S&huffle (Click to ON)";
            this.shuffleClickToONToolStripMenuItem.Click += new System.EventHandler(this.shuffleClickToONToolStripMenuItem_Click);
            // 
            // fullScreanClickToONToolStripMenuItem
            // 
            this.fullScreanClickToONToolStripMenuItem.Name = "fullScreanClickToONToolStripMenuItem";
            this.fullScreanClickToONToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.fullScreanClickToONToolStripMenuItem.Text = "&Fullscreen (Click to ON)";
            this.fullScreanClickToONToolStripMenuItem.Click += new System.EventHandler(this.fullScreanClickToONToolStripMenuItem_Click);
            // 
            // alwaysTopOnToolStripMenuItem
            // 
            this.alwaysTopOnToolStripMenuItem.Name = "alwaysTopOnToolStripMenuItem";
            this.alwaysTopOnToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.alwaysTopOnToolStripMenuItem.Text = "Always &Top (Click to ON)";
            this.alwaysTopOnToolStripMenuItem.Click += new System.EventHandler(this.alwaysTopOnToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.aboutToolStripMenuItem.Text = "&About Image Slideshow";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(73, 17);
            this.statusLabel.Text = "statusLabel";
            // 
            // timerLoop
            // 
            this.timerLoop.Enabled = true;
            this.timerLoop.Interval = 10;
            this.timerLoop.Tick += new System.EventHandler(this.timerLoop_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.PapayaWhip;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(332, 297);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            // 
            // saveImagesListToolStripMenuItem
            // 
            this.saveImagesListToolStripMenuItem.Name = "saveImagesListToolStripMenuItem";
            this.saveImagesListToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.saveImagesListToolStripMenuItem.Text = "Save Image &List";
            this.saveImagesListToolStripMenuItem.Click += new System.EventHandler(this.saveImagesListToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(332, 297);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "App";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripDropDownButton statusButton;
        private System.Windows.Forms.ToolStripMenuItem durationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysTopOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullScreanClickToONToolStripMenuItem;
        private System.Windows.Forms.Timer timerLoop;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTaskbarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shuffleClickToONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImagesListToolStripMenuItem;
    }
}

