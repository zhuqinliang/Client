using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Business;//正则表达式的引用

namespace Client
{
    public partial class FixedOrder : Form
    {
        private HttpAskfor httpReq = new HttpAskfor();
        private string fixedConsumptionsid;
        public FixedOrder(string consumptionsid)
        {
            InitializeComponent();
            fixedConsumptionsid = consumptionsid;
        }

        #region 输入框的变化
        /// <summary>
        /// 输入框文本变化事件
        /// </summary>
        private void TxtDiscount_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.TxtDiscount.Text))
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
                this.TxtDiscount.Text = null;
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
            if (this.TxtDiscount.Text != null)
            {
                string price_fixed = double.Parse(this.TxtDiscount.Text).ToString("0.00");
                if (price_fixed != "0.00")//判断不能为0
                {
                    Discount ds = new Discount();
                    ds.type = "quota";
                    ds.amount = price_fixed;
                    PassValue.discounts.Add(ds);
                    PassValue.Infor_payment.discounts = new Discount[PassValue.discounts.Count];
                }
                int i = 0;
                foreach (Discount ds in PassValue.discounts)
                {
                    PassValue.Infor_payment.discounts[i++] = ds;
                }

                HttpResult httpResult = httpReq.HttpPatch(string.Format("consumptions/{0}", fixedConsumptionsid), PassValue.Infor_payment);
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

        #region 窗体操作和控件的设置
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void FixedOrder_Load(object sender, EventArgs e)
        {
            this.TxtDiscount.Focus();
            this.lbReceiveShould.Text = PassValue.Price_All;
            this.Btn_Ok.Image = Properties.Resources.确定3;
            this.Btn_Ok.Enabled = false;
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
            mb.Btn_Part.Enabled = mb.Btn_Plan.Enabled = mb.Btn_All.Enabled = true;
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
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9')  || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back))
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
