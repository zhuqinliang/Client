using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Web.Script.Serialization;
//要调用哈希表
using System.Text.RegularExpressions;
using Business;
using Kernel;
using System.Collections;
using System.Runtime.InteropServices;

namespace Client
{
    public partial class Desk : Form,IView
    {
        public Desk()
        {
            InitializeComponent();
            CurrentChooseDesk = new Dictionary<string, string>();
        }

        private HttpAskfor httpReq = new HttpAskfor();


        public string Str_Status;//接受到的桌子状态的JSON格式
        public string Str_consumptionsid;//接受到的消费ID的JSON格式
        public string consumptionsid;//消费ID
        List<int> floors = new List<int>();//楼层集合

        /// <summary>
        /// 记录当前的桌子
        /// </summary>
        private Dictionary<string, string> currentChooseDesk;

        public Dictionary<string, string> CurrentChooseDesk
        {
            get { return currentChooseDesk; }
            set
            {
                currentChooseDesk = value;

                this.dataGridView2.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView2.Rows.Clear();


                foreach (KeyValuePair<string, string> item in CurrentChooseDesk)
                {
                    int index = this.dataGridView2.Rows.Add();
                    this.dataGridView2.Rows[index].Cells[0].Value = item.Value;
                    index++;
                    
                }
                this.lbChooseCount.Text = this.dataGridView2.Rows.Count.ToString();
                this.dataGridView2.ClearSelection();//取消选中
            }
        }

        /// <summary>
        /// 记录当前的楼层
        /// </summary>
        private FloorControl.FloorControl CurrentFloor;

        /// <summary>
        /// 记录上一个是否是空闲桌子
        /// </summary>
        private bool LastIsidle = true;

        #region 窗体的加载
        /// <summary>
        /// 登录窗体的加载事件
        /// </summary>
        private void Desk_Load(object sender, EventArgs e)
        {
            SetUp();
            this.PanelFloor.VerticalScroll.Visible = true;//水平的显示
            AddFloor();//添加楼层按钮
            AddTables();//添加桌子按钮
        }
        #endregion

        #region 楼层按钮的构造
        /// <summary>
        /// 楼层按钮的构造
        /// </summary>
        public void AddFloor()
        {
            for (int i = 0; i < PassValue.tablesstatuslist.Count(); i++)
            {
                FloorControl.FloorControl fc = new FloorControl.FloorControl();//实例化新建楼层
                fc.Tag = PassValue.tablesstatuslist[i];//给楼层控件的tag赋个值
                fc.Cursor = Cursors.Hand;//悬停效果为手型按钮
                fc.lbFloor.Tag = PassValue.tablesstatuslist[i];//给楼层控件里的label的tag也赋个值
                fc.lbFloor.Text = PassValue.tablesstatuslist[i].ToString();//楼层控件Label赋值(楼层显示的名字)
                this.PanelFloor.Controls.Add(fc);//添加到容器里面
                if (i == 0)//初始化，指针给第一个楼层按钮
                {
                    fc.picFloor.Image = Properties.Resources.楼层选中;//改变楼层按钮背景色
                    fc.lbFloor.ForeColor = Color.White;//改变字体颜色
                    PassValue.ChooseFloor = fc.Tag.ToString();//将楼层的tag作为标识传到公共变量里存储
                    this.picTriangle.Top = 40;//楼层向导小三角的初始位置控制
                }
                fc.Left = (this.PanelFloor.Width - fc.Width) / 2;//楼层按钮在容器中居中
                fc.Top = 30 + 60 * i;//控制楼层按钮的上下间距
                fc.MouseDown += new MouseEventHandler(btnfloor_MouseDown);//定义楼层点击事件
            }
        }
        #endregion

