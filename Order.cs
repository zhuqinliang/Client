using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Collections;
using DevComponents.DotNetBar;
using WpfControls;
using Business;

namespace Client
{
    public partial class Order : Form
    {
        ICallOrderForm m_frmCashier;
        string m_CurrentConsumptionId;
        public Order(ICallOrderForm p_frmCashier, string p_CurrentConsumptionId)
        {
            InitializeComponent();

            m_frmCashier = p_frmCashier;
            m_CurrentConsumptionId = p_CurrentConsumptionId;

            this.Location = new System.Drawing.Point(0, 0);
            this.Size = new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);
        }

        public string ID_CategoriesParent = null;//菜品大类ID
        public string ID_CategoriesChildren = null;//菜品小类ID
        public string Name_SelectedParent = null;//当前选中菜品大类名称
        public string Name_SelectedChilden = null;//当前选中菜品小类名称
        public int count_Add = 0;//初始化定义一个点菜份数
        public double Price_All = 0.00;//初始化定义一个点菜总价
        List<string> CategoriesParent = new List<string>();//菜品大类集合
        List<string> CategoriesChildren = new List<string>();//菜品小类集合
        List<Item> dishes = new List<Item>();//菜的做法集合
        List<string> Flavor_Dishes = new List<string>();//菜的口味集合
        List<string> ID_Flavor_Dishes = new List<string>();//菜的口味ID集合
        public string Str_Categories;//接受到的菜品信息的JSON格式
        public string Str_Alldishes;//接受到的所有的菜品的JSON格式
        public string ScriptSerialize<T>(T t)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(t);
        }

        public T ScriptDeserialize<T>(string strJson)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<T>(strJson);
        }

        //菜品传值
        public void getstrCategories()
        {
            GetInformation.address = "categories";//菜品
            GetInformation gc = new GetInformation();
            Str_Categories = gc.GetHTTPInfo();//接收JSON数据
        }

        //所有菜品名称
        public void getalldishes()
        {
            GetInformation.address = "menu";//所有菜品
            GetInformation gc = new GetInformation();
            Str_Alldishes = gc.GetHTTPInfo();//接受JSON数据
        }

        //菜品父按钮的构造(菜品大类)
        private void AddCategories()
        {
            getstrCategories();
            var jserCategories = new JavaScriptSerializer();
            var personsCategories = jserCategories.Deserialize<List<Categories>>(Str_Categories);//解析json数据
            for (int i = 0; i < personsCategories.Count(); i++)
            {
                CategoriesParent.Add(personsCategories[i].name);
                Button bt = new Button();
                bt.Size = new Size(88, 37);
                bt.Tag = personsCategories[i].id;
                bt.Text = CategoriesParent[i].ToString();
                this.PanelParent.Controls.Add(bt);
                bt.Click += new EventHandler(btnParent_Click);
                bt.Cursor = Cursors.Hand;
                bt.Font = new Font("微软雅黑", 10);
                ID_CategoriesParent = bt.Tag.ToString();
            }
        }

        //父按钮的点击事件(菜品小类)
        public void btnParent_Click(object sender, EventArgs e)
        {
            foreach (Button bt in this.PanelParent.Controls)
            { bt.BackColor = Color.FromKnownColor(KnownColor.Control); }
            (sender as Button).BackColor = Color.FromArgb(234, 84, 19);
            Name_SelectedParent = (sender as Button).Text;//将当前大类存进公共变量
            this.PanelChildren.Controls.Clear();//先清空一次子容器
            ID_CategoriesParent = (sender as Button).Tag.ToString();//提取出父容器里点击的按钮ID
            CategoriesChildren.Clear();//存储菜品的名称集合也清空一次
            var jserCategories = new JavaScriptSerializer();
            var personsCategories = jserCategories.Deserialize<List<Categories>>(Str_Categories);//解析json数据
            for (int i = 0; i < personsCategories.Count(); i++)
            {
                for (int j = 0; j < personsCategories[i].children.Count(); j++)
                {
                    if (personsCategories[i].id == ID_CategoriesParent)
                    {
                        CategoriesChildren.Add(personsCategories[i].children[j].name.ToString());
                        Button bt = new Button();
                        this.PanelChildren.Controls.Add(bt);//添加按钮组
                        bt.Text = CategoriesChildren[j].ToString();//按钮内容
                        bt.Click += new EventHandler(btnChildren_Click);
                        bt.Cursor = Cursors.Hand;//手型按钮
                        bt.Font = new Font("微软雅黑", 9);
                        bt.Tag = personsCategories[i].children[j].id;//给按钮的Tag属性赋值
                    }
                }
            }
        }

        //子按钮的点击事件
        public void btnChildren_Click(object sender, EventArgs e)
        {
            PanelDishes.Size = new Size(this.ClientSize.Width - PanelInfor.Width - 2 - 5 - PanelDishes.Location.X, this.ClientSize.Height - PanelDishes.Location.Y);

            foreach (Button bt in this.PanelChildren.Controls)
            { bt.BackColor = Color.FromKnownColor(KnownColor.Control); }
            (sender as Button).BackColor = Color.FromArgb(234, 84, 19);
            this.PanelDishes.Controls.Clear();
            dishes.Clear();
            getalldishes();
            Dishes ds = ScriptDeserialize<Dishes>(Str_Alldishes);//解析json数据
            ID_CategoriesChildren = (sender as Button).Tag.ToString();
            for (int i = 0; i < ds.items.Count(); i++)
            {
                if (ds.items[i].category != null)
                {
                    if (ID_CategoriesChildren == ds.items[i].category.id)
                    {
                        dishes.Add(ds.items[i]);
                    }
                }
            }
            //构造点菜菜单
            for (int j = 0; j < dishes.Count(); j++)
            {
                DishControl.DishControl dc = new DishControl.DishControl();
                dc.lbOrder.Text = dishes[j].name;//显示菜名
                dc.lbPrice.Text = "￥" + dishes[j].price;//显示菜价
                this.PanelDishes.Controls.Add(dc);
                dc.PicAdd.Click += new EventHandler(btnAdd_Click);
                dc.PicAdd.Name = dc.lbOrder.Text;//设置标志方便传值
                dc.PicAdd.Tag = dishes[j].price;//设置标志方便传值
                dc.Tag = dishes[j].id;//菜名的ID
                dc.Obj = dishes[j];

                if (dishes[j].processes != null)//判断菜的口味是否为空
                {
                    int t = dishes[j].processes.Count();
                    for (int count = 0; count < t; count++)
                    {
                        Button bt = new Button();
                        bt.Text = dishes[j].processes[count].name.ToString();
                        bt.Size = new Size(50, 20);
                        bt.Click += new EventHandler(Doing_Click);
                        bt.BackColor = Color.White;
                        dc.PanelDoding.Controls.Add(bt);
                    }
                }
                if (dishes[j].flavors != null)//判断菜的口味是否为空
                {
                    for (int c = 0; c < dishes[j].flavors.Count(); c++)
                    {
                        Button bt = new Button();
                        bt.Text = dishes[j].flavors[c].name.ToString();
                        bt.Size = new Size(50, 20);
                        bt.Click += new EventHandler(Flavor_Click);
                        dc.PanelFlavor.Controls.Add(bt);
                    }
                }
            }
        }

        //做法的点击事件
        private void Doing_Click(object sender, EventArgs e)
        {
            foreach (Button bt in ((Button)sender).Parent.Controls)
            {
                bt.BackColor = Color.White;
            }
            (sender as Button).BackColor = Color.Red;
        }

        //口味的点击事件
        private void Flavor_Click(object sender, EventArgs e)
        {
            if ((sender as Button).BackColor == Color.Orange)
            {
                (sender as Button).BackColor = Color.White;
            }
            else
            {
                (sender as Button).BackColor = Color.Orange;
            }
        }

        public string Name_Status_Order = "等叫";
        public string Status_Order = "wait";
        public List<Line> lines = new List<Line>();
        //添加菜（加号）
        public void btnAdd_Click(object sender, EventArgs e)
        {
            //string id_order = (sender as PictureBox).Parent.Tag.ToString();
            //count_Add += 1;
            //Price_All += double.Parse((sender as PictureBox).Tag.ToString());
            //this.lbInfor.Text = count_Add.ToString() + "份 共￥" + Convert.ToDouble(Price_All).ToString("0.00");
            //TreeControl.TreeControl tc = new TreeControl.TreeControl();
            //tc.lbName.Text = (sender as PictureBox).Name.ToString();
            //tc.lbPrice.Text = (sender as PictureBox).Tag.ToString();
            //this.panelHistory.Controls.Add(tc);
            //tc.Name = tc.lbPrice.Text;
            //tc.Location = new System.Drawing.Point(0,30*(count_Add-1));
            //tc.button1.Cursor = Cursors.Hand;
            //tc.button2.Cursor = Cursors.Hand;
            //tc.Tag = tc.button1.Tag  = count_Add;
            //tc.button1.Click += new EventHandler(Back_Click);
            //tc.button2.Click += new EventHandler(StatuChange_Click);
            //Line line = new Line();
            //line.number = count_Add;///////////行number////////////
            //line.item = new Item();
            //line.quantity = 1;///////////行quantity////////////
            //line.item.id = id_order;///////////行菜名ID////////////
            //line.action = Status_Order;//////////等叫状态////////////
            //Flavor flavors = new Flavor();
            //if (ID_Flavor_Dishes != null)
            //{
            //    for (int i = 0; i < ID_Flavor_Dishes.Count; i++)
            //    {
            //        flavors.id = ID_Flavor_Dishes[i];//////////菜的口味ID////////////
            //    }
            //}
            //lines.Add(line);


            Item item = (((sender as PictureBox).Parent) as DishControl.DishControl).Obj as Item;

            WpfControls.DishesListView dlv;
            if (dlvTemp.Visible)
            {
                dlv = dlvTemp;
            }
            else
            {
                dlv = dlvTempHistory;
            }

            AddItem(dlv, item);
        }

        private void AddItem(WpfControls.DishesListView p_dlv,Item item)
        {
            if (p_dlv.GetRowCount() == 0)
            {
                List<DishesObj> DishesObjs = new List<DishesObj>();

                DishesObj dishesobj = new DishesObj();
                dishesobj.Item = item;
                dishesobj.Price = float.Parse(item.price);
                dishesobj.Quantity = 1;

                DishesObjs.Add(dishesobj);

                p_dlv.DataSource = DishesObjs;
            }
            else
            {
                DishesObj dishesobj = new DishesObj();
                dishesobj.Item = item;
                dishesobj.Price = float.Parse(item.price);
                dishesobj.Quantity = 1;

                p_dlv.Add(dishesobj);
            }
        }


        //退菜
        public void Back_Click(object sender, EventArgs e)
        {
            //foreach (Control ctl in this.panelHistory.Controls)
            //{
            //    if (ctl is TreeControl.TreeControl && ctl.Tag == (sender as Button).Tag)
            //    {
            //        this.panelHistory.Controls.Remove(ctl);
            //        count_Add -= 1;
            //        double price = double.Parse((sender as Button).Parent.Name.ToString());
            //        Price_All -= price;
            //        //this.lbInfor.Text = count_Add.ToString() + "份 共￥" + Convert.ToDouble(Price_All).ToString("0.00");
            //    }
            //}
        }

        public void StatuChange_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "等叫")
            {
                (sender as Button).Text = "即起";
            }
            else
            { (sender as Button).Text = "等叫"; }
        }

        //窗体打开
        private void Order_Load(object sender, EventArgs e)
        {
            AddCategories();

            LoadFinishedDetailss(m_CurrentConsumptionId);

            LoadTempItems();
        }

        //提交菜单
        private void buttonSend_Click(object sender, EventArgs e)
        {
            //PostOrders po = new PostOrders();
            //int i = 0;
            //Line[] linearr = new Line[lines.Count];
            //if (lines.Count == 0)
            //{
            //    PassValue.MessageInfor = "您还未点任何菜！";
            //    Messagebox mb = new Messagebox();
            //    mb.ShowDialog();
            //}
            //else
            //{
            //    foreach (Line line in lines)
            //    {
            //        linearr[i++] = line;
            //    }
            //    po.lines = linearr;
            //    po.consumption = new Consumption();
            //    po.consumption.id = PassValue.consumptionid;
            //    System.Net.WebHeaderCollection headers = new System.Net.WebHeaderCollection();
            //    headers.Add("Authorization", PassValue.token);
            //    Post.PostHttp(headers, "orders", po);
            //    PassValue.MessageInfor = "点菜已提交成功！";
            //    Messagebox mb = new Messagebox();
            //    mb.ShowDialog();
            //}
        }

        //清空已点菜单
        private void buttonClear_Click(object sender, EventArgs e)
        {
            //this.panelHistory.Controls.Clear();
            count_Add = 0 ;
            Str_Alldishes = "";
            Str_Categories = "";
            //this.lbInfor.Text = "";
            Price_All = 0.00;
            lines.Clear();
        }

        #region   王海

        InputRetrievalDishesPanel m_InputRetrievalDishesPanel = new InputRetrievalDishesPanel();

        //检索菜品
        private List<Item> GetDishes(string p_Condition)
        {
            GetInformation.address = "items?name=" + p_Condition;
            GetInformation gc = new GetInformation();
            string result = gc.GetHTTPInfo();//接收JSON数据

            var jserCategories = new JavaScriptSerializer();
            List<Item> items = jserCategories.Deserialize<List<Item>>(result);//解析json数据
            return items;
        }

        private void CategoriesRetrieval()
        {
            PanelParent.Visible = true;
            PanelChildren.Visible = true;
            PanelDishes.Visible = true;

            if (this.Controls.Contains(m_InputRetrievalDishesPanel))
            {
                m_InputRetrievalDishesPanel.Visible = false;
            }
            m_PanelDishesByRetrievalCode.Visible = false;
        }

        private void DishesRetrieval()
        {
            if (this.Controls.Contains(m_InputRetrievalDishesPanel))
            {

            }
            else
            {
                this.Controls.Add(m_InputRetrievalDishesPanel);
                m_InputRetrievalDishesPanel.Location = new System.Drawing.Point(0, 38);
                m_InputRetrievalDishesPanel.RetrievalDishes += new RetrievalDishesHandler(m_InputRetrievalDishesPanel_RetrievalDishes);

                m_PanelDishesByRetrievalCode.Location = new System.Drawing.Point(0,77);
                m_PanelDishesByRetrievalCode.Size = new Size(this.ClientSize.Width - PanelInfor.Width - 2 - 5 - m_PanelDishesByRetrievalCode.Location.X, this.ClientSize.Height - PanelDishes.Location.Y);
            }

            PanelParent.Visible = false;
            PanelChildren.Visible = false;
            PanelDishes.Visible = false;
            
            m_InputRetrievalDishesPanel.Visible = true;
            TextBox textbox = m_InputRetrievalDishesPanel.GetRetrievalCodeTextBox();
            textbox.SelectionStart = textbox.Text.Length;
            textbox.Focus();
            m_PanelDishesByRetrievalCode.Visible = true;

        }

        private void Btn_Classification_Click(object sender, EventArgs e)
        {
            Btn_Simplecode.BackColor = Color.FromKnownColor(KnownColor.Control); 
            Btn_Classification.BackColor =Color.FromArgb(234, 84, 19);

            CategoriesRetrieval();
        }

        private void Btn_Simplecode_Click(object sender, EventArgs e)
        {
            Btn_Simplecode.BackColor = Color.FromArgb(234, 84, 19);
            Btn_Classification.BackColor  = Color.FromKnownColor(KnownColor.Control);

            DishesRetrieval();
        }

        void m_InputRetrievalDishesPanel_RetrievalDishes(string p_RetrievaCode)
        {
            //PanelDishes.BackColor = Color.Yellow;
            //PanelDishes.Location = new System.Drawing.Point(10, 72);
            //PanelDishes.Size = new Size(this.ClientSize.Width - PanelInfor.Width - PanelDishes.Location.X, this.Height - PanelChildren.Height - PanelChildren.Location.Y - 20);

            if (string.IsNullOrEmpty(p_RetrievaCode))
            {
                this.m_PanelDishesByRetrievalCode.Controls.Clear();
                return;
            }


            List<Item> items = GetDishes(p_RetrievaCode);

            this.m_PanelDishesByRetrievalCode.Controls.Clear();

            m_PanelDishesByRetrievalCode.SuspendLayout();

            List<DishControl.DishControl> TempDishControls = new List<DishControl.DishControl>();

            //构造点菜菜单
            for (int j = 0; j < items.Count(); j++)
            {
                DishControl.DishControl dc = new DishControl.DishControl();
                dc.lbOrder.Text = items[j].name;//显示菜名
                dc.lbPrice.Text = "￥" + items[j].price;//显示菜价
                
                dc.PicAdd.Click += new EventHandler(btnAdd_Click);
                dc.PicAdd.Name = dc.lbOrder.Text;//设置标志方便传值
                dc.PicAdd.Tag = items[j].price;//设置标志方便传值
                dc.Tag = items[j].id;//菜名的ID
                dc.Obj = items[j];

                if (items[j].processes != null)//判断菜的口味是否为空
                {
                    int t = items[j].processes.Count();
                    for (int count = 0; count < t; count++)
                    {
                        Button bt = new Button();
                        bt.Text = items[j].processes[count].name.ToString();
                        bt.Size = new Size(50, 20);
                        bt.Click += new EventHandler(Doing_Click);
                        bt.BackColor = Color.White;
                        dc.PanelDoding.Controls.Add(bt);
                    }
                }
                if (items[j].flavors != null)//判断菜的口味是否为空
                {
                    for (int c = 0; c < items[j].flavors.Count(); c++)
                    {
                        Button bt = new Button();
                        bt.Text = items[j].flavors[c].name.ToString();
                        bt.Size = new Size(50, 20);
                        bt.Click += new EventHandler(Flavor_Click);
                        dc.PanelFlavor.Controls.Add(bt);
                    }
                }

                TempDishControls.Add(dc);
            }

            this.m_PanelDishesByRetrievalCode.Controls.AddRange(TempDishControls.ToArray());

            m_PanelDishesByRetrievalCode.ResumeLayout(true);
        }

        private List<Detail> GetFinishedDetails(string p_CurrentConsumptionId)
        {
            List<Detail> details = new List<Detail>();
            //if (PassValue.consumptionid != null)
            //{
                GetInformation.address = "consumptions/" + p_CurrentConsumptionId;
                GetInformation gc = new GetInformation();
                string result = gc.GetHTTPInfo();//接收JSON数据

                var jserCategories = new JavaScriptSerializer();
                Consumption consumption = jserCategories.Deserialize<Consumption>(result);//解析json数据
                if (consumption.details != null)
                {
                    details.AddRange(consumption.details);
                }
            //}
            return details;
        }


        private void LoadFinishedDetailss(string p_CurrentConsumptionId)
        {
            List<Detail> details = GetFinishedDetails(p_CurrentConsumptionId);

            List<DishesObj> dishesobjs = new List<DishesObj>();
            foreach (Detail detail in details)
            {
                Item item = new Item();
                item.id = detail.item.id;
                item.name = detail.item.name;

               DishesObj resultdishesobj= FindDishesObj(item, dishesobjs);
               if (resultdishesobj == null)
               {
                   DishesObj dishesobj = new DishesObj();
                   dishesobj.Item = item;
                   dishesobj.Price = float.Parse(detail.price);
                   dishesobj.Quantity = float.Parse(detail.quantity);

                   dishesobjs.Add(dishesobj);
               }
               else
               {
                   resultdishesobj.Quantity += float.Parse(detail.quantity);
               }
            }

            if (details.Count == 0)
            {
                eleHistory.Visible = false;
                dlvHistory.Visible = false;

                eleTempHistory.Visible = true;
                dlvTempHistory.Visible = true;

                eleTemp.Visible = false;
                dlvTemp.Visible = false;
            }
            else
            {
                eleHistory.Visible = true;
                dlvHistory.Visible = true;

                eleTempHistory.Visible = false;
                dlvTempHistory.Visible = false;

                eleTemp.Visible = true;
                dlvTemp.Visible = true;
            }

            if (details.Count>0)
            {
                //dlvHistory.DataSource = dishesobjs;
            }
        }

        private void LoadTempItems()
        {
            WpfControls.DishesListView dlv;
            if (dlvTempHistory.Visible)
            {
                dlv = dlvTempHistory;
            }
            else
            {
                dlv = dlvTemp;
            }

            if (dlvTempHistory.Visible == true)
            {
               dlv.DataSource= m_frmCashier.GetdlvTempDataSource();
            }
            else
            {
                dlv.DataSource = m_frmCashier.GetdlvTempDataSource();
            }
        }

        private DishesObj FindDishesObj(Item p_Item, List<DishesObj> p_DishesObjs)
        {
            foreach (DishesObj dishesobj in p_DishesObjs)
            {
                if (dishesobj.Item.id == p_Item.id)
                {
                    return dishesobj;
                }
            }
            return null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            WpfControls.DishesListView dlv;
            if (dlvTempHistory.Visible)
            {
                dlv = dlvTempHistory;
            }
            else
            {
                dlv = dlvTemp;
            }
            m_frmCashier.SetdlvTempDataSource(dlv.GetDataSource());

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        private void Order_SizeChanged(object sender, EventArgs e)
        {
            panelBottom.Location = new System.Drawing.Point(this.ClientSize.Width - panelBottom.Width-2, this.ClientSize.Height - panelBottom.Height);

            PanelInfor.Location = new System.Drawing.Point(this.ClientSize.Width  - PanelInfor.Width-2, 0);
            PanelInfor.Size = new Size(PanelInfor.Width, this.ClientSize.Height - panelBottom.Height);

            PanelParent.Width = PanelChildren.Width =PanelDishes.Width=m_PanelDishesByRetrievalCode.Width=this.Width - PanelInfor.Width - 2 - 5;
        }

        private void PanelInfor_SizeChanged(object sender, EventArgs e)
        {
            int h = (PanelInfor.Height - lblTitle1.Height * 2) / 2;
            lblTitle1.Location = new System.Drawing.Point(0, 0);

            eleHistory.Location = new System.Drawing.Point(0, lblTitle1.Height + lblTitle1.Location.Y);
            eleHistory.Size = new Size(PanelInfor.Width, h);

            eleTempHistory.Location = new System.Drawing.Point(0, lblTitle1.Height + lblTitle1.Location.Y);
            eleTempHistory.Size = new Size(PanelInfor.Width, h);

            lblTitle2.Location = new System.Drawing.Point(0, eleHistory.Location.Y + eleHistory.Height);

            eleTemp.Location = new System.Drawing.Point(0, lblTitle2.Location.Y + lblTitle2.Height);
            eleTemp.Size = new Size(PanelInfor.Width, h);
        }
    }
}
