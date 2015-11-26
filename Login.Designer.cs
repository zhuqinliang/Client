namespace Client
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.TxtKey = new System.Windows.Forms.TextBox();
            this.TxtUser = new System.Windows.Forms.TextBox();
            this.picLogin = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picKey = new System.Windows.Forms.PictureBox();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.picCardOut = new System.Windows.Forms.PictureBox();
            this.pictureBoxSetup = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.picBackImage = new System.Windows.Forms.PictureBox();
            this.panelCardOut = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCardOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackImage)).BeginInit();
            this.panelCardOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtKey
            // 
            this.TxtKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtKey.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtKey.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.TxtKey.Location = new System.Drawing.Point(392, 48);
            this.TxtKey.Name = "TxtKey";
            this.TxtKey.PasswordChar = '●';
            this.TxtKey.Size = new System.Drawing.Size(184, 26);
            this.TxtKey.TabIndex = 3;
            this.TxtKey.Text = "请输入密码";
            this.TxtKey.Enter += new System.EventHandler(this.TxtKey_Enter);
            this.TxtKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKey_KeyPress);
            this.TxtKey.Leave += new System.EventHandler(this.TxtKey_Leave);
            // 
            // TxtUser
            // 
            this.TxtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtUser.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtUser.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.TxtUser.Location = new System.Drawing.Point(98, 48);
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.Size = new System.Drawing.Size(186, 26);
            this.TxtUser.TabIndex = 2;
            this.TxtUser.Text = "请输入用户名";
            this.TxtUser.Enter += new System.EventHandler(this.TxtUser_Enter);
            this.TxtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKey_KeyPress);
            this.TxtUser.Leave += new System.EventHandler(this.TxtUser_Leave);
            // 
            // picLogin
            // 
            this.picLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogin.Image = global::Client.Properties.Resources.loginmouseleave;
            this.picLogin.Location = new System.Drawing.Point(649, 25);
            this.picLogin.Name = "picLogin";
            this.picLogin.Size = new System.Drawing.Size(125, 65);
            this.picLogin.TabIndex = 11;
            this.picLogin.TabStop = false;
            this.picLogin.Click += new System.EventHandler(this.picLogin_Click);
            this.picLogin.MouseEnter += new System.EventHandler(this.picLogin_MouseEnter);
            this.picLogin.MouseLeave += new System.EventHandler(this.picLogin_MouseLeave);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(582, 45);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(26, 30);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Client.Properties.Resources.key_stroke;
            this.pictureBox2.Location = new System.Drawing.Point(357, 46);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 29);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(62, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 27);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // picKey
            // 
            this.picKey.BackColor = System.Drawing.Color.Transparent;
            this.picKey.Image = global::Client.Properties.Resources.圆角矩形_4;
            this.picKey.Location = new System.Drawing.Point(333, 25);
            this.picKey.Name = "picKey";
            this.picKey.Size = new System.Drawing.Size(288, 66);
            this.picKey.TabIndex = 10;
            this.picKey.TabStop = false;
            // 
            // picUser
            // 
            this.picUser.BackColor = System.Drawing.Color.Transparent;
            this.picUser.Image = ((System.Drawing.Image)(resources.GetObject("picUser.Image")));
            this.picUser.Location = new System.Drawing.Point(23, 25);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(288, 66);
            this.picUser.TabIndex = 9;
            this.picUser.TabStop = false;
            // 
            // picCardOut
            // 
            this.picCardOut.BackColor = System.Drawing.Color.Transparent;
            this.picCardOut.Image = ((System.Drawing.Image)(resources.GetObject("picCardOut.Image")));
            this.picCardOut.Location = new System.Drawing.Point(-1, 0);
            this.picCardOut.Name = "picCardOut";
            this.picCardOut.Size = new System.Drawing.Size(785, 108);
            this.picCardOut.TabIndex = 8;
            this.picCardOut.TabStop = false;
            // 
            // pictureBoxSetup
            // 
            this.pictureBoxSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSetup.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSetup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSetup.Image = global::Client.Properties.Resources.设置;
            this.pictureBoxSetup.Location = new System.Drawing.Point(746, 12);
            this.pictureBoxSetup.Name = "pictureBoxSetup";
            this.pictureBoxSetup.Size = new System.Drawing.Size(136, 135);
            this.pictureBoxSetup.TabIndex = 0;
            this.pictureBoxSetup.TabStop = false;
            this.pictureBoxSetup.Click += new System.EventHandler(this.pictureBoxSetup_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(298, 51);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(396, 171);
            this.pictureBoxLogo.TabIndex = 6;
            this.pictureBoxLogo.TabStop = false;
            // 
            // picBackImage
            // 
            this.picBackImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBackImage.Image = global::Client.Properties.Resources.bg;
            this.picBackImage.Location = new System.Drawing.Point(0, 0);
            this.picBackImage.Name = "picBackImage";
            this.picBackImage.Size = new System.Drawing.Size(894, 571);
            this.picBackImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBackImage.TabIndex = 7;
            this.picBackImage.TabStop = false;
            // 
            // panelCardOut
            // 
            this.panelCardOut.BackColor = System.Drawing.Color.Transparent;
            this.panelCardOut.Controls.Add(this.pictureBox3);
            this.panelCardOut.Controls.Add(this.TxtUser);
            this.panelCardOut.Controls.Add(this.pictureBox1);
            this.panelCardOut.Controls.Add(this.TxtKey);
            this.panelCardOut.Controls.Add(this.pictureBox2);
            this.panelCardOut.Controls.Add(this.picLogin);
            this.panelCardOut.Controls.Add(this.picKey);
            this.panelCardOut.Controls.Add(this.picUser);
            this.panelCardOut.Controls.Add(this.picCardOut);
            this.panelCardOut.Location = new System.Drawing.Point(51, 247);
            this.panelCardOut.Name = "panelCardOut";
            this.panelCardOut.Size = new System.Drawing.Size(785, 108);
            this.panelCardOut.TabIndex = 12;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(894, 571);
            this.Controls.Add(this.panelCardOut);
            this.Controls.Add(this.pictureBoxSetup);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.picBackImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Login_Load);
            this.Resize += new System.EventHandler(this.Login_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCardOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackImage)).EndInit();
            this.panelCardOut.ResumeLayout(false);
            this.panelCardOut.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSetup;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox TxtUser;
        private System.Windows.Forms.TextBox TxtKey;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox picBackImage;
        private System.Windows.Forms.PictureBox picCardOut;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.PictureBox picKey;
        private System.Windows.Forms.PictureBox picLogin;
        private System.Windows.Forms.Panel panelCardOut;

    }
}