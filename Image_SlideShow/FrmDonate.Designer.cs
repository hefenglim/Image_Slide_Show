namespace PayPalDonate
{
    partial class FrmDonate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDonate));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtOther = new System.Windows.Forms.TextBox();
            this.RdOther = new System.Windows.Forms.RadioButton();
            this.RdFifty = new System.Windows.Forms.RadioButton();
            this.RdTwentyFive = new System.Windows.Forms.RadioButton();
            this.RdTwenty = new System.Windows.Forms.RadioButton();
            this.RdFifteen = new System.Windows.Forms.RadioButton();
            this.RdTen = new System.Windows.Forms.RadioButton();
            this.RdFive = new System.Windows.Forms.RadioButton();
            this.BtnDonate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtOther);
            this.groupBox1.Controls.Add(this.RdOther);
            this.groupBox1.Controls.Add(this.RdFifty);
            this.groupBox1.Controls.Add(this.RdTwentyFive);
            this.groupBox1.Controls.Add(this.RdTwenty);
            this.groupBox1.Controls.Add(this.RdFifteen);
            this.groupBox1.Controls.Add(this.RdTen);
            this.groupBox1.Controls.Add(this.RdFive);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 112);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Amount";
            // 
            // TxtOther
            // 
            this.TxtOther.Location = new System.Drawing.Point(80, 82);
            this.TxtOther.Name = "TxtOther";
            this.TxtOther.Size = new System.Drawing.Size(118, 22);
            this.TxtOther.TabIndex = 7;
            // 
            // RdOther
            // 
            this.RdOther.AutoSize = true;
            this.RdOther.Location = new System.Drawing.Point(16, 83);
            this.RdOther.Name = "RdOther";
            this.RdOther.Size = new System.Drawing.Size(49, 16);
            this.RdOther.TabIndex = 6;
            this.RdOther.TabStop = true;
            this.RdOther.Text = "Other";
            this.RdOther.UseVisualStyleBackColor = true;
            this.RdOther.Click += new System.EventHandler(this.RadioChecked);
            // 
            // RdFifty
            // 
            this.RdFifty.AutoSize = true;
            this.RdFifty.Location = new System.Drawing.Point(140, 62);
            this.RdFifty.Name = "RdFifty";
            this.RdFifty.Size = new System.Drawing.Size(50, 16);
            this.RdFifty.TabIndex = 5;
            this.RdFifty.TabStop = true;
            this.RdFifty.Text = "50.00";
            this.RdFifty.UseVisualStyleBackColor = true;
            this.RdFifty.Click += new System.EventHandler(this.RadioChecked);
            // 
            // RdTwentyFive
            // 
            this.RdTwentyFive.AutoSize = true;
            this.RdTwentyFive.Location = new System.Drawing.Point(140, 41);
            this.RdTwentyFive.Name = "RdTwentyFive";
            this.RdTwentyFive.Size = new System.Drawing.Size(50, 16);
            this.RdTwentyFive.TabIndex = 4;
            this.RdTwentyFive.TabStop = true;
            this.RdTwentyFive.Text = "25.00";
            this.RdTwentyFive.UseVisualStyleBackColor = true;
            this.RdTwentyFive.Click += new System.EventHandler(this.RadioChecked);
            // 
            // RdTwenty
            // 
            this.RdTwenty.AutoSize = true;
            this.RdTwenty.Location = new System.Drawing.Point(140, 19);
            this.RdTwenty.Name = "RdTwenty";
            this.RdTwenty.Size = new System.Drawing.Size(50, 16);
            this.RdTwenty.TabIndex = 3;
            this.RdTwenty.TabStop = true;
            this.RdTwenty.Text = "20.00";
            this.RdTwenty.UseVisualStyleBackColor = true;
            this.RdTwenty.Click += new System.EventHandler(this.RadioChecked);
            // 
            // RdFifteen
            // 
            this.RdFifteen.AutoSize = true;
            this.RdFifteen.Location = new System.Drawing.Point(16, 62);
            this.RdFifteen.Name = "RdFifteen";
            this.RdFifteen.Size = new System.Drawing.Size(50, 16);
            this.RdFifteen.TabIndex = 2;
            this.RdFifteen.TabStop = true;
            this.RdFifteen.Text = "15.00";
            this.RdFifteen.UseVisualStyleBackColor = true;
            this.RdFifteen.Click += new System.EventHandler(this.RadioChecked);
            // 
            // RdTen
            // 
            this.RdTen.AutoSize = true;
            this.RdTen.Location = new System.Drawing.Point(16, 41);
            this.RdTen.Name = "RdTen";
            this.RdTen.Size = new System.Drawing.Size(50, 16);
            this.RdTen.TabIndex = 1;
            this.RdTen.TabStop = true;
            this.RdTen.Text = "10.00";
            this.RdTen.UseVisualStyleBackColor = true;
            this.RdTen.Click += new System.EventHandler(this.RadioChecked);
            // 
            // RdFive
            // 
            this.RdFive.AutoSize = true;
            this.RdFive.Location = new System.Drawing.Point(16, 19);
            this.RdFive.Name = "RdFive";
            this.RdFive.Size = new System.Drawing.Size(44, 16);
            this.RdFive.TabIndex = 0;
            this.RdFive.TabStop = true;
            this.RdFive.Text = "5.00";
            this.RdFive.UseVisualStyleBackColor = true;
            this.RdFive.Click += new System.EventHandler(this.RadioChecked);
            // 
            // BtnDonate
            // 
            this.BtnDonate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDonate.Location = new System.Drawing.Point(366, 102);
            this.BtnDonate.Name = "BtnDonate";
            this.BtnDonate.Size = new System.Drawing.Size(90, 21);
            this.BtnDonate.TabIndex = 3;
            this.BtnDonate.Text = "Donate";
            this.BtnDonate.UseVisualStyleBackColor = true;
            this.BtnDonate.Click += new System.EventHandler(this.BtnDonate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Image_SlideShow.Properties.Resources.paypal;
            this.pictureBox1.Location = new System.Drawing.Point(235, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FrmDonate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 132);
            this.Controls.Add(this.BtnDonate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDonate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmDonate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RdOther;
        private System.Windows.Forms.RadioButton RdFifty;
        private System.Windows.Forms.RadioButton RdTwentyFive;
        private System.Windows.Forms.RadioButton RdTwenty;
        private System.Windows.Forms.RadioButton RdFifteen;
        private System.Windows.Forms.RadioButton RdTen;
        private System.Windows.Forms.RadioButton RdFive;
        private System.Windows.Forms.Button BtnDonate;
        private System.Windows.Forms.TextBox TxtOther;
    }
}