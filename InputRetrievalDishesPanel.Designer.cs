namespace Client
{
    partial class InputRetrievalDishesPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRetrievalCode = new System.Windows.Forms.TextBox();
            this.btnRetrieval = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRetrievalCode
            // 
            this.txtRetrievalCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRetrievalCode.Location = new System.Drawing.Point(35, 7);
            this.txtRetrievalCode.Name = "txtRetrievalCode";
            this.txtRetrievalCode.Size = new System.Drawing.Size(136, 26);
            this.txtRetrievalCode.TabIndex = 0;
            // 
            // btnRetrieval
            // 
            this.btnRetrieval.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRetrieval.Location = new System.Drawing.Point(254, 5);
            this.btnRetrieval.Name = "btnRetrieval";
            this.btnRetrieval.Size = new System.Drawing.Size(75, 30);
            this.btnRetrieval.TabIndex = 1;
            this.btnRetrieval.Text = "开始";
            this.btnRetrieval.UseVisualStyleBackColor = true;
            this.btnRetrieval.Click += new System.EventHandler(this.btnRetrieval_Click);
            // 
            // InputRetrievalDishesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRetrieval);
            this.Controls.Add(this.txtRetrievalCode);
            this.Name = "InputRetrievalDishesPanel";
            this.Size = new System.Drawing.Size(376, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRetrievalCode;
        private System.Windows.Forms.Button btnRetrieval;
    }
}
