using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using Business;

namespace Client
{
    public partial class RemoveList : Form
    {
        public RemoveList()
        {
            InitializeComponent();
        }
        public string Str_Status;//接受到的桌子状态的JSON格式
        public string Str_Tables;//接受到的桌子信息的JSON格式
        public string Str_consumptionsid;//接受到的消费的JSON格式

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public List<string> TableidList = new List<string>();
        public List<string> ConsumptionList = new List<string>();
        public List<int> PeopleList = new List<int>();
        public List<string> SizeList = new List<string>();
        public List<string> SubtotalList = new List<string>();

        public List<string> SelectIDList = new List<string>();//将被选中的桌子的消费ID存放进集合

        //桌子传值
        public void getstrTables()
        {
            GetInformation.address = "tables";//桌子
            GetInformation gd = new GetInformation();
            Str_Tables = gd.GetHTTPInfo();//接收JSON数据
        }

        //桌子状态信息查询
        public void getstrstatus()
        {
            GetInformation.address = "statuses";//桌子状态信息
            GetInformation gd = new GetInformation();
            Str_Status = gd.GetHTTPInfo();//接收JSON数据
        }

        //消费ID的信息查询
        public void getconsumptionsid()
        {
            GetInformation.address = "consumptions/" + this.Txt_Master.Text;//桌子状态信息
            if (this.Txt_Master.Text != "")
            {
                GetInformation gi = new GetInformation();
                Str_consumptionsid = gi.GetHTTPInfo();//接收JSON数据
            }
            else
            {
                Str_consumptionsid = "";
            }
        }

        public string consumptionsid;
        //桌子的点击事件
        public void btntalbe_MouseDown(object sender, MouseEventArgs e)
        {
            DeskControl.DeskControl dc = (DeskControl.DeskControl)sender;
            consumptionsid = dc.Tag.ToString();
            if (e.Button == MouseButtons.Left)
            {
                if (dc.picCheck.Visible == false)
                {
                    foreach (Control ctl in this.panelTables.Controls)
                    {
                        if (ctl is DeskControl.DeskControl)
                        {
                            DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                            if (ctl.Tag.ToString() == consumptionsid && ctl.Tag != null)
                            {
                                dcdc.picCheck.Visible = true;
                                //PassValue.count_select_ordering += 1;
                            }
                        }
                    }
                    if (SelectIDList.Contains(dc.lbConsumption.Text) == false)
                    {
                        SelectIDList.Add(dc.lbConsumption.Text);//添加的时候，如果不包含才能添加
                    }
                }
                else
                {
                    foreach (Control ctl in this.panelTables.Controls)
                    {
                        if (ctl is DeskControl.DeskControl)
                        {
                            DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                            if (ctl.Tag.ToString() == consumptionsid && ctl.Tag != null)
                            {
                                dcdc.picCheck.Visible = false;
                                //PassValue.count_select_ordering -= 1;
                            }
                        }
                    }
                    if (SelectIDList.Contains(dc.lbConsumption.Text) == true)
                    {
                        SelectIDList.Remove(dc.lbConsumption.Text);//删除的时候，只有包含才能删除
                    }
                }
            }
        }

        //取消
        private void Btn_Canel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RemoveList_Load(object sender, EventArgs e)
        {
            this.Txt_Master.Text = PassValue.consumptionid;
            AddTable();
        }

