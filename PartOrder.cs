using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;//正则表达式的引用
using System.Web.Script.Serialization;
using Business;
using System.Collections;

namespace Client
{
    public partial class PartOrder : Form
    {
        private string partConsumptionid;
        private HttpAskfor httpReq = new HttpAskfor();

        public PartOrder(string consumption)
        {
            InitializeComponent();
            partConsumptionid = consumption;
        }

        public string Str_consumptionsid;
        public string consumptionsid;

        #region JOSN数据获取方法
        /// <summary>
        /// 获取JSON数据
        /// </summary>
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

        //消费ID的信息查询
        public void getconsumptionsid()
        {
            consumptionsid = PassValue.consumptionid;
            GetInformation.address = "consumptions/" + consumptionsid;//桌子状态信息
            if (consumptionsid != "")
            {
                GetInformation gi = new GetInformation();
                Str_consumptionsid = gi.GetHTTPInfo();//接收JSON数据
            }
            else
            {
                Str_consumptionsid = "";
            }
        }
        #endregion

        #region 取消按钮
        /// <summary>
        /// 取消
        /// </summary>
        private void Btn_Canel_Click(object sender, EventArgs e)
        {
            Member mb = new Member();
            mb = (Member)this.Owner;
            mb.panelChildren.Controls.Remove(this);
            mb.panelInfor.Visible = true;
            mb.lbTitle.Text = "支付信息";
            mb.SetUp();
            mb.Btn_All.Enabled = mb.Btn_Plan.Enabled = mb.Btn_Fixed.Enabled = true;
            mb.AddInformation();//重新加载打折信息
        }
        /// <summary>
        /// 取消按钮的悬停
        /// </summary>
        private void Btn_Canel_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_Canel.Image = Properties.Resources.取消2;
        }