        #region 桌子按钮的构造
        /// <summary>
        /// 添加桌子按钮
        /// </summary>
        public void AddTables()
        {
            int n = Screen.PrimaryScreen.Bounds.Width - this.panelstatus.Width - this.panelInformation.Width;//总宽度
            DeskControl.DeskControl dcdc2 = new DeskControl.DeskControl();
            int w = dcdc2.Width;//控件宽度
            int h = dcdc2.Height;//控件高度
            int item = w / 5;//控件间距
            int item2 = h / 5;
            int t = (n - item) / (w + item);//控件个数
            int y = n - (item + w) * t;//右边间距

            //获取当前的桌子状态
            var personsStatus = httpReq.HttpGet<List<Status>>("statuses");

            //先清空所有缓存
            PassValue.ht.Clear();
            PassValue.roomnames.Clear(); PassValue.tablenames.Clear(); PassValue.tableides.Clear();

            var personsTable = PassValue.Tables;
            for (int i = 0; i < personsTable.Count(); i++)
            {
                if (personsTable[i].floor.number == PassValue.ChooseFloor)//判断所属楼层是否等于点击的楼层
                {
                    PassValue.roomnames.Add(personsTable[i].room.name);//将此楼层里的房间名全部存储到roomname里面去
                    PassValue.tablenames.Add(personsTable[i].name);//将此楼层里的桌名全部存储到tablename里面去
                    PassValue.tableides.Add(personsTable[i].id);//将此楼层里的桌子ID全部存储到tableid里面去
                }
            }
            for (int j = 0; j < PassValue.tableides.Count(); j++)
            {
                DeskControl.DeskControl dc = new DeskControl.DeskControl();//开始添加桌子
                dc.Location = new System.Drawing.Point(item + (y - item) / 2 + (item + w) * (j % t), item2 * 2 + (item2 + h) * (j / t));//桌子控件坐标位置[动态坐标]

                this.panelDesk.Controls.Add(dc);//tab里面添加构造好的桌子
                dc.lbName.Text = PassValue.tablenames[j].ToString();//桌子名
                dc.lbNumber.Text = PassValue.roomnames[j].ToString();//包间名
                dc.lbTableID.Text = PassValue.tableides[j].ToString();//桌子ID

                if (personsStatus != null)
                {
                    for (int c = 0; c < personsStatus.Count; c++)
                    {
                        //构造桌子People和颜色信息
                        if (personsStatus[c].table.id == PassValue.tableides[j])
                        {
                            switch (personsStatus[c].state)
                            {
                                case "idle":
                                    dc.lbPeople.Text = "人数：0/" + personsStatus[c].table.size.ToString();
                                    dc.lbStatus.Text = "idle";
                                    foreach (Control ctl in this.panelDesk.Controls)
                                    {
                                        if (ctl is DeskControl.DeskControl)
                                        {
                                            DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                                            if (PassValue.selectedtableid.Contains(dcdc.lbTableID.Text))
                                            {
                                                dcdc.picCheck.Visible = true;
                                            }
                                        }
                                    }
                                    break;
                                case "ordering":
                                    dc.lbPeople.Text = "人数：" + personsStatus[c].consumption.people + "/" + personsStatus[c].table.size.ToString();
                                    dc.picDesk.Image = Properties.Resources.ordering;
                                    dc.lbStatus.Text = "ordering";
                                    break;
                                case "dining":
                                    dc.lbPeople.Text = personsStatus[c].consumption.people + "/" + personsStatus[c].table.size.ToString() + "  ¥" + personsStatus[c].consumption.subtotal;
                                    dc.picDesk.Image = Properties.Resources.dining;
                                    dc.lbStatus.Text = "dining";
                                    break;
                                case "reserved":
                                    dc.lbPeople.Text = "人数：" + personsStatus[c].booking.people + "/" + personsStatus[c].table.size.ToString();
                                    dc.picDesk.Image = Properties.Resources.reserved;
                                    dc.lbStatus.Text = "reserved";
                                    break;
                            }
                            //消费ID的构造
                            if (personsStatus[c].consumption == null)
                            {
                                dc.lbConsumption.Text = "";
                                dc.Tag = "";
                            }
                            else
                            {
                                dc.lbConsumption.Text = personsStatus[c].consumption.id;
                                dc.Tag = dc.lbConsumption.Text;
                            }
                            PassValue.ht.Add(dc.lbTableID.Text, personsStatus[c].state);//哈希表(桌号名,状态)
                        }
                    }
                    //桌子的点击触发事件
                    dc.MouseDown += new MouseEventHandler(btntalbe_MouseDown);
                }
            }

            if (string.IsNullOrEmpty(PassValue.consumptionid))
            {
                this.dataGridView1.Rows.Clear();
            }
        }
        #endregion

        #region 小三角的位置切换
        /// <summary>
        /// 小三角的位置切换
        /// </summary>
        public void changefloor()
        {
            int h_c = 0;//小三角高度初始值
            foreach (Control ctl in this.PanelFloor.Controls)//遍历整个楼层容器控件
            {
                if (ctl is FloorControl.FloorControl && ctl.Tag.ToString() == PassValue.ChooseFloor)
                {
                    h_c = ctl.Top;//楼层按钮的高度赋值
                }
            }
            this.picTriangle.Top = h_c + 10;//小三角高度的设置
        }
        #endregion

        #region 点击楼层
        /// <summary>
        /// 楼层的切换
        /// </summary>
        public void btnfloor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PassValue.ht.Clear();//清空哈希表
                if (sender is FloorControl.FloorControl)//判断是否点击到楼层按钮
                {
                    FloorControl.FloorControl fc = (FloorControl.FloorControl)sender;
                    PassValue.ChooseFloor = fc.Tag.ToString();
                    foreach (Control ctl in this.PanelFloor.Controls)
                    {
                        if (ctl is FloorControl.FloorControl == false)
                        {
                            continue;
                        }

                        FloorControl.FloorControl fcc = ctl as FloorControl.FloorControl;
                        if (ctl.Tag.ToString() == PassValue.ChooseFloor)
                        {
                            fcc.picFloor.Image = Properties.Resources.楼层选中;//选中按钮变成橘黄色
                            fcc.lbFloor.ForeColor = Color.White;
                        }
                        else
                        {
                            fcc.picFloor.Image = Properties.Resources.楼层未选中;//未选中按钮的颜色变成灰色
                            fcc.lbFloor.ForeColor = Color.Black;
                        }
                    }
                }
                changefloor();//小三角的位置的变化
                this.panelDesk.Controls.Clear();//清空tab里面的内容准备重新加载
                AddTables();//添加桌子

