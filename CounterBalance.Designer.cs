namespace Client
{
    partial class CounterBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CounterBalance));
            this.label1 = new System.Windows.Forms.Label();
            this.TxtNote = new System.Windows.Forms.TextBox();
            this.Btn_Enter = new System.Windows.Forms.Button();
            this.Btn_Clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入反结算原因";
            // 
            // TxtNote
            // 
            this.TxtNote.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtNote.Location = new System.Drawing.Point(12, 47);
            this.TxtNote.Multiline = true;
            this.TxtNote.Name = "TxtNote";
            this.TxtNote.Size = new System.Drawing.Size(239, 93);
            this.TxtNote.TabIndex = 1;
            // 
            // Btn_Enter
            // 
            this.Btn_Enter.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Enter.Location = new System.Drawing.Point(128, 154);
            this.Btn_Enter.Name = "Btn_Enter";
            this.Btn_Enter.Size = new System.Drawing.Size(51, 34);
            this.Btn_Enter.TabIndex = 2;
            this.Btn_Enter.Text = "确认";
            this.Btn_Enter.UseVisualStyleBackColor = true;
            this.Btn_Enter.Click += new System.EventHandler(this.Btn_Enter_Click);
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Clear.Location = new System.Drawing.Point(207, 154);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(51, 34);
            this.Btn_Clear.TabIndex = 3;
            this.Btn_Clear.Text = "清空";
            this.Btn_Clear.UseVisualStyleBackColor = true;
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // CounterBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 192);
            this.Controls.Add(this.Btn_Clear);
            this.Controls.Add(this.Btn_Enter);
            this.Controls.Add(this.TxtNote);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CounterBalance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "反结算";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtNote;
        private System.Windows.Forms.Button Btn_Enter;
        private System.Windows.Forms.Button Btn_Clear;
    }
}