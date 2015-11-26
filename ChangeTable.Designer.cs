namespace Client
{
    partial class ChangeTable
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
            this.Txt_ID = new System.Windows.Forms.TextBox();
            this.Btn_Canel = new System.Windows.Forms.Button();
            this.Btn_Enter = new System.Windows.Forms.Button();
            this.Btn_Find = new System.Windows.Forms.Button();
            this.Txt_Find = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTable = new System.Windows.Forms.Label();
            this.panelTables = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 27);
            this.label1.TabIndex = 29;
            this.label1.Text = "换台";
            // 
            // Txt_ID
            // 
            this.Txt_ID.Location = new System.Drawing.Point(100, 53);
            this.Txt_ID.Name = "Txt_ID";
            this.Txt_ID.Size = new System.Drawing.Size(253, 21);
            this.Txt_ID.TabIndex = 28;
            this.Txt_ID.Visible = false;
            // 
            // Btn_Canel
            // 
            this.Btn_Canel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Canel.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Btn_Canel.Location = new System.Drawing.Point(801, 33);
            this.Btn_Canel.Name = "Btn_Canel";
            this.Btn_Canel.Size = new System.Drawing.Size(76, 46);
            this.Btn_Canel.TabIndex = 27;
            this.Btn_Canel.Text = "取消";
            this.Btn_Canel.UseVisualStyleBackColor = true;
            this.Btn_Canel.Click += new System.EventHandler(this.Btn_Canel_Click);
            // 
            // Btn_Enter
            // 
            this.Btn_Enter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Enter.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Btn_Enter.Location = new System.Drawing.Point(705, 33);
            this.Btn_Enter.Name = "Btn_Enter";
            this.Btn_Enter.Size = new System.Drawing.Size(76, 46);
            this.Btn_Enter.TabIndex = 26;
            this.Btn_Enter.Text = "确认";
            this.Btn_Enter.UseVisualStyleBackColor = true;
            this.Btn_Enter.Click += new System.EventHandler(this.Btn_Enter_Click);
            // 
            // Btn_Find
            // 
            this.Btn_Find.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Find.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Btn_Find.Location = new System.Drawing.Point(609, 33);
            this.Btn_Find.Name = "Btn_Find";
            this.Btn_Find.Size = new System.Drawing.Size(76, 46);
            this.Btn_Find.TabIndex = 25;
            this.Btn_Find.Text = "查询";
            this.Btn_Find.UseVisualStyleBackColor = true;
            this.Btn_Find.Click += new System.EventHandler(this.Btn_Find_Click);
            // 
            // Txt_Find
            // 
            this.Txt_Find.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Txt_Find.Location = new System.Drawing.Point(417, 40);
            this.Txt_Find.Name = "Txt_Find";
            this.Txt_Find.Size = new System.Drawing.Size(155, 34);
            this.Txt_Find.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(363, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 27);
            this.label2.TabIndex = 23;
            this.label2.Text = "查找:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(113, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 27);
            this.label3.TabIndex = 30;
            this.label3.Text = "当前桌:";
            // 
            // lbTable
            // 
            this.lbTable.AutoSize = true;
            this.lbTable.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lbTable.Location = new System.Drawing.Point(186, 5);
            this.lbTable.Name = "lbTable";
            this.lbTable.Size = new System.Drawing.Size(48, 20);
            this.lbTable.TabIndex = 31;
            this.lbTable.Text = "1F101";
            // 
            // panelTables
            // 
            this.panelTables.AutoScroll = true;
            this.panelTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTables.Location = new System.Drawing.Point(3, 80);
            this.panelTables.Name = "panelTables";
            this.panelTables.Size = new System.Drawing.Size(875, 489);
            this.panelTables.TabIndex = 32;
            // 
            // ChangeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 572);
            this.Controls.Add(this.panelTables);
            this.Controls.Add(this.lbTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_ID);
            this.Controls.Add(this.Btn_Canel);
            this.Controls.Add(this.Btn_Enter);
            this.Controls.Add(this.Btn_Find);
            this.Controls.Add(this.Txt_Find);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChangeTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "改台";
            this.Load += new System.EventHandler(this.ChangeTable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_ID;
        private System.Windows.Forms.Button Btn_Canel;
        private System.Windows.Forms.Button Btn_Enter;
        private System.Windows.Forms.Button Btn_Find;
        private System.Windows.Forms.TextBox Txt_Find;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbTable;
        private System.Windows.Forms.Panel panelTables;
    }
}