                //选中之前选择的桌子
                ChooseCurrent();
            }
        }
        #endregion

        #region 点击桌子
        /// <summary>
        /// 桌子的点击事件
        /// </summary>
        public void btntalbe_MouseDown(object sender, MouseEventArgs e)
        {
            DeskControl.DeskControl dc = (DeskControl.DeskControl)sender;

            consumptionsid = dc.lbConsumption.Text;
            PassValue.Tableid = "";//先清空
            PassValue.Tablename = "";//先清空
            PassValue.Tableid = dc.lbTableID.Text;

            var personsConsumption = httpReq.HttpGet<Consumption>(string.Format("consumptions/{0}", consumptionsid));

            if (e.Button == MouseButtons.Left)
            {
                if (personsConsumption != null)//接受到的不为空
                {
                    string time_start = DateTime.Parse(personsConsumption.timestamp).ToString("MM-dd hh:mm");//解析最后一次更新的时间格式
                    this.lbTime.Text = time_start.ToString();//将时间赋值到右侧的信息栏里面去
                }
                else//判断接受到的数据为空的情况
                {
                    this.lbTime.Text = "";//右侧信息栏里面的时间为空
                }
                string value = (string)PassValue.ht[dc.lbTableID.Text];//从哈希表里面提取出房间号对应的桌子状态信息
                //判断是否被选中
                if (dc.picCheck.Visible == false)
                {
                    if (value == "idle")//空桌
                    {
                        this.dataGridView1.Rows.Clear();//清空数据表缓存
                        PassValue.consumptionid = "";
                        foreach (Control ctl in this.panelDesk.Controls)
                        {
                            if (ctl is DeskControl.DeskControl)
                            {
                                DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                                if (dcdc.lbConsumption.Text != "")
                                {
                                    dcdc.picCheck.Visible = false;
                                }
                            }
                        }
                        dc.picCheck.Visible = true;//显示被选中
                        PassValue.count_select_idle += 1;
                        //保存选中的桌子
                        if (LastIsidle == false)
                        {
                            CurrentChooseDesk.Clear();
                            CurrentChooseDesk = CurrentChooseDesk;
                        }
                        LastIsidle = true;

                        if (CurrentChooseDesk.Keys.Contains(dc.lbTableID.Text) == false)
                        {
                            CurrentChooseDesk.Add(dc.lbTableID.Text, string.Format("{0}({1})", dc.lbName.Text, dc.lbNumber.Text));
                            CurrentChooseDesk = CurrentChooseDesk;
                        }

                        this.lbInfor.Text = dc.lbName.Text.ToString() + "(" + dc.lbNumber.Text.ToString() + ")[空闲]";//餐台名称
                        this.lbTime.Text = "";//开台时间
                        //人数
                        Match m = Regex.Match(dc.lbPeople.Text, @"：([\s\S]*?)/");
                        if (m.Success)
                        {
                            this.lbPeople.Text = m.Result("$1").ToString() + "人";
                        }
                        this.lbMoney.Text = "";//消费金额为空
                        PassValue.selectedtableid.Add(dc.lbTableID.Text);//集合中添加桌子ID(桌子的唯一性)
                    }
                    else if (value == "ordering")//刚开台正在点菜
                    {
                        LastIsidle = false;
                        PassValue.count_select_ordering = 0;
                        PassValue.count_select_idle = 0;
                        this.dataGridView1.Rows.Clear();//清空数据表缓存
                        consumptionsid = dc.lbConsumption.Text;
                        if (dc.picCheck.Visible == false)//没有选中
                        {
                            CurrentChooseDesk.Clear();
                            CurrentChooseDesk = new Dictionary<string, string>();
                            foreach (Control ctl in this.panelDesk.Controls)
                            {
                                if (ctl is DeskControl.DeskControl)
                                {
                                    DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                                    dcdc.picCheck.Visible = false;
                                    PassValue.selectedtableid.Clear();//重新初始化
                                    if (ctl.Tag.ToString() == consumptionsid && ctl.Tag != null)
                                    {
                                        dcdc.picCheck.Visible = true;
                                        PassValue.count_select_ordering += 1;
                                        if (CurrentChooseDesk.Keys.Contains(dcdc.lbTableID.Text) == false)
                                        {
                                            CurrentChooseDesk.Add(dcdc.lbTableID.Text, string.Format("{0}({1})", dcdc.lbName.Text, dcdc.lbNumber.Text));
                                            CurrentChooseDesk = CurrentChooseDesk;
                                        }
                                    }
                                }
                            }
                        }
                        this.lbInfor.Text = dc.lbName.Text.ToString() + "(" + dc.lbNumber.Text.ToString() + ")[正在点菜]";//餐台名称
                        //人数
                        Match m = Regex.Match(dc.lbPeople.Text, @"：([\s\S]*?)/");
                        if (m.Success)
                        {
                            this.lbPeople.Text = m.Result("$1").ToString() + "人";
                        }
                        this.lbMoney.Text = "";
                    }
                    else if (value == "dining")//已消费
                    {
                        LastIsidle = false;
                        PassValue.count_select_idle = 0;
                        if (dc.picCheck.Visible == false)//没有选中
                        {
                            CurrentChooseDesk.Clear();
                            CurrentChooseDesk = new Dictionary<string, string>();
                            foreach (Control ctl in this.panelDesk.Controls)
                            {
                                if (ctl is DeskControl.DeskControl)
                                {
                                    DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                                    dcdc.picCheck.Visible = false;
                                    PassValue.selectedtableid.Clear();//重新初始化
                                    if (ctl.Tag.ToString() == consumptionsid && ctl.Tag != null)
                                    {
                                        dcdc.picCheck.Visible = true;
                                        if (CurrentChooseDesk.Keys.Contains(dcdc.lbTableID.Text) == false)
                                        {
                                            CurrentChooseDesk.Add(dcdc.lbTableID.Text, string.Format("{0}({1})", dcdc.lbName.Text, dcdc.lbNumber.Text));
                                            CurrentChooseDesk = CurrentChooseDesk;
                                        }
                                    }
                                }
                            }
                            CurrentChooseDesk = CurrentChooseDesk;
                        }
                        this.lbInfor.Text = dc.lbName.Text.ToString() + "(" + dc.lbNumber.Text.ToString() + ")[就餐]";//餐台名称
                        //人数
                        int strpeople = dc.lbPeople.Text.IndexOf("/");
                        this.lbPeople.Text = dc.lbPeople.Text.Substring(0, strpeople).ToString() + "人";
                        int strmoney = dc.lbPeople.Text.IndexOf("¥");
                        this.lbMoney.Text = dc.lbPeople.Text.Substring(strmoney, dc.lbPeople.Text.Length - strmoney).ToString();
                        //对datagridviewx的设计
                        this.dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dataGridView1.Rows.Clear();//清空数据表缓存

                        if (personsConsumption != null)
                        {
                            var lds = from item in personsConsumption.details
                                      group item by new { item.item.name, item.price } into g
                                      select new { name = g.Key.name, price = g.Key.price, quantity = g.Sum(p => Convert.ToDecimal(p.quantity)) };

                            foreach (var item in lds)
                            {
                                if (item.quantity > 0)
                                {
                                    int index = this.dataGridView1.Rows.Add();
                                    this.dataGridView1.Rows[index].Cells[0].Value = item.name;
                                    this.dataGridView1.Rows[index].Cells[1].Value = item.quantity;
                                    this.dataGridView1.Rows[index].Cells[2].Value = (decimal.Parse(item.price) * item.quantity).ToString("0.00");
                                }
                            }
                            this.dataGridView1.ClearSelection();//取消选中
                        }
                    }
                    this.lbOthers.Text = "";
                    PassValue.consumptionid = dc.lbConsumption.Text;//将消费ID传值给PassValue里面存储起来
                }
                else//已经被选中的情况下
                {
                    //只有桌子的状态在闲置的情况下才能把选中的情况减去一个
                    if (dc.lbStatus.Text == "idle")
                    {
                        LastIsidle = true;

                        PassValue.count_select_idle -= 1;
                        dc.picCheck.Visible = false;

                        //移除解除选中的桌子
                        if (CurrentChooseDesk.Keys.Contains(dc.lbTableID.Text))
                        {
                            CurrentChooseDesk.Remove(dc.lbTableID.Text);
                            CurrentChooseDesk = CurrentChooseDesk;
                        }
                    }
                    else
                    {
                        LastIsidle = false;
                        foreach (Control ctl in this.panelDesk.Controls)
                        {
                            if (ctl is DeskControl.DeskControl)
                            {
                                DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                                dcdc.picCheck.Visible = false;
                                PassValue.selectedtableid.Clear();//重新初始化
                                if (ctl.Tag.ToString() == consumptionsid && ctl.Tag != null)
                                {
                                    dcdc.picCheck.Visible = false;
                                    PassValue.count_select_ordering -= 1;
                                    //移除解除选中的桌子
                                    if (CurrentChooseDesk.Keys.Contains(dcdc.lbTableID.Text))
                                    {
                                        CurrentChooseDesk.Remove(dcdc.lbTableID.Text);
                                        CurrentChooseDesk = CurrentChooseDesk;
                                    }
                                }
                            }
                        }
                    }

                    this.lbInfor.Text = "";
                    this.lbPeople.Text = "";
                    this.lbMoney.Text = "";
                    this.lbOthers.Text = "";
                    this.lbTime.Text = "";//开台时间
                    this.dataGridView1.Rows.Clear();
                    //人数
                    Match m = Regex.Match(dc.lbPeople.Text, @"：([\s\S]*?)/");
                    if (m.Success)
                    {
                        this.lbPeople.Text = m.Result("$1").ToString() + "人";
                    }
                    this.lbMoney.Text = "";//添加消费金额
                    PassValue.selectedtableid.Remove(dc.lbTableID.Text);//集合中删除桌子ID(桌子的唯一性)
                    PassValue.consumptionid = "";
                }
            }
            else if (e.Button == MouseButtons.Right)//鼠标右击菜单
            {
                DeskControl.DeskControl dcdc = (DeskControl.DeskControl)sender;
                if (dcdc.picCheck.Visible == true)
                {
                    this.contextMenuStripTables.Show(MousePosition);//显示右键菜单
                    PassValue.consumptionid = dc.lbConsumption.Text;
                    //桌子右键菜单的可用性编辑
                    if (dc.lbStatus.Text == "idle")//空闲状态
                    {
                        this.toolStripMenuItem1.Enabled = true;//开台
                        this.toolStripMenuItem2.Enabled = false;//点菜
                        this.toolStripMenuItem3.Enabled = true;//预定
                        this.toolStripMenuItem4.Enabled = false;//清台
                        this.toolStripMenuItem5.Enabled = false;//合并
                    }
                    else if (dc.lbStatus.Text == "ordering")//正在点菜
                    {
                        this.toolStripMenuItem1.Enabled = false;
                        this.toolStripMenuItem2.Enabled = true;
                        this.toolStripMenuItem3.Enabled = false;
                        this.toolStripMenuItem4.Enabled = true;
                        this.toolStripMenuItem5.Enabled = false;
                    }
                    else if (dc.lbStatus.Text == "dining")
                    {
                        this.toolStripMenuItem1.Enabled = false;
                        this.toolStripMenuItem2.Enabled = true;
                        this.toolStripMenuItem3.Enabled = false;
                        this.toolStripMenuItem4.Enabled = false;
                        this.toolStripMenuItem5.Enabled = true;
                    }
                    if (personsConsumption != null && personsConsumption.merge != null)
                    {
                        this.toolStripMenuItem6.Enabled = true;//拆分
                    }
                    else
                    {
                        this.toolStripMenuItem6.Enabled = false;
                    }

                    //获取当前的桌子状态
                    var personsStatus = httpReq.HttpGet<List<Status>>("statuses");


                    if ((dc.lbStatus.Text == "dining" || dc.lbStatus.Text == "ordering") && personsConsumption.merge == null)
                    {
                        this.toolStripMenuItem8.Enabled = true;//改台
                    }
                    else
                    {
                        this.toolStripMenuItem8.Enabled = false;//改台
                    }

                    if (string.IsNullOrEmpty(PassValue.consumptionid))
                    {
                        this.修改人数ToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        this.修改人数ToolStripMenuItem.Enabled = true;
                    }
                }
            }

            ChooseDesk();
        }
        #endregion

        #region 窗体的缓冲
        /// <summary>
        /// 开启双缓冲
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion

        #region 开台
        /// <summary>
        /// 单独开台操作
        /// </summary>
        private void BtnOpenTables_Click(object sender, EventArgs e)
        {

            PassValue.count_select_idle = 0;
            PassValue.selectedtableid.Clear();

            if (CurrentChooseDesk.Count > 0)
            {
                foreach (Control ct in this.panelDesk.Controls)
                {
                    if (ct is DeskControl.DeskControl)
                    {
                        DeskControl.DeskControl dc = (DeskControl.DeskControl)ct;
                        if (CurrentChooseDesk.Keys.Contains(dc.lbTableID.Text) && dc.lbStatus.Text == "idle")
                        {
                            PassValue.count_select_idle = CurrentChooseDesk.Count;
                            PassValue.selectedtableid.Add(dc.lbTableID.Text);
                        }
                    }
                }
            }

            if (PassValue.count_select_idle > 0)
            {
                if (string.IsNullOrEmpty(PassValue.consumptionid))
                {
                    PassValue.desk = PassValue.selectedtableid.ToArray();//将集合转换为string数组
                    OpenTables ot = new OpenTables();
                    ot.Owner = this;
                    ot.ShowDialog();
                }
                else
                {
                    Messagebox mb = new Messagebox();
                    PassValue.MessageInfor = "请选择空桌！";
                    mb.Show();
                }
            }
            else
            {
                Messagebox mb = new Messagebox();
                PassValue.MessageInfor = "请选择空桌！";
                mb.Show();
            }

            //选中默认
            ChooseCurrent();
        }

        /// <summary>
        /// 组合开台操作
        /// </summary>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PassValue.consumptionid))
            {
                Messagebox mb = new Messagebox();
                PassValue.MessageInfor = "请选择空桌！";
                mb.Show();
                return;
            }

            LastIsidle = false;
            PassValue.selectedtableid.Clear();

            PassValue.count_select_idle = 0;

            foreach (KeyValuePair<string, string> item in CurrentChooseDesk)
            {
                PassValue.selectedtableid.Add(item.Key);
            }
            PassValue.count_select_idle = CurrentChooseDesk.Count;

            if (PassValue.count_select_idle > 0)
            {
                PassValue.desk = PassValue.selectedtableid.ToArray();//将集合转换为string数组
                OpenTables ot = new OpenTables();
                ot.Owner = this;
                if (ot.ShowDialog() == DialogResult.Cancel)
                {
                    LastIsidle = true;
                }
            }
            else
            {
                Messagebox mb = new Messagebox();
                PassValue.MessageInfor = "请先选择桌子！";
                mb.Show();
            }

            //选中默认
            ChooseCurrent();

        }
        #endregion

        #region 刷新
        /// <summary>
        /// 刷新方法
        /// </summary>
        public void Refresh_Method()
        {
            PassValue.selectedtableid.Clear();//清空集合方便一下次存储值
            this.panelDesk.Controls.Clear();
            PassValue.ht.Clear();
            AddTables();

            ChooseCurrent();
        }
        #endregion

        #region 清台
        /// <summary>
        /// 右键清台操作
        /// </summary>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (PassValue.count_select_ordering == 1)//单张桌子的消台
            {
                HttpResult hr = httpReq.HttpDelete(string.Format("consumptions/{0}", PassValue.consumptionid));
                if ((int)hr.StatusCode == 0)
                {
                    MessageBox.Show(string.Format("{0}{1}", hr.StatusDescription, hr.OtherDescription), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else if ((int)hr.StatusCode == 409)
                {
                    MessageBox.Show("该桌子已被操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                PassValue.selectedtableid.Clear();//清空集合方便一下次存储值
                this.panelDesk.Controls.Clear();
                PassValue.ht.Clear();//哈希表清空
                PassValue.count_select_ordering = 0;//被选中的桌子清空
                AddTables();//刷新
                CurrentChooseDesk.Clear();
                CurrentChooseDesk = new Dictionary<string, string>();
                PassValue.consumptionid = "";
                this.lbInfor.Text = this.lbInfor.Text.Split('[')[0] + "[空桌]";//餐台名称
                //人数
                this.lbPeople.Text = "0人";
            }
            else if (PassValue.count_select_ordering > 1)//多张桌子的消台
            {
                RemoveOrderingDesks rod = new RemoveOrderingDesks();
                rod.Owner = this;
                rod.ShowDialog();
            }
            else//其他情况
            {
                MessageBox.Show("请选择正在点菜的桌子", "天天100系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 结账
        /// <summary>
        /// 结账按钮
        /// </summary>
        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            bool choosedesk = false;
            foreach (Control ctl in this.panelDesk.Controls)
            {
                if (ctl is DeskControl.DeskControl)
                {
                    DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                    if (dcdc.lbStatus.Text == "dining" && dcdc.picCheck.Visible == true)
                    {
                        choosedesk = true;
                    }
                }
            }
            if (choosedesk)
            {
                Member mb = new Member(PassValue.consumptionid);
                mb.Owner = this;
                mb.ShowDialog();
            }
        }
        #endregion

        #region 点菜
        /// <summary>
        /// 右击菜单里的点菜事件
        /// </summary>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("点菜");
        }

        /// <summary>
        /// 按钮点菜操作
        /// </summary>
        private void BtnOrder_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("点菜");
        }

        /// <summary>
        /// 点菜方法
        /// </summary>
        public void order()
        {
            Main m;
            m = (Main)this.Owner;
            m.btnorders();
        }
        #endregion

        #region 合并
        /// <summary>
        /// 右键点击合并
        /// </summary>
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ListJoin lj = new ListJoin();
            lj.Owner = this;
            lj.ShowDialog();
        }
        #endregion

        #region 拆单
        /// <summary>
        /// 右键点击拆单
        /// </summary>
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            RemoveList rl = new RemoveList();
            rl.Owner = this;
            rl.ShowDialog();
        }
        #endregion

        #region 改台
        /// <summary>
        /// 右键点击改台
        /// </summary>
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            //获取当前的桌子状态
            var personsStatus = httpReq.HttpGet<List<Status>>("statuses");
            int people = 0;
            if (personsStatus != null)
            {
                foreach (KeyValuePair<string, string> tableid in CurrentChooseDesk)
                {
                    Status s = personsStatus.Where(p => p.table.id == tableid.Key).FirstOrDefault();
                    if (s != null)
                    {
                        people = s.consumption.people;
                        PassValue.Tablename += " " + s.table.name;
                    }
                }
            }
            ChangeTable ct = new ChangeTable(people);
            ct.Owner = this;
            ct.ShowDialog();
        }
        #endregion

        #region 隐藏横向滚动条
        /// <summary>
        /// 隐藏横向滚动条
        /// </summary>
        private void PanelFloor_Paint(object sender, PaintEventArgs e)
        {
            Control _Control = (Control)sender;
            ShowScrollBar(_Control.Handle, 0, 0);
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);
        #endregion

        #region 预定
        /// <summary>
        /// 预定
        /// </summary>
        private void BtnYd_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("预定");
        }
        #endregion

        #region 重写右键行切换背景色
        /// <summary>
        /// 从ProfessionalColorTable派生一个自己的颜色表，按需重写其成员（都是只读属性）
        /// ToolStripProfessionalRenderer使用一个颜色表（ProfessionalColorTable）来保存它需要用到的颜色。
        /// 只要从这个ProfessionalColorTable派生自己的颜色表并使用这个自己的颜色表就可以更改render使用的颜色了。
        /// </summary>
        public class MyColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(255, 221, 183); }
            }
            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(255, 221, 183); }
            }
        }
        /// <summary>
        /// 快捷信息栏鼠标悬停和离开事件
        /// </summary>
        private void contextMenuStripTables_MouseMove(object sender, MouseEventArgs e)
        {
            this.contextMenuStripTables.Renderer = new ToolStripProfessionalRenderer(new MyColorTable());
        }
        #endregion

        #region 右下角按钮事件
        /// <summary>
        /// 开台
        /// </summary>
        private void BtnOpenTables_MouseMove(object sender, MouseEventArgs e)
        {
            this.BtnOpenTables.Image = Properties.Resources.开台12;
        }

        private void BtnOpenTables_MouseLeave(object sender, EventArgs e)
        {
            this.BtnOpenTables.Image = Properties.Resources.开台11;
        }

        /// <summary>
        /// 点菜
        /// </summary>
        private void BtnOrder_MouseMove(object sender, MouseEventArgs e)
        {
            this.BtnOrder.Image = Properties.Resources.点菜12;
        }

        private void BtnOrder_MouseLeave(object sender, EventArgs e)
        {
            this.BtnOrder.Image = Properties.Resources.点菜11;
        }

        /// <summary>
        /// 预定
        /// </summary>
        private void BtnYd_MouseMove(object sender, MouseEventArgs e)
        {
            this.BtnYd.Image = Properties.Resources.预定12;
        }

        private void BtnYd_MouseLeave(object sender, EventArgs e)
        {
            this.BtnYd.Image = Properties.Resources.预定11;
        }

        /// <summary>
        /// 结账
        /// </summary>
        private void BtnCheckout_MouseMove(object sender, MouseEventArgs e)
        {
            this.BtnCheckout.Image = Properties.Resources.结账12;
        }

        private void BtnCheckout_MouseLeave(object sender, EventArgs e)
        {
            this.BtnCheckout.Image = Properties.Resources.结账11;
        }

        /// <summary>
        /// 初始化图标
        /// </summary>
        public void SetUp()
        {
            this.BtnOpenTables.Image = Properties.Resources.开台11;//开台
            this.BtnOrder.Image = Properties.Resources.点菜11;//点菜
            this.BtnYd.Image = Properties.Resources.预定11;//预定
            this.BtnCheckout.Image = Properties.Resources.结账11;//结账
        }
        #endregion

        /// <summary>
        /// 点菜刷新
        /// </summary>
        public void Active()
        {
            //刷新
            btnfloor_MouseDown(CurrentFloor, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
            //选中默认
            ChooseCurrent();
        }

        /// <summary>
        /// 选中默认
        /// </summary>
        public void ChooseCurrent()
        {
            CurrentChooseDesk = CurrentChooseDesk;
            if (CurrentChooseDesk.Count == 0)
            {
                return;
            }

            PassValue.Tableid = CurrentChooseDesk.First().Key.ToString();

            Dictionary<string, string> al = CurrentChooseDesk;

            //最后一次点击的桌子
            DeskControl.DeskControl lastChoose = null;

            //根据桌子ID选择选择桌子
            foreach (Control ct in this.panelDesk.Controls)
            {
                if (ct is DeskControl.DeskControl)
                {
                    DeskControl.DeskControl dc = (DeskControl.DeskControl)ct;
                    if (al.Keys.Contains(dc.lbTableID.Text))
                    {
                        dc.picCheck.Visible = true;
                        consumptionsid = dc.lbConsumption.Text;
                        PassValue.consumptionid = consumptionsid;

                    }

                    if (dc.lbTableID.Text == PassValue.Tableid)
                    {
                        lastChoose = dc;
                    }
                }
            }

            if (lastChoose != null)
            {
                string state = "空闲";
                switch (lastChoose.lbStatus.Text)
                {
                    case "idle":
                        state = "空闲";
                        this.lbTime.Text = "";
                        this.lbMoney.Text = "0.00";
                        this.lbPeople.Text = "0人";
                        break;
                    case "ordering":
                        state = "正在点菜";
                        break;
                    case "dining":
                        state = "用餐中";
                        break;

                }
                this.lbInfor.Text = string.Format("{0}({1})[{2}]", lastChoose.lbNumber.Text, lastChoose.lbName.Text, state);
            }
            else
            {
                this.lbInfor.Text = "";
                this.lbPeople.Text = "0人";
                this.lbMoney.Text = "0.00";
                this.lbTime.Text = "";
            }

            var personsConsumption = httpReq.HttpGet<Consumption>(string.Format("consumptions/{0}", consumptionsid));

            //对datagridviewx的设计
            this.dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Rows.Clear();//清空数据表缓存

            if (personsConsumption == null)
            {
                //改变按钮状态
                ChooseDesk();
                return;
            }

            if (personsConsumption.tables.Count() > 0)
            {
                this.lbInfor.Text = personsConsumption.tables[0].number;
            }
            this.lbPeople.Text = personsConsumption.people.ToString() + "人";
            this.lbMoney.Text = personsConsumption.total;
            this.lbTime.Text = personsConsumption.timestamp.Replace('T', ' ');

            var lds = from item in personsConsumption.details
                      group item by new { item.item.name, item.price } into g
                      select new { name = g.Key.name, price = g.Key.price, quantity = g.Sum(p => Convert.ToDecimal(p.quantity)) };

            foreach (var item in lds)
            {
                if (item.quantity > 0)
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = item.name;
                    this.dataGridView1.Rows[index].Cells[1].Value = item.quantity;
                    this.dataGridView1.Rows[index].Cells[2].Value = (decimal.Parse(item.price) * item.quantity).ToString("0.00");
                }
            }

            //如果有消费ID的话 根据消费ID来选中桌子
            if (!string.IsNullOrEmpty(PassValue.consumptionid))
            {
                PassValue.count_select_idle = 0;
                foreach (Control ct in this.panelDesk.Controls)
                {
                    if (ct is DeskControl.DeskControl)
                    {
                        DeskControl.DeskControl dc = (DeskControl.DeskControl)ct;
                        if (PassValue.consumptionid == dc.lbConsumption.Text)
                        {
                            dc.picCheck.Visible = true;
                            if (CurrentChooseDesk.Keys.Contains(dc.lbTableID.Text) == false)
                            {
                                CurrentChooseDesk.Add(dc.lbTableID.Text, string.Format("{0}({1})", dc.lbName.Text, dc.lbNumber.Text));
                            }
                        }
                    }
                }
                CurrentChooseDesk = CurrentChooseDesk;
            }
            else //如果没有消费ID的话 就是统计空闲桌子的数量
            {
                PassValue.count_select_idle = CurrentChooseDesk.Count;
            }

            ChooseDesk();
        }

        public string GetName()
        {
            return "开桌";
        }

        private void 修改人数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PassValue.consumptionid))
            {
                ChangeNumber cn = new ChangeNumber(PassValue.consumptionid);
                cn.Owner = this;
                cn.ShowDialog();
            }
        }

        public void ChooseDesk()
        {
            string status = "";
            foreach (Control ctl in this.panelDesk.Controls)
            {
                if (ctl is DeskControl.DeskControl)
                {
                    DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                    if (dcdc.picCheck.Visible == true)
                    {
                        status = dcdc.lbStatus.Text;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(status))
            {
                this.BtnOpenTables.Enabled = this.BtnOrder.Enabled = this.BtnCheckout.Enabled = false;//先初始化
                this.BtnOpenTables.Image = Properties.Resources.开台13;//不可以开台
                this.BtnOrder.Image = Properties.Resources.点菜13;//不可以点菜
                this.BtnCheckout.Image = Properties.Resources.结账13;//不可以结账
            }
            else
            {
                this.BtnOpenTables.Enabled = this.BtnOrder.Enabled = this.BtnCheckout.Enabled = true;//先初始化
                switch (status)
                {
                    case "idle":
                        this.BtnOpenTables.Image = Properties.Resources.开台11;//可以开台
                        this.BtnOrder.Image = Properties.Resources.点菜13;//不可以点菜
                        this.BtnCheckout.Image = Properties.Resources.结账13;//不可以结账
                        this.BtnOrder.Enabled = this.BtnCheckout.Enabled = false;//设置是否可用
                        break;
                    case "ordering":
                        this.BtnOpenTables.Image = Properties.Resources.开台13;//不可以开台
                        this.BtnOrder.Image = Properties.Resources.点菜11;//可以点菜
                        this.BtnCheckout.Image = Properties.Resources.结账13;//不可以结账

                        this.BtnOpenTables.Enabled = this.BtnCheckout.Enabled = false;//设置是否可用
                        break;
                    case "dining":
                        this.BtnOpenTables.Image = Properties.Resources.开台13;//不可以开台
                        this.BtnOrder.Image = Properties.Resources.点菜11;//可以点菜
                        this.BtnCheckout.Image = Properties.Resources.结账11;//可以结账
                        this.BtnOpenTables.Enabled = false;//设置是否可用
                        break;
                }
            }
        }

        private void Desk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.B)
            {
                if (this.BtnCheckout.Enabled == false)
                {
                    return;
                }
                BtnCheckout_Click(sender, e);
            }
            //else if (e.KeyCode == Keys.O)
            //{
            //    toolStripMenuItem1_Click(sender, e);
            //}
            //else if (e.KeyCode == Keys.D)
            //{
            //    toolStripMenuItem2_Click(sender, e);
            //}
            //else if (e.KeyCode == Keys.R)
            //{
            //    toolStripMenuItem4_Click(sender, e);
            //}
            //else if (e.KeyCode == Keys.M)
            //{
            //    toolStripMenuItem5_Click(sender, e);
            //}
            //else if (e.KeyCode == Keys.S)
            //{
            //    toolStripMenuItem6_Click(sender, e);
            //}
            //else if (e.KeyCode == Keys.C)
            //{
            //    toolStripMenuItem8_Click(sender, e);
            //}
            //else if (e.KeyCode == Keys.A)
            //{
            //    修改人数ToolStripMenuItem_Click(sender, e);
            //}
        }
    }
}
