using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;//正则表达式的引用
using Business;
using System.Web.Script.Serialization;

namespace Client
{
    public partial class MalingOrder : Form
    {
        private HttpAskfor httpReq = new HttpAskfor();
        private string mailConsumptionsid;
        public MalingOrder(string consumptionsid)
        {
            InitializeComponent();
            mailConsumptionsid = consumptionsid;
        }

        #region JOSN数据获取方法
        /// <summary>
        /// 获取JSON数据
        /// </summary>
        public string Str_consumptionsid;//消费ID
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
            GetInformation.address = "consumptions/" + PassValue.consumptionid;//桌子状态信息
            if (PassValue.consumptionid != "")
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

        #region 窗体操作和控件的设置
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void MalingOrder_Load(object sender, EventArgs e)
        {
            this.lbReceiveShould.Text = PassValue.Price_Now;

            if (this.lbReceiveShould.Text.IndexOf(".") > 0)
            {
                string[] part = this.lbReceiveShould.Text.Split('.');
                this.TxtDiscount.Text = string.Format("0.{0}", part[1]);
                this.Btn_Ok.Image = Properties.Resources.确定2;
                this.Btn_Ok.Enabled = true;
            }
            else
            {
                this.Btn_Ok.Image = Properties.Resources.确定3;
                this.Btn_Ok.Enabled = false;
            }

            this.TxtDiscount.Focus();
            this.Btn_Canel.Image = Properties.Resources.取消;
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

        #region 输入框的变化
        /// <summary>
        /// 输入框文本变化事件
        /// </summary>
        private void TxtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (this.TxtDiscount.Text != "" && this.TxtDiscount.Text != null)
            {
                this.Btn_Ok.Image = Properties.Resources.确定2;
                this.Btn_Ok.Enabled = true;
            }
            else
            {
                this.Btn_Ok.Image = Properties.Resources.确定3;
                this.Btn_Ok.Enabled = false;
            }
        }
        /// <summary>
        /// 解除快捷键
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
        /// 确认
        /// </summary>
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (double.Parse(this.TxtDiscount.Text) > double.Parse(this.lbReceiveShould.Text))
            {
                MessageBox.Show("输入金额大于应收金额，请确认!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            button_ok();
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
        /// <summary>
        /// 确认提交方法
        /// </summary>
        public void button_ok()
        {
            if (this.TxtDiscount.Text != "")
            {
                Consumption personsConsumption = httpReq.HttpGet<Consumption>(string.Format("consumptions/{0}", mailConsumptionsid));
                if (personsConsumption.discounts != null)
                {
                    List<Discount> discount = new List<Discount>();
                    PassValue.discounts = personsConsumption.discounts.ToList();
                    discount = PassValue.discounts.Where(d => d.type == "round").ToList();
                    if (discount.Count != 0)
                    {
                        PassValue.discounts.Remove(PassValue.discounts.Where(d => d.type == "round").FirstOrDefault());
                    }
                }
                string price_fixed = double.Parse(this.TxtDiscount.Text).ToString("0.00");
                if (price_fixed != "0.00")//判断不能为0
                {
                    Discount ds = new Discount();
                    ds.type = "round";
                    ds.amount = price_fixed;
                    PassValue.discounts.Add(ds);
                    PassValue.Infor_payment.discounts = new Discount[PassValue.discounts.Count];
                }
                int i = 0;
                foreach (Discount ds in PassValue.discounts)
                {
                    PassValue.Infor_payment.discounts[i++] = ds;
                }

                HttpResult httpResult = httpReq.HttpPatch(string.Format("consumptions/{0}", mailConsumptionsid), PassValue.Infor_payment);
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
            mb.Btn_Maling.Image = Properties.Resources.摸零;
            mb.panelInfor.Visible = true;
            mb.lbTitle.Text = "支付信息";
            mb.AddInformation();
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
