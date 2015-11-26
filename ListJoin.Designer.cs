namespace Client
{
    partial class ListJoin
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
            this.panelTables = new System.Windows.Forms.Panel();
            this.Btn_Find = new System.Windows.Forms.Button();
            this.Txt_Find = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Btn_Enter = new System.Windows.Forms.Button();
            this.Btn_Canel = new System.Windows.Forms.Button();
            this.Txt_Master = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelTables
            // 
            this.panelTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTables.AutoScroll = true;
            this.panelTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTables.Location = new System.Drawing.Point(2, 78);
            this.panelTables.Name = "panelTables";
            this.panelTables.Size = new System.Drawing.Size(878, 491);
            this.panelTables.TabIndex = 11;
            // 
            // Btn_Find
            // 
            this.Btn_Find.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Btn_Find.Location = new System.Drawing.Point(602, 18);
            this.Btn_Find.Name = "Btn_Find";
            this.Btn_Find.Size = new System.Drawing.Size(76, 46);
            this.Btn_Find.TabIndex = 10;
            this.Btn_Find.Text = "查询";
            this.Btn_Find.UseVisualStyleBackColor = true;
            this.Btn_Find.Click += new System.EventHandler(this.Btn_Find_Click);
            // 
            // Txt_Find
            // 
            this.Txt_Find.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Txt_Find.Location = new System.Drawing.Point(281, 26);
            this.Txt_Find.Name = "Txt_Find";
            this.Txt_Find.Size = new System.Drawing.Size(250, 34);
            this.Txt_Find.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(202, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 27);
            this.label2.TabIndex = 8;
            this.label2.Text = "查找:";
            // 
            // Btn_Enter
            // 
            this.Btn_Enter.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Btn_Enter.Location = new System.Drawing.Point(698, 18);
            this.Btn_Enter.Name = "Btn_Enter";
            this.Btn_Enter.Size = new System.Drawing.Size(76, 46);
            this.Btn_Enter.TabIndex = 11;
            this.Btn_Enter.Text = "确认";
            this.Btn_Enter.UseVisualStyleBackColor = true;
            this.Btn_Enter.Click += new System.EventHandler(this.Btn_Enter_Click);
            // 
            // Btn_Canel
            // 
            this.Btn_Canel.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Btn_Canel.Location = new System.Drawing.Point(794, 18);
            this.Btn_Canel.Name = "Btn_Canel";
            this.Btn_Canel.Size = new System.Drawing.Size(76, 46);
            this.Btn_Canel.TabIndex = 12;
            this.Btn_Canel.Text = "取消";
            this.Btn_Canel.UseVisualStyleBackColor = true;
            this.Btn_Canel.Click += new System.EventHandler(this.Btn_Canel_Click);
            // 
            // Txt_Master
            // 
            this.Txt_Master.Location = new System.Drawing.Point(96, 4);
            this.Txt_Master.Name = "Txt_Master";
            this.Txt_Master.Size = new System.Drawing.Size(253, 21);
            this.Txt_Master.TabIndex = 13;
            this.Txt_Master.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 27);
            this.label1.TabIndex = 22;
            this.label1.Text = "并单";
            // 
            // ListJoin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 572);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_Master);
            this.Controls.Add(this.Btn_Canel);
            this.Controls.Add(this.Btn_Enter);
            this.Controls.Add(this.panelTables);
            this.Controls.Add(this.Btn_Find);
            this.Controls.Add(this.Txt_Find);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ListJoin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "并单";
            this.Load += new System.EventHandler(this.ListJoin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTables;
        private System.Windows.Forms.Button Btn_Find;
        private System.Windows.Forms.TextBox Txt_Find;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_Canel;
        private System.Windows.Forms.Button Btn_Enter;
        private System.Windows.Forms.TextBox Txt_Master;
        private System.Windows.Forms.Label label1;
    }
}