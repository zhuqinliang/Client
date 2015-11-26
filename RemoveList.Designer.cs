namespace Client
{
    partial class RemoveList
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
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_Master = new System.Windows.Forms.TextBox();
            this.Btn_Canel = new System.Windows.Forms.Button();
            this.Btn_Enter = new System.Windows.Forms.Button();
            this.panelTables = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 27);
            this.label2.TabIndex = 25;
            this.label2.Text = "拆单";
            // 
            // Txt_Master
            // 
            this.Txt_Master.Location = new System.Drawing.Point(121, 16);
            this.Txt_Master.Name = "Txt_Master";
            this.Txt_Master.Size = new System.Drawing.Size(253, 21);
            this.Txt_Master.TabIndex = 24;
            this.Txt_Master.Visible = false;
            // 
            // Btn_Canel
            // 
            this.Btn_Canel.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Btn_Canel.Location = new System.Drawing.Point(779, 16);
            this.Btn_Canel.Name = "Btn_Canel";
            this.Btn_Canel.Size = new System.Drawing.Size(76, 46);
            this.Btn_Canel.TabIndex = 23;
            this.Btn_Canel.Text = "取消";
            this.Btn_Canel.UseVisualStyleBackColor = true;
            this.Btn_Canel.Click += new System.EventHandler(this.Btn_Canel_Click);
            // 
            // Btn_Enter
            // 
            this.Btn_Enter.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.Btn_Enter.Location = new System.Drawing.Point(637, 16);
            this.Btn_Enter.Name = "Btn_Enter";
            this.Btn_Enter.Size = new System.Drawing.Size(76, 46);
            this.Btn_Enter.TabIndex = 22;
            this.Btn_Enter.Text = "确认";
            this.Btn_Enter.UseVisualStyleBackColor = true;
            this.Btn_Enter.Click += new System.EventHandler(this.Btn_Enter_Click);
            // 
            // panelTables
            // 
            this.panelTables.AutoScroll = true;
            this.panelTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTables.Location = new System.Drawing.Point(2, 68);
            this.panelTables.Name = "panelTables";
            this.panelTables.Size = new System.Drawing.Size(877, 502);
            this.panelTables.TabIndex = 26;
            // 
            // RemoveList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 572);
            this.Controls.Add(this.panelTables);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Txt_Master);
            this.Controls.Add(this.Btn_Canel);
            this.Controls.Add(this.Btn_Enter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RemoveList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RemoveList";
            this.Load += new System.EventHandler(this.RemoveList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_Master;
        private System.Windows.Forms.Button Btn_Canel;
        private System.Windows.Forms.Button Btn_Enter;
        private System.Windows.Forms.Panel panelTables;
    }
}