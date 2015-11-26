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
    public partial class ListJoin : Form
    {
        private HttpAskfor httpReq = new HttpAskfor();
        public ListJoin()
        {
            InitializeComponent();
        }

        public string Str_Status;//接受到的桌子状态的JSON格式
        public string Str_Tables;//接受到的桌子信息的JSON格式

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
        public List<string> ConsumptionList2 = new List<string>();
        public List<int> PeopleList = new List<int>();
        public List<string> SizeList = new List<string>();
        public List<string> SubtotalList = new List<string>();
        public string Str_consumptionsid;

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

            List<Table> personsTable = httpReq.HttpGet<List<Table>>("tables");
            List<Status> personsStatus = httpReq.HttpGet<List<Status>>("statuses");

            for (int i = 0; i < personsStatus.Count(); i++)
            {
                if (personsStatus[i].consumption != null && personsStatus[i].consumption.id != PassValue.consumptionid && personsStatus[i].state == "dining")
                {
                    //判断是否是合并单，如果是合并单的话就直接
                    if (personsStatus[i].consumption.merge != null)
                    {
                        continue;
                    }

                    TableidList.Add(personsStatus[i].table.id);//桌子ID
                    ConsumptionList.Add(personsStatus[i].consumption.id);//消费ID
                    PeopleList.Add(personsStatus[i].consumption.people);//人数
                    SizeList.Add(personsStatus[i].table.size.ToString());//大小
                    SubtotalList.Add(personsStatus[i].consumption.subtotal);//总价
                }
            }
            if (TableidList.Count() != 0)//判断是否有消费的桌子，如果有的情况
            {
                for (int j = 0; j < TableidList.Count(); j++)
                {
                    DeskControl.DeskControl dc = new DeskControl.DeskControl();
                    dc.Location = new System.Drawing.Point(item + (y - item) / 2 + (item + w) * (j % t), item2 * 2 + (item2 + h) * (j / t));//桌子控件坐标位置[动态坐标]
                    this.panelTables.Controls.Add(dc);//tab里面添加构造好的桌子
                    dc.MouseDown += new MouseEventHandler(btntalbe_MouseDown);//桌子的点击触发事件
                    dc.lbConsumption.Text = ConsumptionList[j];//消费ID
                    for (int c = 0; c < personsTable.Count(); c++)
                    {
                        if (personsTable[c].id == TableidList[j])
                        {
                            dc.lbName.Text = personsTable[c].name;//桌子名
                            dc.lbNumber.Text = personsTable[c].room.name;//包间名
                            dc.lbTableID.Text = personsTable[c].id;//桌子ID
                            dc.lbPeople.Text = PeopleList[j].ToString() + "/" + SizeList[j] + "  ¥" + SubtotalList[j];
                            dc.picDesk.Image = Properties.Resources.dining;
                            dc.lbStatus.Text = "dining";
                        }
                    }
                }
            }
        }

        public List<string> SelectIDList = new List<string>();//将被选中的桌子的消费ID存放进集合
        //桌子的点击事件
        public void btntalbe_MouseDown(object sender, MouseEventArgs e)
        {
            DeskControl.DeskControl dc = (DeskControl.DeskControl)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (!dc.picCheck.Visible)
                {
                    dc.picCheck.Visible = true;
                    if (!SelectIDList.Contains(dc.lbConsumption.Text))
                    {
                        SelectIDList.Add(dc.lbConsumption.Text);//添加的时候，如果不包含才能添加
                    }
                }
                else
                {
                    dc.picCheck.Visible = false;
                    if (SelectIDList.Contains(dc.lbConsumption.Text))
                    {
                        SelectIDList.Remove(dc.lbConsumption.Text);//删除的时候，只有包含才能删除
                    }
                }
            }
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

        //查询
        private void Btn_Find_Click(object sender, EventArgs e)
        {
            int n = this.Width;//总宽度
            DeskControl.DeskControl dcdc2 = new DeskControl.DeskControl();
            int w = dcdc2.Width;//控件宽度
            int h = dcdc2.Height;//控件高度
            int item = w / 5;//控件间距
            int item2 = h / 5;
            int t = (n - item) / (w + item);//控件个数
            int y = n - (item + w) * t;//右边间距

            if (this.Txt_Find.Text == "")
            {
                AddTable();
            }
            else
            {
                TableidList.Clear(); PeopleList.Clear(); SizeList.Clear(); SubtotalList.Clear();

                List<Table> personsTable = httpReq.HttpGet<List<Table>>("tables");
                List<Status> personsStatus = httpReq.HttpGet<List<Status>>("statuses");

                for (int i = 0; i < personsStatus.Count(); i++)
                {
                    if (personsStatus[i].table.name.Equals(this.Txt_Find.Text) && personsStatus[i].consumption != null && personsStatus[i].state == "dining" && personsStatus[i].consumption.id != PassValue.consumptionid)
                    {
                        //判断是否是合并单，如果是合并单的话就直接
                        //if (personsStatus[i].consumption.merge != null)
                        //{
                        //    continue;
                        //}

                        TableidList.Add(personsStatus[i].table.id);//桌子ID
                        ConsumptionList2.Add(personsStatus[i].consumption.id);//消费ID
                        PeopleList.Add(personsStatus[i].consumption.people);//人数
                        SizeList.Add(personsStatus[i].table.size.ToString());//大小
                        SubtotalList.Add(personsStatus[i].consumption.subtotal);//总价
                    }
                }
                if (TableidList.Count() != 0)//判断是否有消费的桌子，如果有的情况
                {
                    for (int j = 0; j < TableidList.Count(); j++)
                    {
                        DeskControl.DeskControl dc = new DeskControl.DeskControl();
                        dc.Location = new System.Drawing.Point(item + (y - item) / 2 + (item + w) * (j % t), item2 * 2 + (item2 + h) * (j / t));//桌子控件坐标位置[动态坐标]
                        this.panelTables.Controls.Clear();
                        this.panelTables.Controls.Add(dc);//tab里面添加构造好的桌子
                        dc.MouseDown += new MouseEventHandler(btntalbe_MouseDown);//桌子的点击触发事件
                        dc.lbConsumption.Text = ConsumptionList2[j];//消费ID
                        for (int c = 0; c < personsTable.Count(); c++)
                        {
                            if (personsTable[c].id == TableidList[j])
                            {
                                dc.lbName.Text = personsTable[c].name;//桌子名
                                dc.lbNumber.Text = personsTable[c].room.name;//包间名
                                dc.lbTableID.Text = personsTable[c].id;//桌子ID
                                dc.lbPeople.Text = PeopleList[j].ToString() + "/" + SizeList[j] + "  ¥" + SubtotalList[j];
                                dc.picDesk.Image = Properties.Resources.dining;
                                dc.lbStatus.Text = "dining";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("您输入的桌号不存在或未消费！");
                }
            }
        }

        private void ListJoin_Load(object sender, EventArgs e)
        {
            this.Txt_Master.Text = PassValue.consumptionid;//主消费ID
            AddTable();
        }

        //取消
        private void Btn_Canel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //合并单子
        private void Btn_Enter_Click(object sender, EventArgs e)
        {
            if (SelectIDList != null)
            {
                if (SelectIDList.Count == 0)
                {
                    MessageBox.Show("您未选择任何桌子！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                Consumption personsConsumption = httpReq.HttpGet<Consumption>(string.Format("consumptions/{0}", this.Txt_Master.Text));

                if (personsConsumption.merge == null)
                {
                    Merge merge = new Merge();
                    List<Consumption> branches = new List<Consumption>();
                    Consumption master = new Consumption();
                    master.id = this.Txt_Master.Text;
                    for (int i = 0; i < SelectIDList.Count(); i++)
                    {
                        Consumption branch = new Consumption();
                        branch.id = SelectIDList[i];
                        branches.Add(branch);
                    }
                    merge.master = master;
                    merge.branches = branches.ToArray();
                    HttpResult httpResult = httpReq.HttpPost("consumptions/merge", merge);
                    if ((int)httpResult.StatusCode == 409)
                    {
                        MessageBox.Show("选择的桌子已被操作，请选择其他！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        AddTable();
                        return;
                    }
                    else if ((int)httpResult.StatusCode == 401)
                    {
                        LoginBusiness lg = new LoginBusiness();
                        lg.LoginAgain();
                        return;
                    }
                    else if ((int)httpResult.StatusCode == 0)
                    {
                        MessageBox.Show(string.Format("{0}{1}", httpResult.StatusDescription, httpResult.OtherDescription), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
                else if (personsConsumption.merge != null)
                {
                    Merge mg = new Merge();
                    List<Consumption> branches = new List<Consumption>();
                    Consumption merge = new Consumption();
                    merge.id = this.Txt_Master.Text;
                    Consumption master = new Consumption();
                    master.id = personsConsumption.merge.master.id;
                    for (int i = 0; i < SelectIDList.Count(); i++)
                    {
                        Consumption branch = new Consumption();
                        branch.id = SelectIDList[i];
                        branches.Add(branch);
                    }
                    for (int j = 0; j < personsConsumption.merge.branches.Count(); j++)
                    {
                        Consumption branch2 = new Consumption();
                        branch2.id = personsConsumption.merge.branches[j].id;
                        branches.Add(branch2);
                    }
                    mg.merge = merge;
                    mg.master = master;
                    mg.branches = branches.ToArray();


                    HttpResult httpResult = httpReq.HttpPost("consumptions/merge", mg);
                    if ((int)httpResult.StatusCode == 409)
                    {
                        MessageBox.Show("选择的桌子已被操作，请选择其他！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        AddTable();
                        return;
                    }
                    else if ((int)httpResult.StatusCode == 401)
                    {
                        LoginBusiness lg = new LoginBusiness();
                        lg.LoginAgain();
                        return;
                    }
                    else if ((int)httpResult.StatusCode == 0)
                    {
                        MessageBox.Show(string.Format("{0}{1}", httpResult.StatusDescription, httpResult.OtherDescription), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
                MessageBox.Show("合并成功！");
                Desk d;
                d = (Desk)this.Owner;
                d.Refresh_Method();
                this.Close();
            }
        }
    }
}
