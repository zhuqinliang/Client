namespace Client
{
    partial class MalingOrder
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
            this.Btn_Canel = new System.Windows.Forms.PictureBox();
            this.Btn_Ok = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDiscount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbReceiveShould = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.line2 = new DevComponents.DotNetBar.Controls.Line();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Canel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Ok)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Canel
            // 
            this.Btn_Canel.Location = new System.Drawing.Point(211, 302);
            this.Btn_Canel.Name = "Btn_Canel";
            this.Btn_Canel.Size = new System.Drawing.Size(92, 46);
            this.Btn_Canel.TabIndex = 33;
            this.Btn_Canel.TabStop = false;
            this.Btn_Canel.Click += new System.EventHandler(this.Btn_Canel_Click);
            this.Btn_Canel.MouseLeave += new System.EventHandler(this.Btn_Canel_MouseLeave);
            this.Btn_Canel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Btn_Canel_MouseMove);
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Ok.Location = new System.Drawing.Point(364, 302);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(92, 46);
            this.Btn_Ok.TabIndex = 29;
            this.Btn_Ok.TabStop = false;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            this.Btn_Ok.MouseLeave += new System.EventHandler(this.Btn_Ok_MouseLeave);
            this.Btn_Ok.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Btn_Ok_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(306, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 21);
            this.label2.TabIndex = 28;
            this.label2.Text = "抹零金额应>0";
            // 
            // TxtDiscount
            // 
            this.TxtDiscount.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtDiscount.Location = new System.Drawing.Point(169, 84);
            this.TxtDiscount.MaxLength = 1000;
            this.TxtDiscount.Name = "TxtDiscount";
            this.TxtDiscount.Size = new System.Drawing.Size(124, 33);
            this.TxtDiscount.TabIndex = 27;
            this.TxtDiscount.TextChanged += new System.EventHandler(this.TxtDiscount_TextChanged);
            this.TxtDiscount.Enter += new System.EventHandler(this.TxtDiscount_Enter);
            this.TxtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDiscount_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(48, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "抹零金额:";
            // 
            // lbReceiveShould
            // 
            this.lbReceiveShould.AutoSize = true;
            this.lbReceiveShould.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbReceiveShould.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbReceiveShould.Location = new System.Drawing.Point(135, 37);
            this.lbReceiveShould.Name = "lbReceiveShould";
            this.lbReceiveShould.Size = new System.Drawing.Size(74, 21);
            this.lbReceiveShould.TabIndex = 25;
            this.lbReceiveShould.Text = "应收金额";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label3.Location = new System.Drawing.Point(48, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "应收金额:";
            // 
            // line2
            // 
            this.line2.Font = new System.Drawing.Font("宋体", 14F);
            this.line2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.line2.Location = new System.Drawing.Point(-1, 118);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(482, 23);
            this.line2.TabIndex = 23;
            this.line2.Text = "line2";
            // 
            // line1
            // 
            this.line1.Font = new System.Drawing.Font("宋体", 14F);
            this.line1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.line1.Location = new System.Drawing.Point(-1, 59);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(482, 23);
            this.line1.TabIndex = 22;
            this.line1.Text = "line1";
            // 
            // MalingOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 440);
            this.Controls.Add(this.Btn_Canel);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtDiscount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbReceiveShould);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.line2);
            this.Controls.Add(this.line1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MalingOrder";
            this.Text = "MalingOrder";
            this.Load += new System.EventHandler(this.MalingOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Canel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Ok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Btn_Canel;
        private System.Windows.Forms.PictureBox Btn_Ok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtDiscount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbReceiveShould;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.Line line2;
        private DevComponents.DotNetBar.Controls.Line line1;
    }
}