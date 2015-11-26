namespace Client
{
    partial class Order
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Order));
            this.Btn_Classification = new System.Windows.Forms.Button();
            this.Btn_Simplecode = new System.Windows.Forms.Button();
            this.PanelParent = new System.Windows.Forms.FlowLayoutPanel();
            this.PanelChildren = new System.Windows.Forms.FlowLayoutPanel();
            this.PanelDishes = new System.Windows.Forms.FlowLayoutPanel();
            this.PanelInfor = new System.Windows.Forms.Panel();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.eleTempHistory = new System.Windows.Forms.Integration.ElementHost();
            this.dlvTempHistory = new WpfControls.DishesListView();
            this.eleTemp = new System.Windows.Forms.Integration.ElementHost();
            this.dlvTemp = new WpfControls.DishesListView();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.eleHistory = new System.Windows.Forms.Integration.ElementHost();
            this.dlvHistory = new WpfControls.HistoryDishesListView();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.imageListDirection = new System.Windows.Forms.ImageList(this.components);
            this.m_PanelDishesByRetrievalCode = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.PanelInfor.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Classification
            // 
            this.Btn_Classification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(84)))), ((int)(((byte)(19)))));
            this.Btn_Classification.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Classification.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Classification.Location = new System.Drawing.Point(144, 2);
            this.Btn_Classification.Name = "Btn_Classification";
            this.Btn_Classification.Size = new System.Drawing.Size(88, 37);
            this.Btn_Classification.TabIndex = 0;
            this.Btn_Classification.Text = "分类检索";
            this.Btn_Classification.UseVisualStyleBackColor = false;
            this.Btn_Classification.Click += new System.EventHandler(this.Btn_Classification_Click);
            // 
            // Btn_Simplecode
            // 
            this.Btn_Simplecode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Simplecode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Simplecode.Location = new System.Drawing.Point(254, 2);
            this.Btn_Simplecode.Name = "Btn_Simplecode";
            this.Btn_Simplecode.Size = new System.Drawing.Size(88, 37);
            this.Btn_Simplecode.TabIndex = 1;
            this.Btn_Simplecode.Text = "简码检索";
            this.Btn_Simplecode.UseVisualStyleBackColor = true;
            this.Btn_Simplecode.Click += new System.EventHandler(this.Btn_Simplecode_Click);
            // 
            // PanelParent
            // 
            this.PanelParent.Cursor = System.Windows.Forms.Cursors.Default;
            this.PanelParent.Location = new System.Drawing.Point(0, 38);
            this.PanelParent.Name = "PanelParent";
            this.PanelParent.Size = new System.Drawing.Size(548, 40);
            this.PanelParent.TabIndex = 2;
            // 
            // PanelChildren
            // 
            this.PanelChildren.Cursor = System.Windows.Forms.Cursors.Default;
            this.PanelChildren.Location = new System.Drawing.Point(0, 77);
            this.PanelChildren.Name = "PanelChildren";
            this.PanelChildren.Size = new System.Drawing.Size(547, 26);
            this.PanelChildren.TabIndex = 3;
            // 
            // PanelDishes
            // 
            this.PanelDishes.AutoScroll = true;
            this.PanelDishes.Cursor = System.Windows.Forms.Cursors.Default;
            this.PanelDishes.Location = new System.Drawing.Point(0, 102);
            this.PanelDishes.Name = "PanelDishes";
            this.PanelDishes.Size = new System.Drawing.Size(573, 327);
            this.PanelDishes.TabIndex = 5;
            // 
            // PanelInfor
            // 
            this.PanelInfor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelInfor.Controls.Add(this.lblTitle2);
            this.PanelInfor.Controls.Add(this.eleTempHistory);
            this.PanelInfor.Controls.Add(this.eleTemp);
            this.PanelInfor.Controls.Add(this.lblTitle1);
            this.PanelInfor.Controls.Add(this.eleHistory);
            this.PanelInfor.Location = new System.Drawing.Point(576, 0);
            this.PanelInfor.Name = "PanelInfor";
            this.PanelInfor.Size = new System.Drawing.Size(546, 473);
            this.PanelInfor.TabIndex = 6;
            this.PanelInfor.SizeChanged += new System.EventHandler(this.PanelInfor_SizeChanged);
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblTitle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle2.Location = new System.Drawing.Point(6, 249);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(58, 22);
            this.lblTitle2.TabIndex = 1;
            this.lblTitle2.Text = "追加菜";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eleTempHistory
            // 
            this.eleTempHistory.Location = new System.Drawing.Point(56, 37);
            this.eleTempHistory.Name = "eleTempHistory";
            this.eleTempHistory.Size = new System.Drawing.Size(463, 189);
            this.eleTempHistory.TabIndex = 8;
            this.eleTempHistory.Text = "elementHost3";
            this.eleTempHistory.Child = this.dlvTempHistory;
            // 
            // eleTemp
            // 
            this.eleTemp.Location = new System.Drawing.Point(19, 317);
            this.eleTemp.Name = "eleTemp";
            this.eleTemp.Size = new System.Drawing.Size(413, 111);
            this.eleTemp.TabIndex = 9;
            this.eleTemp.Text = "elementHost1";
            this.eleTemp.Child = this.dlvTemp;
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblTitle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle1.Location = new System.Drawing.Point(6, 1);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(58, 22);
            this.lblTitle1.TabIndex = 1;
            this.lblTitle1.Text = "已点菜";
            this.lblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eleHistory
            // 
            this.eleHistory.Location = new System.Drawing.Point(10, 37);
            this.eleHistory.Name = "eleHistory";
            this.eleHistory.Size = new System.Drawing.Size(509, 209);
            this.eleHistory.TabIndex = 6;
            this.eleHistory.Text = "elementHost2";
            this.eleHistory.Child = this.dlvHistory;
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.VisualStudio2010Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // imageListDirection
            // 
            this.imageListDirection.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDirection.ImageStream")));
            this.imageListDirection.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDirection.Images.SetKeyName(0, "打开");
            this.imageListDirection.Images.SetKeyName(1, "收起");
            // 
            // m_PanelDishesByRetrievalCode
            // 
            this.m_PanelDishesByRetrievalCode.AutoScroll = true;
            this.m_PanelDishesByRetrievalCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_PanelDishesByRetrievalCode.Location = new System.Drawing.Point(0, 77);
            this.m_PanelDishesByRetrievalCode.Name = "m_PanelDishesByRetrievalCode";
            this.m_PanelDishesByRetrievalCode.Size = new System.Drawing.Size(235, 239);
            this.m_PanelDishesByRetrievalCode.TabIndex = 11;
            this.m_PanelDishesByRetrievalCode.Visible = false;
            // 
            // panelBottom
            // 
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Location = new System.Drawing.Point(576, 475);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(546, 60);
            this.panelBottom.TabIndex = 12;
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(335, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 37);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(134, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 37);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 535);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.m_PanelDishesByRetrievalCode);
            this.Controls.Add(this.PanelInfor);
            this.Controls.Add(this.PanelChildren);
            this.Controls.Add(this.PanelParent);
            this.Controls.Add(this.Btn_Simplecode);
            this.Controls.Add(this.Btn_Classification);
            this.Controls.Add(this.PanelDishes);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Order";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "点菜";
            this.Load += new System.EventHandler(this.Order_Load);
            this.SizeChanged += new System.EventHandler(this.Order_SizeChanged);
            this.PanelInfor.ResumeLayout(false);
            this.PanelInfor.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Classification;
        private System.Windows.Forms.Button Btn_Simplecode;
        private System.Windows.Forms.FlowLayoutPanel PanelParent;
        private System.Windows.Forms.FlowLayoutPanel PanelChildren;
        private System.Windows.Forms.FlowLayoutPanel PanelDishes;
        private System.Windows.Forms.Panel PanelInfor;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        public System.Windows.Forms.ImageList imageListDirection;
        private System.Windows.Forms.FlowLayoutPanel m_PanelDishesByRetrievalCode;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Integration.ElementHost eleHistory;
        private WpfControls.HistoryDishesListView dlvHistory;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Integration.ElementHost eleTemp;
        private WpfControls.DishesListView dlvTemp;
        private System.Windows.Forms.Integration.ElementHost eleTempHistory;
        private WpfControls.DishesListView dlvTempHistory;
        private System.Windows.Forms.Label lblTitle2;
    }
}