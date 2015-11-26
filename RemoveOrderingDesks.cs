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
using System.Net;
using System.Collections;

namespace Client
{
    public partial class RemoveOrderingDesks : Form
    {
        HttpAskfor httpReq = new HttpAskfor();

        public RemoveOrderingDesks()
        {
            InitializeComponent();
        }

        private void Btn_Canel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public List<string> TableidList = new List<string>();
        public List<string> ConsumptionList = new List<string>();
        public List<string> ConsumptionList2 = new List<string>();
        public List<int> PeopleList = new List<int>();
        public List<string> SizeList = new List<string>();
        public List<string> SubtotalList = new List<string>();
        public string Str_consumptionsid;
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

            TableidList.Clear(); PeopleList.Clear(); SizeList.Clear(); SubtotalList.Clear(); RestTableIDList.Clear();

            List<Table> personsTable = httpReq.HttpGet<List<Table>>("tables");
            List<Status> personsStatus = httpReq.HttpGet<List<Status>>("statuses");

            for (int i = 0; i < personsStatus.Count(); i++)
            {
                if (personsStatus[i].consumption != null && personsStatus[i].consumption.id == PassValue.consumptionid)
                {
                    TableidList.Add(personsStatus[i].table.id);//桌子ID
                    ConsumptionList.Add(personsStatus[i].consumption.id);//消费ID
                    PeopleList.Add(personsStatus[i].consumption.people);//人数
                    SizeList.Add(personsStatus[i].table.size.ToString());//大小
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
                            dc.lbPeople.Text = "人数：" + PeopleList[j].ToString() + "/" + SizeList[j];
                            dc.picDesk.Image = Properties.Resources.ordering;
                            dc.lbStatus.Text = "ordering";
                            RestTableIDList.Add(dc.lbTableID.Text);
                        }
                    }
                }
            }
        }

        public List<string> RestTableIDList = new List<string>();//将剩余的的桌子的桌子ID存放进集合
        //桌子的点击事件
        public void btntalbe_MouseDown(object sender, MouseEventArgs e)
        {
            DeskControl.DeskControl dc = (DeskControl.DeskControl)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (!dc.picCheck.Visible)//未被选中的情况
                {
                    dc.picCheck.Visible = true;//选中
                    if (RestTableIDList.Contains(dc.lbTableID.Text))
                    {
                        RestTableIDList.Remove(dc.lbTableID.Text);
                    }
                }
                else//选中的情况
                {
                    dc.picCheck.Visible = false;//取消选中
                    if (!RestTableIDList.Contains(dc.lbTableID.Text))
                    {
                        RestTableIDList.Add(dc.lbTableID.Text);
                    }
                }
            }
        }

        private void RemoveOrderingDesks_Load(object sender, EventArgs e)
        {
            this.Txt_Master.Text = PassValue.consumptionid;//主消费ID
            AddTable();
        }

        //查找
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
                TableidList.Clear(); PeopleList.Clear(); SizeList.Clear(); SubtotalList.Clear(); RestTableIDList.Clear();

                List<Table> personsTable = httpReq.HttpGet<List<Table>>("tables");
                List<Status> personsStatus = httpReq.HttpGet<List<Status>>("statuses");

                for (int i = 0; i < personsStatus.Count(); i++)
                {
                    if (personsStatus[i].table.name.Equals(this.Txt_Find.Text) && personsStatus[i].consumption != null && personsStatus[i].consumption.id == PassValue.consumptionid)
                    {
                        TableidList.Add(personsStatus[i].table.id);//桌子ID
                        ConsumptionList2.Add(personsStatus[i].consumption.id);//消费ID
                        PeopleList.Add(personsStatus[i].consumption.people);//人数
                        SizeList.Add(personsStatus[i].table.size.ToString());//大小
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
                                dc.lbPeople.Text = "人数：" + PeopleList[j].ToString() + "/" + SizeList[j];
                                dc.picDesk.Image = Properties.Resources.ordering;
                                dc.lbStatus.Text = "ordering";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("查询有误！");
                }
            }
        }

        //消台操作
        private void Btn_Enter_Click(object sender, EventArgs e)
        {
            Desk d;
            d = (Desk)this.Owner;
            bool choose = false;
            ArrayList al = new ArrayList();
            foreach (Control ctl in this.panelTables.Controls)
            {
                if (ctl is DeskControl.DeskControl)
                {
                    DeskControl.DeskControl dcdc = (DeskControl.DeskControl)ctl;
                    if (dcdc.picCheck.Visible)
                    {
                        choose = true;
                        al.Add(dcdc.lbTableID.Text);
                    }
                }
            }
            if (!choose)
            {
                Messagebox mb = new Messagebox();
                PassValue.MessageInfor = "还未勾选桌子！";
                mb.ShowDialog();
                return;
            }

            foreach (string item in al)
            {
                d.CurrentChooseDesk.Remove(item);
            }
           

            Consumption cp = new Consumption();
            cp.id = this.Txt_Master.Text;
            int count = RestTableIDList.Count();
            cp.tables = new Table[count];
            for (int i = 0; i < count; i++)
            {
                cp.tables[i] = new Table();
                cp.tables[i].id = RestTableIDList[i];
            }
            cp.people = PeopleList[0];

            HttpResult httpResult;
            if (cp.tables.Count() == 0)
            {
                httpResult = httpReq.HttpDelete(string.Format("consumptions/{0}", PassValue.consumptionid));
            }
            else
            {
                httpResult = httpReq.HttpPatch(string.Format("consumptions/{0}", this.Txt_Master.Text), cp);
            }

            if ((int)httpResult.StatusCode == 409)
            {
                MessageBox.Show("选择的桌子已被操作，将为您刷新！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

         
            d.Refresh_Method();
            this.Close();
            PassValue.count_select_idle = 0;//被选中的桌子数量
            PassValue.count_select_ordering = cp.tables.Count();
            PassValue.selectedtableid.Clear();
            d.lbInfor.Text = d.lbInfor.Text.Split('[')[0] + "[空桌]";//餐台名称
            //人数
            d.lbPeople.Text = "0人";
        }
    }
}
