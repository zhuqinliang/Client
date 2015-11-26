namespace Client
{
    partial class LockWindows
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
            this.PicLock = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Txt_PassWords = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicLock)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicLock
            // 
            this.PicLock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicLock.Image = global::Client.Properties.Resources._lock;
            this.PicLock.Location = new System.Drawing.Point(108, 30);
            this.PicLock.Name = "PicLock";
            this.PicLock.Size = new System.Drawing.Size(207, 204);
            this.PicLock.TabIndex = 0;
            this.PicLock.TabStop = false;
            this.PicLock.Click += new System.EventHandler(this.PicLock_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Txt_PassWords);
            this.panel1.Controls.Add(this.PicLock);
            this.panel1.Location = new System.Drawing.Point(125, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 313);
            this.panel1.TabIndex = 1;
            // 
            // Txt_PassWords
            // 
            this.Txt_PassWords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Txt_PassWords.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Txt_PassWords.Location = new System.Drawing.Point(108, 258);
            this.Txt_PassWords.Name = "Txt_PassWords";
            this.Txt_PassWords.PasswordChar = '●';
            this.Txt_PassWords.Size = new System.Drawing.Size(207, 27);
            this.Txt_PassWords.TabIndex = 1;
            this.Txt_PassWords.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Txt_PassWords.Visible = false;
            this.Txt_PassWords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_PassWords_KeyPress);
            // 
            // LockWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 458);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LockWindows";
            this.Opacity = 0.8D;
            this.Text = "LockWindows";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LockWindows_FormClosing);
            this.Load += new System.EventHandler(this.LockWindows_Load);
            this.Resize += new System.EventHandler(this.LockWindows_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PicLock)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PicLock;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Txt_PassWords;
    }
}