        private void Btn_Canel_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_Canel.Image = Properties.Resources.取消;
        }
        #endregion

        #region 输入框的变化
        /// <summary>
        /// 输入框文本变化事件
        /// </summary>
        private void TxtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.Match(this.TxtDiscount.Text.Trim(), "^\\d+$").Success)//利用正则表达式控制只能输入数字
            {
                this.TxtDiscount.Text = "";
            }
            else
            {
                if (this.TxtDiscount.Text != "" && this.TxtDiscount.Text != null && Int32.Parse(this.TxtDiscount.Text) > 0 && Int32.Parse(this.TxtDiscount.Text) < 100)
                {
                    this.Btn_Ok.Image = Properties.Resources.确定2;
                }
            }

            if (string.IsNullOrEmpty(this.TxtDiscount.Text))
            {
                this.Btn_Ok.Image = Properties.Resources.确定3;
                this.Btn_Ok.Enabled = false;
            }
            else
            {
                this.Btn_Ok.Image = Properties.Resources.确定2;
                this.Btn_Ok.Enabled = true;
            }
        }

        /// <summary>
        /// 关闭快捷键
        /// </summary>
        private void TxtDiscount_Enter(object sender, EventArgs e)
        {
            //Member mb = new Member();
            //mb = (Member)this.Owner;
            //mb.KeyPreview = false;
        }
        #endregion

        #region 确认按钮
        /// <summary>
        /// 打折事件(button)
        /// </summary>
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            button_ok();
        }
        /// <summary>
        /// 确认按钮
        /// </summary>
        public void button_ok()
        {
            if (this.TxtDiscount.Text != null && this.TxtDiscount.Text != "")
            {
                PassValue.Percent = Int32.Parse(this.TxtDiscount.Text);
                if (PassValue.Percent != 0)
                {
                    Discount ds = new Discount();
                    ds.type = "partial";
                    ds.percent = PassValue.Percent;
                    List<DiscountItem> items = new List<DiscountItem>();

                    if (PassValue.listItemID.Count == 0)
                    {
                        for (int count = 0; count < PassValue.listItemIDBefore.Count; count++)
                        {
                            DiscountItem im = new DiscountItem();
                            im.id = PassValue.listItemIDBefore[count].ToString();
                            PassValue.listItemID.Add(PassValue.listItemIDBefore[count].ToString());
                            im.percent = PassValue.Percent;
                            items.Add(im);
                        }
                    }
                    else
                    {
                        for (int count = 0; count < PassValue.listItemID.Count; count++)
                        {
                            DiscountItem im = new DiscountItem();
                            im.id = PassValue.listItemID[count].ToString();
                            im.percent = PassValue.Percent;
                            items.Add(im);
                        }
                    }
                    ds.items = new DiscountItem[items.Count];
                    int t = 0;
                    foreach (DiscountItem itm in items)
                    { ds.items[t++] = itm; }
                    PassValue.discounts.Add(ds);
                }
                PassValue.Infor_payment.discounts = new Discount[PassValue.discounts.Count];
                int i = 0;
                foreach (Discount ds in PassValue.discounts)
                {
                    PassValue.Infor_payment.discounts[i++] = ds;
                }

                HttpResult httpResult = httpReq.HttpPatch(string.Format("consumptions/{0}", partConsumptionid), PassValue.Infor_payment);
                if ((int)httpResult.StatusCode == 401)
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
                Form_Esc();
            }
        }
        /// <summary>
        /// 确认按钮鼠标悬停
        /// </summary>
        private void Btn_Ok_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.TxtDiscount.Text.Trim() != "" && this.TxtDiscount.Text != null)
            {
                this.Btn_Ok.Image = Properties.Resources.确定;
            }
        }

        private void Btn_Ok_MouseLeave(object sender, EventArgs e)
        {
            if (this.TxtDiscount.Text.Trim() != "" && this.TxtDiscount.Text != null)
            {
                this.Btn_Ok.Image = Properties.Resources.确定2;
            }
        }
        #endregion

        #region 窗体的加载事件
        /// <summary>
        /// 窗体的加载事件
        /// </summary>
        private void PartOrder_Load(object sender, EventArgs e)
        {
            this.TxtDiscount.Focus();
            this.lbConsumption.Text = PassValue.Price_All + "元";
            this.Btn_Ok.Image = Properties.Resources.确定3;
            this.Btn_Ok.Enabled = false;
            this.Btn_Canel.Image = Properties.Resources.取消;
            this.TxtDiscount.Text = PassValue.Percent == 0 ? "" : PassValue.Percent.ToString();
            this.lbCount.Text = PassValue.listItemID.Count == 0 ? PassValue.listItemIDBefore.Count.ToString() : PassValue.listItemID.Count.ToString();
        }
        /// <summary>
        /// 退出后的刷新
        /// </summary>
        public void Form_Esc()
        {
            Member mb = new Member();
            mb = (Member)this.Owner;
            mb.KeyPreview = true;
            mb.panelChildren.Controls.Remove(this);
            mb.panelInfor.Visible = true;
            mb.lbTitle.Text = "支付信息";
            mb.SetUp();
            PassValue.discounts.Clear();
            mb.AddInformation();//重新加载打折信息
            this.Close();
        }
        #endregion

        #region 打折菜单的展开
        /// <summary>
        /// 打折菜单的展开
        /// </summary>
        private void panelChoose_Click(object sender, EventArgs e)
        {
            if (this.TxtDiscount.Text != "")
            {
                Member mb = new Member();
                mb = (Member)this.Owner;
                PartOrderChoose poc = new PartOrderChoose(partConsumptionid);

                Consumption personsConsumption = httpReq.HttpGet<Consumption>(string.Format("consumptions/{0}", partConsumptionid));

                //获取折扣的菜
                //if (personsConsumption != null)
                //{
                //    Discount d = personsConsumption.discounts.Where(r => r.type == "partial").FirstOrDefault();
                //    if (d != null)
                //    {
                //        foreach (DiscountItem item in d.items)
                //        {
                            
                //        }
                //    }
                //}

                ArrayList haveAdd = new ArrayList();
                for (int i = 0; i < personsConsumption.details.Count(); i++)
                {
                    if (personsConsumption.details[i].discountable == true)
                    {
                        if (!haveAdd.Contains(personsConsumption.details[i].item.id))
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = "";
                            item.SubItems.Add(personsConsumption.details[i].item.name);
                            item.Tag = personsConsumption.details[i].item.id;
                            if (PassValue.listItemID.Count == 0)
                            {
                                item.Checked = true;
                            }
                            else
                            {
                                if (PassValue.listItemID.Contains(personsConsumption.details[i].item.id))
                                {
                                    item.Checked = true;
                                }
                                else
                                {
                                    item.Checked = false;
                                }
                            }
                            haveAdd.Add(personsConsumption.details[i].item.id);
                            poc.lv.Items.Add(item);
                        }
                    }
                }
                poc.Owner = this;
                this.Visible = false;
                poc.TopLevel = false;
                mb.panelChildren.Controls.Add(poc);
                poc.Show();
            }
            else if (this.TxtDiscount.Text == "")
            {
                Messagebox mb = new Messagebox();
                PassValue.MessageInfor = "请先输入折扣率!";
                mb.ShowDialog();
            }
        }
        #endregion

        private void TxtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                button_ok();
            }
        }
    }
}