        //桌子的构造
        public void AddTable()
        {
            int n = this.Width;//总宽度
            DeskControl.DeskControl dcdc2 = new DeskControl.DeskControl();
            int w = dcdc2.Width;//控件宽度
            int h = dcdc2.Height;//控件高度
            int item = w / 5;//控件间距
            int item2 = h / 5;
            int t = (n - item) / (w + item);//控件个数
            int y = n - (item + w) * t;//右边间距

            TableidList.Clear(); PeopleList.Clear(); SizeList.Clear(); SubtotalList.Clear();
            getstrstatus();//重新加载桌子状态
            var jserStatus = new JavaScriptSerializer();
            var personsStatus = jserStatus.Deserialize<List<Status>>(Str_Status);//重新获得桌子状态信息
            getstrTables();//重新加载桌子信息
            var jserTable = new JavaScriptSerializer();
            var personsTable = jserTable.Deserialize<List<Table>>(Str_Tables);//重新获得桌子信息

            JavaScriptSerializer jserConsumptions = new JavaScriptSerializer();
            getconsumptionsid();
            Consumption personsConsumptions = jserConsumptions.Deserialize<Consumption>(Str_consumptionsid);
            Consumption master = personsConsumptions.merge.master;
            string[] mTableIds = new string[master.tables.Count()];
            int nn = 0;
            foreach (Table mTable in master.tables)
            {
                mTableIds[nn++] = mTable.id;
            }
            foreach (Table table in personsConsumptions.tables)
            {
                for (int i = 0; i < personsStatus.Count(); i++)
                {
                    if (table.id.Equals(personsStatus[i].table.id))
                    {

                        if (!mTableIds.Contains(table.id))
                        {
                            TableidList.Add(personsStatus[i].table.id);//桌子ID
                            ConsumptionList.Add(personsStatus[i].consumption.id);//消费ID
                            PeopleList.Add(personsStatus[i].consumption.people);//人数
                            SizeList.Add(personsStatus[i].table.size.ToString());//大小
                            SubtotalList.Add(personsStatus[i].consumption.subtotal);//总价
                        }
                    }
                }
            }

            for (int j = 0; j < TableidList.Count(); j++)
            {
                DeskControl.DeskControl dc = new DeskControl.DeskControl();
                dc.Location = new System.Drawing.Point(item + (y - item) / 2 + (item + w) * (j % t), item2 * 2 + (item2 + h) * (j / t));//桌子控件坐标位置[动态坐标]
                this.panelTables.Controls.Add(dc);//tab里面添加构造好的桌子
                dc.MouseDown += new MouseEventHandler(btntalbe_MouseDown);//桌子的点击触发事件
                for (int c = 0; c < personsTable.Count(); c++)
                {
                    if (personsTable[c].id == TableidList[j])
                    {
                        if (personsConsumptions != null && personsConsumptions.merge != null)
                        {
                            if (personsConsumptions != null && personsConsumptions.merge != null)
                            {
                                if (master != null)
                                {
                                    foreach (Table table in master.tables)
                                    {
                                        if (personsTable[c].id.Equals(table.id))
                                        {
                                            dc.lbConsumption.Text = master.id;
                                            dc.Tag = dc.lbConsumption.Text;
                                        }
                                    }
                                }
                                Consumption[] branches = personsConsumptions.merge.branches;
                                if (branches != null)
                                {
                                    foreach (Consumption branch in branches)
                                    {
                                        foreach (Table table in branch.tables)
                                        {
                                            if (personsTable[c].id.Equals(table.id))
                                            {
                                                dc.lbConsumption.Text = branch.id;
                                                dc.Tag = dc.lbConsumption.Text;
                                            }
                                        }
                                    }
                                }
                            }
                            dc.lbName.Text = personsTable[c].name;//桌子名
                            dc.lbNumber.Text = personsTable[c].room.name;//包间名
                            dc.lbTableID.Text = personsTable[c].id;//桌子ID
                            dc.lbPeople.Text = PeopleList[j].ToString() + "/" + SizeList[j] + "  ¥" + SubtotalList[j];
                            dc.picDesk.Image = Properties.Resources.dining;
                            dc.lbStatus.Text = "dining";
                            //dc.lbConsumption.Visible = true;
                        }
                    }
                }
            }
        }

        //拆单
        private void Btn_Enter_Click(object sender, EventArgs e)
        {
            if (SelectIDList != null)
            {
                if (SelectIDList.Count == 0)
                {
                    MessageBox.Show("您未选择任何桌子！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                Desk d;
                d = (Desk)this.Owner;

                getconsumptionsid();//接受json数据
                var jserConsumption = new JavaScriptSerializer();
                var personsConsumption = jserConsumption.Deserialize<Consumption>(Str_consumptionsid);//解析json数据
                if (personsConsumption.merge.branches.Count() > 1)
                {
                    Merge mg = new Merge();
                    List<Consumption> branches = new List<Consumption>();
                    Consumption merge = new Consumption();
                    merge.id = this.Txt_Master.Text;
                    Consumption master = new Consumption();
                    master.id = personsConsumption.merge.master.id;
                    for (int j = 0; j < personsConsumption.merge.branches.Count(); j++)
                    {
                        Consumption branch = new Consumption();

                        branch.id = personsConsumption.merge.branches[j].id;
                        branches.Add(branch);
                    }
                    for (int i = 0; i < SelectIDList.Count(); i++)
                    {
                        Consumption target = branches.Where(r => r.id == SelectIDList[i]).FirstOrDefault();
                        branches.Remove(target);

                        //根据消费ID 移除桌子
                        foreach (Control ct in this.panelTables.Controls)
                        {
                            if (ct is DeskControl.DeskControl)
                            {
                                DeskControl.DeskControl dc = (DeskControl.DeskControl)ct;
                                if (SelectIDList[i] == dc.lbConsumption.Text)
                                {
                                    d.CurrentChooseDesk.Remove(dc.lbTableID.Text);
                                }
                            }
                        }
                    }
                    mg.merge = merge;
                    mg.master = master;
                    mg.branches = branches.ToArray();
                    System.Net.WebHeaderCollection headers = new System.Net.WebHeaderCollection();
                    headers.Add("Authorization", PassValue.token);
                    Post.PostHttp(headers, "consumptions/merge", mg);
                }
                else if (personsConsumption.merge.branches.Count() == 1)
                {
                    Merge mg = new Merge();
                    List<Consumption> branches = new List<Consumption>();
                    Consumption merge = new Consumption();
                    merge.id = this.Txt_Master.Text;
                    mg.merge = merge;
                    mg.master = null;
                    mg.branches = null;

                    for (int i = 0; i < SelectIDList.Count(); i++)
                    {
                        //根据消费ID 移除桌子
                        foreach (Control ct in this.panelTables.Controls)
                        {
                            if (ct is DeskControl.DeskControl)
                            {
                                DeskControl.DeskControl dc = (DeskControl.DeskControl)ct;
                                if (SelectIDList[i] == dc.lbConsumption.Text)
                                {
                                    d.CurrentChooseDesk.Remove(dc.lbTableID.Text);
                                }
                            }
                        }
                    }


                    System.Net.WebHeaderCollection headers = new System.Net.WebHeaderCollection();
                    headers.Add("Authorization", PassValue.token);
                    Post.PostHttp(headers, "consumptions/merge", mg);
                }
                MessageBox.Show("拆单成功！");
              
                d.Refresh_Method();
                this.Close();
            }
        }
    }
}
