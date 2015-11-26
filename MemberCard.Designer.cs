namespace Client
{
    partial class MemberCard
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
            this.TxtDiscount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbReceiveShould = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.line2 = new DevComponents.DotNetBar.Controls.Line();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.label2 = new System.Windows.Forms.Label();
            this.Btn_Member = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Txt_MemberCard = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDis = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Canel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Ok)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Canel
            // 
            this.Btn_Canel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Canel.Location = new System.Drawing.Point(211, 302);
            this.Btn_Canel.Name = "Btn_Canel";
            this.Btn_Canel.Size = new System.Drawing.Size(100, 50);
            this.Btn_Canel.TabIndex = 49;
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
            this.Btn_Ok.Size = new System.Drawing.Size(100, 50);
            this.Btn_Ok.TabIndex = 43;
            this.Btn_Ok.TabStop = false;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // TxtDiscount
            // 
            this.TxtDiscount.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtDiscount.Location = new System.Drawing.Point(323, 31);
            this.TxtDiscount.MaxLength = 1000;
            this.TxtDiscount.Name = "TxtDiscount";
            this.TxtDiscount.Size = new System.Drawing.Size(141, 33);
            this.TxtDiscount.TabIndex = 41;
            this.TxtDiscount.TextChanged += new System.EventHandler(this.TxtDiscount_TextChanged);
            this.TxtDiscount.Enter += new System.EventHandler(this.TxtDiscount_Enter);
            this.TxtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDiscount_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(232, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 40;
            this.label1.Text = "实收金额:";
            // 
            // lbReceiveShould
            // 
            this.lbReceiveShould.AutoSize = true;
            this.lbReceiveShould.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbReceiveShould.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbReceiveShould.Location = new System.Drawing.Point(137, 37);
            this.lbReceiveShould.Name = "lbReceiveShould";
            this.lbReceiveShould.Size = new System.Drawing.Size(74, 21);
            this.lbReceiveShould.TabIndex = 39;
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
            this.label3.TabIndex = 38;
            this.label3.Text = "应收金额:";
            // 
            // line2
            // 
            this.line2.Font = new System.Drawing.Font("宋体", 14F);
            this.line2.ForeColor = System.Drawing.Color.DarkGray;
            this.line2.Location = new System.Drawing.Point(-1, 118);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(482, 23);
            this.line2.TabIndex = 37;
            this.line2.Text = "line2";
            // 
            // line1
            // 
            this.line1.Font = new System.Drawing.Font("宋体", 14F);
            this.line1.ForeColor = System.Drawing.Color.DarkGray;
            this.line1.Location = new System.Drawing.Point(-1, 59);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(482, 23);
            this.line1.TabIndex = 36;
            this.line1.Text = "line1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(367, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 21);
            this.label2.TabIndex = 47;
            // 
            // Btn_Member
            // 
            this.Btn_Member.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Member.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Member.Location = new System.Drawing.Point(298, 85);
            this.Btn_Member.Name = "Btn_Member";
            this.Btn_Member.Size = new System.Drawing.Size(60, 31);
            this.Btn_Member.TabIndex = 48;
            this.Btn_Member.Text = "搜索(&Q)";
            this.Btn_Member.UseVisualStyleBackColor = true;
            this.Btn_Member.Click += new System.EventHandler(this.Btn_Member_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label4.Location = new System.Drawing.Point(44, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 20);
            this.label4.TabIndex = 50;
            this.label4.Text = "会员卡:";
            // 
            // Txt_MemberCard
            // 
            this.Txt_MemberCard.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Txt_MemberCard.Location = new System.Drawing.Point(108, 85);
            this.Txt_MemberCard.MaxLength = 1000;
            this.Txt_MemberCard.Name = "Txt_MemberCard";
            this.Txt_MemberCard.Size = new System.Drawing.Size(184, 33);
            this.Txt_MemberCard.TabIndex = 51;
            this.Txt_MemberCard.TextChanged += new System.EventHandler(this.Txt_MemberCard_TextChanged);
            this.Txt_MemberCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_MemberCard_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label5.Location = new System.Drawing.Point(44, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 20);
            this.label5.TabIndex = 52;
            this.label5.Text = "姓名:";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.ForeColor = System.Drawing.Color.Red;
            this.txtUserName.Location = new System.Drawing.Point(108, 137);
            this.txtUserName.MaxLength = 1000;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(146, 33);
            this.txtUserName.TabIndex = 53;
            // 
            // txtMobile
            // 
            this.txtMobile.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMobile.ForeColor = System.Drawing.Color.Red;
            this.txtMobile.Location = new System.Drawing.Point(320, 137);
            this.txtMobile.MaxLength = 1000;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ReadOnly = true;
            this.txtMobile.Size = new System.Drawing.Size(146, 33);
            this.txtMobile.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label6.Location = new System.Drawing.Point(265, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 20);
            this.label6.TabIndex = 54;
            this.label6.Text = "手机号:";
            // 
            // txtDis
            // 
            this.txtDis.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDis.ForeColor = System.Drawing.Color.Red;
            this.txtDis.Location = new System.Drawing.Point(108, 182);
            this.txtDis.MaxLength = 1000;
            this.txtDis.Name = "txtDis";
            this.txtDis.ReadOnly = true;
            this.txtDis.Size = new System.Drawing.Size(146, 33);
            this.txtDis.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label7.Location = new System.Drawing.Point(44, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 20);
            this.label7.TabIndex = 56;
            this.label7.Text = "折扣:";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAmount.ForeColor = System.Drawing.Color.Red;
            this.txtAmount.Location = new System.Drawing.Point(320, 182);
            this.txtAmount.MaxLength = 1000;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(146, 33);
            this.txtAmount.TabIndex = 59;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label8.Location = new System.Drawing.Point(265, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 20);
            this.label8.TabIndex = 58;
            this.label8.Text = "余额:";
            // 
            // MemberCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 440);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDis);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Txt_MemberCard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Btn_Member);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_Canel);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.TxtDiscount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbReceiveShould);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.line2);
            this.Controls.Add(this.line1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MemberCard";
            this.Text = "MemberCard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MemberCard_FormClosed);
            this.Load += new System.EventHandler(this.MemberCard_Load);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_Member;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Txt_MemberCard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDis;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label8;
    }
}