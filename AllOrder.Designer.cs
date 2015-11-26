namespace Client
{
    partial class AllOrder
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
            this.lbDiscount = new System.Windows.Forms.Label();
            this.TxtDiscount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.lbConsumption = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblbReceiveActual = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.line2 = new DevComponents.DotNetBar.Controls.Line();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.line3 = new DevComponents.DotNetBar.Controls.Line();
            this.Btn_Canel = new System.Windows.Forms.PictureBox();
            this.Btn_Ok = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Canel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Ok)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDiscount
            // 
            this.lbDiscount.AutoSize = true;
            this.lbDiscount.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lbDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.lbDiscount.Location = new System.Drawing.Point(48, 38);
            this.lbDiscount.Name = "lbDiscount";
            this.lbDiscount.Size = new System.Drawing.Size(66, 20);
            this.lbDiscount.TabIndex = 1;
            this.lbDiscount.Text = "折 扣 率:";
            // 
            // TxtDiscount
            // 
            this.TxtDiscount.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.TxtDiscount.Location = new System.Drawing.Point(161, 26);
            this.TxtDiscount.MaxLength = 2;
            this.TxtDiscount.Name = "TxtDiscount";
            this.TxtDiscount.Size = new System.Drawing.Size(142, 32);
            this.TxtDiscount.TabIndex = 2;
            this.TxtDiscount.TextChanged += new System.EventHandler(this.TxtDiscount_TextChanged);
            this.TxtDiscount.Enter += new System.EventHandler(this.TxtDiscount_Enter);
            this.TxtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDiscount_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(309, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(339, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "请输入折扣率";
            // 
            // line1
            // 
            this.line1.Font = new System.Drawing.Font("宋体", 14F);
            this.line1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.line1.Location = new System.Drawing.Point(-1, 59);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(482, 23);
            this.line1.TabIndex = 5;
            this.line1.Text = "line1";
            // 
            // lbConsumption
            // 
            this.lbConsumption.AutoSize = true;
            this.lbConsumption.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbConsumption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbConsumption.Location = new System.Drawing.Point(139, 94);
            this.lbConsumption.Name = "lbConsumption";
            this.lbConsumption.Size = new System.Drawing.Size(74, 21);
            this.lbConsumption.TabIndex = 7;
            this.lbConsumption.Text = "消费金额";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label3.Location = new System.Drawing.Point(48, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "消费金额:";
            // 
            // lblbReceiveActual
            // 
            this.lblbReceiveActual.AutoSize = true;
            this.lblbReceiveActual.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblbReceiveActual.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblbReceiveActual.Location = new System.Drawing.Point(360, 94);
            this.lblbReceiveActual.Name = "lblbReceiveActual";
            this.lblbReceiveActual.Size = new System.Drawing.Size(41, 21);
            this.lblbReceiveActual.TabIndex = 9;
            this.lblbReceiveActual.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label4.Location = new System.Drawing.Point(265, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "实收金额:";
            // 
            // line2
            // 
            this.line2.Font = new System.Drawing.Font("宋体", 14F);
            this.line2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.line2.Location = new System.Drawing.Point(-1, 118);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(482, 23);
            this.line2.TabIndex = 10;
            this.line2.Text = "line2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(139, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 21);
            this.label5.TabIndex = 12;
            this.label5.Text = "暂无";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label6.Location = new System.Drawing.Point(48, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "优惠原因:";
            // 
            // line3
            // 
            this.line3.Font = new System.Drawing.Font("宋体", 14F);
            this.line3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.line3.Location = new System.Drawing.Point(-1, 177);
            this.line3.Name = "line3";
            this.line3.Size = new System.Drawing.Size(482, 23);
            this.line3.TabIndex = 13;
            this.line3.Text = "line3";
            // 
            // Btn_Canel
            // 
            this.Btn_Canel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Canel.Location = new System.Drawing.Point(211, 302);
            this.Btn_Canel.Name = "Btn_Canel";
            this.Btn_Canel.Size = new System.Drawing.Size(92, 46);
            this.Btn_Canel.TabIndex = 18;
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
            this.Btn_Ok.TabIndex = 14;
            this.Btn_Ok.TabStop = false;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            this.Btn_Ok.MouseLeave += new System.EventHandler(this.Btn_Ok_MouseLeave);
            this.Btn_Ok.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Btn_Ok_MouseMove);
            // 
            // AllOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(480, 440);
            this.Controls.Add(this.Btn_Canel);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.line3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.line2);
            this.Controls.Add(this.lblbReceiveActual);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbConsumption);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtDiscount);
            this.Controls.Add(this.lbDiscount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AllOrder";
            this.Text = "AllOrder";
            this.Load += new System.EventHandler(this.AllOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Canel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Ok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDiscount;
        private System.Windows.Forms.TextBox TxtDiscount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.Line line1;
        private System.Windows.Forms.Label lbConsumption;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblbReceiveActual;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.Line line3;
        private System.Windows.Forms.PictureBox Btn_Ok;
        private System.Windows.Forms.PictureBox Btn_Canel;
        private DevComponents.DotNetBar.Controls.Line line2;
    }
}