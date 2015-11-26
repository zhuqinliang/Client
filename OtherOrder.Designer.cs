namespace Client
{
    partial class OtherOrder
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
            this.TxtDiscount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbReceiveShould = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.line2 = new DevComponents.DotNetBar.Controls.Line();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.line3 = new DevComponents.DotNetBar.Controls.Line();
            this.label2 = new System.Windows.Forms.Label();
            this.panelChoose = new System.Windows.Forms.Panel();
            this.lbReasons = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.picPart = new System.Windows.Forms.PictureBox();
            this.Btn_Canel = new System.Windows.Forms.PictureBox();
            this.Btn_Ok = new System.Windows.Forms.PictureBox();
            this.panelChoose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Canel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Ok)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtDiscount
            // 
            this.TxtDiscount.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtDiscount.Location = new System.Drawing.Point(135, 88);
            this.TxtDiscount.MaxLength = 10;
            this.TxtDiscount.Name = "TxtDiscount";
            this.TxtDiscount.Size = new System.Drawing.Size(124, 33);
            this.TxtDiscount.TabIndex = 66;
            this.TxtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDiscount_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(18, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 65;
            this.label1.Text = "其他支付金额:";
            // 
            // lbReceiveShould
            // 
            this.lbReceiveShould.AutoSize = true;
            this.lbReceiveShould.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbReceiveShould.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbReceiveShould.Location = new System.Drawing.Point(135, 38);
            this.lbReceiveShould.Name = "lbReceiveShould";
            this.lbReceiveShould.Size = new System.Drawing.Size(74, 21);
            this.lbReceiveShould.TabIndex = 64;
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
            this.label3.TabIndex = 63;
            this.label3.Text = "应收金额:";
            // 
            // line2
            // 
            this.line2.Font = new System.Drawing.Font("宋体", 14F);
            this.line2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.line2.Location = new System.Drawing.Point(-1, 118);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(482, 23);
            this.line2.TabIndex = 62;
            this.line2.Text = "line2";
            // 
            // line1
            // 
            this.line1.Font = new System.Drawing.Font("宋体", 14F);
            this.line1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.line1.Location = new System.Drawing.Point(-1, 59);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(482, 23);
            this.line1.TabIndex = 61;
            this.line1.Text = "line1";
            // 
            // line3
            // 
            this.line3.Font = new System.Drawing.Font("宋体", 14F);
            this.line3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.line3.Location = new System.Drawing.Point(-1, 177);
            this.line3.Name = "line3";
            this.line3.Size = new System.Drawing.Size(482, 23);
            this.line3.TabIndex = 69;
            this.line3.Text = "line3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label2.Location = new System.Drawing.Point(49, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 70;
            this.label2.Text = "支付原因:";
            this.label2.Click += new System.EventHandler(this.panelChoose_Click);
            // 
            // panelChoose
            // 
            this.panelChoose.Controls.Add(this.lbReasons);
            this.panelChoose.Controls.Add(this.label4);
            this.panelChoose.Controls.Add(this.label2);
            this.panelChoose.Controls.Add(this.picPart);
            this.panelChoose.Location = new System.Drawing.Point(-1, 132);
            this.panelChoose.Name = "panelChoose";
            this.panelChoose.Size = new System.Drawing.Size(482, 53);
            this.panelChoose.TabIndex = 71;
            this.panelChoose.Click += new System.EventHandler(this.panelChoose_Click);
            // 
            // lbReasons
            // 
            this.lbReasons.AutoSize = true;
            this.lbReasons.Location = new System.Drawing.Point(251, 23);
            this.lbReasons.Name = "lbReasons";
            this.lbReasons.Size = new System.Drawing.Size(0, 12);
            this.lbReasons.TabIndex = 72;
            this.lbReasons.Visible = false;
            this.lbReasons.Click += new System.EventHandler(this.panelChoose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(188, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 21);
            this.label4.TabIndex = 71;
            this.label4.Text = "请选择其他原因";
            this.label4.Click += new System.EventHandler(this.panelChoose_Click);
            // 
            // picPart
            // 
            this.picPart.Image = global::Client.Properties.Resources.组1;
            this.picPart.Location = new System.Drawing.Point(425, 15);
            this.picPart.Name = "picPart";
            this.picPart.Size = new System.Drawing.Size(21, 26);
            this.picPart.TabIndex = 38;
            this.picPart.TabStop = false;
            this.picPart.Click += new System.EventHandler(this.panelChoose_Click);
            // 
            // Btn_Canel
            // 
            this.Btn_Canel.Location = new System.Drawing.Point(211, 302);
            this.Btn_Canel.Name = "Btn_Canel";
            this.Btn_Canel.Size = new System.Drawing.Size(100, 50);
            this.Btn_Canel.TabIndex = 67;
            this.Btn_Canel.TabStop = false;
            this.Btn_Canel.Click += new System.EventHandler(this.Btn_Canel_Click);
            this.Btn_Canel.MouseLeave += new System.EventHandler(this.Btn_Canel_MouseLeave);
            this.Btn_Canel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Btn_Canel_MouseMove);
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Location = new System.Drawing.Point(364, 302);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(100, 50);
            this.Btn_Ok.TabIndex = 68;
            this.Btn_Ok.TabStop = false;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            this.Btn_Ok.MouseLeave += new System.EventHandler(this.Btn_Ok_MouseLeave);
            this.Btn_Ok.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Btn_Ok_MouseMove);
            // 
            // OtherOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 440);
            this.Controls.Add(this.panelChoose);
            this.Controls.Add(this.line3);
            this.Controls.Add(this.Btn_Canel);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.TxtDiscount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbReceiveShould);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.line2);
            this.Controls.Add(this.line1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OtherOrder";
            this.Text = "OtherOrder";
            this.Load += new System.EventHandler(this.OtherOrder_Load);
            this.panelChoose.ResumeLayout(false);
            this.panelChoose.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Canel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Ok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Btn_Canel;
        private System.Windows.Forms.PictureBox Btn_Ok;
        private System.Windows.Forms.TextBox TxtDiscount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbReceiveShould;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.Line line2;
        private DevComponents.DotNetBar.Controls.Line line1;
        private DevComponents.DotNetBar.Controls.Line line3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelChoose;
        private System.Windows.Forms.PictureBox picPart;
        public System.Windows.Forms.Label lbReasons;
        public System.Windows.Forms.Label label4;
    }
}