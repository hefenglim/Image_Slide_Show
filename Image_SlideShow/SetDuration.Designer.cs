namespace Image_SlideShow
{
    partial class SetDuration
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
            this.label1 = new System.Windows.Forms.Label();
            this.textSecond = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seconds.";
            // 
            // textSecond
            // 
            this.textSecond.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textSecond.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSecond.Location = new System.Drawing.Point(5, 5);
            this.textSecond.Name = "textSecond";
            this.textSecond.Size = new System.Drawing.Size(43, 15);
            this.textSecond.TabIndex = 1;
            this.textSecond.Text = "3";
            this.textSecond.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textSecond_KeyUp);
            this.textSecond.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textSecond_KeyPress);
            // 
            // SetDuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(101, 27);
            this.Controls.Add(this.textSecond);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SetDuration";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Set Duration";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SetDuration_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSecond;
    }
}