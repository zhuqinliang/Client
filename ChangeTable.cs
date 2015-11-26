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
    public partial class ChangeTable : Form
    {
        int people = 0;
        HttpAskfor httpReq = new HttpAskfor();
        public ChangeTable(int _people)
        {
            InitializeComponent();
            people = _people;
        }

        public string Str_Tables;
        public string Str_Status;
        public string Str_consumptionsid;
        public List<string> TableidList = new List<string>();
        public List<string> ConsumptionList = new List<string>();
        public List<int> PeopleList = new List<int>();
        public List<string> SizeList = new List<string>();
        public List<string> SubtotalList = new List<string>();
        public List<string> StatusList = new List<string>();

        private void ChangeTable_Load(object sender, EventArgs e)
        {
            this.lbTable.Text = PassValue.ChooseFloor +"F "+ PassValue.Tablename;
            this.Txt_ID.Text = PassValue.consumptionid;
            AddTable();
        }

        //取消
        private void Btn_Canel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public List<string> SelectIDList = new List<string>();//将被选中的桌子的消费ID存放进集合
        //桌子点击事件
        public void btntalbe_MouseDown(object sender, MouseEventArgs e)
        {
            PassValue.count_select_ordering = 0;
            DeskControl.DeskControl dc = (DeskControl.DeskControl)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (!dc.picCheck.Visible)
                {
                    dc.picCheck.Visible = true;
                    if (!SelectIDList.Contains(dc.lbTableID.Text))
                    {
                        SelectIDList.Add(dc.lbTableID.Text);//添加的时候，如果不包含才能添加
                        PassValue.count_select_ordering +=1;
                    }
                }
                else
                {
                    dc.picCheck.Visible = false;
                    if (SelectIDList.Contains(dc.lbTableID.Text))
                    {
                        SelectIDList.Remove(dc.lbTableID.Text);//删除的时候，只有包含才能删除
                        PassValue.count_select_ordering -=1;
                    }
                }
            }
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

            this.panelTables.Controls.Clear();
            SelectIDList.Clear();
            TableidList.Clear();
            SizeList.Clear();
            StatusList.Clear();

            List<Table> personsTable = httpReq.HttpGet<List<Table>>("tables");
            List<Status> personsStatus = httpReq.HttpGet<List<Status>>("statuses");

            //排序
            if (personsStatus != null)
            {
                personsStatus = personsStatus.OrderBy(r => r.table.name).ToList(); 
            }


            for (int i = 0; i < personsStatus.Count(); i++)
            {
                if (personsStatus[i].consumption == null || personsStatus[i].consumption.id == this.Txt_ID.Text)
                {
                    TableidList.Add(personsStatus[i].table.id);//桌子ID
                    SizeList.Add(personsStatus[i].table.size.ToString());//大小
                    StatusList.Add(personsStatus[i].state);//状态
                    if (personsStatus[i].consumption == null) 
                    { 
                        ConsumptionList.Add(""); 
                        PeopleList.Add(0);//人数
                        SubtotalList.Add("0.00");//总价
                    } 
                    else 
                    { 
                        ConsumptionList.Add(personsStatus[i].consumption.id); //消费ID
                        PeopleList.Add(personsStatus[i].consumption.people);//人数
                        SubtotalList.Add(personsStatus[i].consumption.subtotal);//总价
                    }
                }
            }


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
                        if (StatusList[j].Equals("ordering")) { dc.picDesk.Image = Properties.Resources.ordering; dc.lbStatus.Text = "ordering"; }
                        else if (StatusList[j].Equals("dining")) { dc.picDesk.Image = Properties.Resources.dining; dc.lbStatus.Text = "dining"; }
                    }
                }
            }
        }

        //提交
        private void Btn_Enter_Click(object sender, EventArgs e)
        {
            if (SelectIDList.Count > 0)
            {
                Desk d;
                d = (Desk)this.Owner;

                d.CurrentChooseDesk.Clear();
                //添加桌子
                foreach (Control ctl in this.panelTables.Controls)
                {
                    if (ctl is DeskControl.DeskControl)
                    {
                        DeskControl.DeskControl dc = (DeskControl.DeskControl)ctl;
                        if (dc.picCheck.Visible)
                        {
                            d.CurrentChooseDesk.Add(dc.lbTableID.Text, string.Format("{0}({1})", dc.lbName.Text, dc.lbNumber.Text));
                        }
                    }
                }

                Consumption cp = new Consumption();
                cp.id = this.Txt_ID.Text;
                cp.tables = new Table[SelectIDList.Count()];
                cp.people = people;
                int i = 0;
                foreach (string id in SelectIDList)
                {
                    Table table = new Table();
                    table.id = id;
                    cp.tables[i++] = table;
                }

                HttpResult httpResult = httpReq.HttpPatch(string.Format("consumptions/{0}", cp.id), cp);
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

                MessageBox.Show("换台成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                d.Refresh_Method();
                this.Close();
            }
            else
            {
                MessageBox.Show("请先选择桌子！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

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
            this.panelTables.Controls.Clear();
            TableidList.Clear(); PeopleList.Clear(); SizeList.Clear(); SubtotalList.Clear(); SelectIDList.Clear();
            if (string.IsNullOrEmpty(this.Txt_Find.Text))
            {
                AddTable();
            }
            else
            {
                List<Table> personsTable = httpReq.HttpGet<List<Table>>("tables");
                List<Status> personsStatus = httpReq.HttpGet<List<Status>>("statuses");

                for (int i = 0; i < personsStatus.Count(); i++)
                {
                    if (personsStatus[i].table.name.Equals(this.Txt_Find.Text) && personsStatus[i].consumption == null && personsStatus[i].state == "idle")
                    {
                        TableidList.Add(personsStatus[i].table.id);//桌子ID
                        SizeList.Add(personsStatus[i].table.size.ToString());//大小
                    }
                }
                if (TableidList.Count() != 0)//判断是否有空的桌子，如果有的情况
                {
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
                                dc.lbName.Text = personsTable[c].name;//桌子名
                                dc.lbNumber.Text = personsTable[c].room.name;//包间名
                                dc.lbTableID.Text = personsTable[c].id;//桌子ID
                                dc.lbPeople.Text = "人数：0/" + SizeList[j]+ "  ¥" +"0.00";
                                dc.picDesk.Image = Properties.Resources.idle;
                                dc.lbStatus.Text = "idle";
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
    }
}
