using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business;
using System.Net;

namespace Client
{
    public partial class BankCard : Form
    {
        private HttpAskfor httpReq = new HttpAskfor();
        private string BankCardConsumptionid;
        public BankCard(string consumptionid)
        {
            InitializeComponent();
            BankCardConsumptionid = consumptionid;
        }

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

        #region 窗体操作和控件的设置
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void BankCard_Load(object sender, EventArgs e)
        {
            this.lbReceiveShould.Text = PassValue.Price_Now;
            this.TxtDiscount.Focus();
            this.TxtDiscount.Text = this.lbReceiveShould.Text;
            this.Btn_Ok.Image = Properties.Resources.确定2;
            this.Btn_Ok.Enabled = true;
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
            mb.Btn_BankCard.Image = Properties.Resources.银联卡;
            mb.AddInformation();//重新加载打折信息
            this.Close();
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
        // <summary>
        /// 确认提交方法
        /// </summary>
        public void button_ok()
        {
            if (this.TxtDiscount.Text != null && this.TxtDiscount.Text.Trim()!="0")
            {
                string price_fixed = double.Parse(this.TxtDiscount.Text).ToString("0.00");
                Member mb = new Member();
                mb = (Member)this.Owner;
                mb.KeyPreview = true;
                mb.panelChildren.Controls.Remove(this);
                mb.panelInfor.Visible = true;
                mb.lbTitle.Text = "支付信息";

                if (PassValue.payments.Count != 0 && PassValue.payments.Where(payment => payment.method == "bankcard").FirstOrDefault() != null)
                {
                    mb.lbReceiveActual.Text = (mb.Price_Recive + double.Parse(price_fixed) - double.Parse(PassValue.payments.Where(payment => payment.method == "bankcard").FirstOrDefault().amount)).ToString("0.00");
                    PassValue.payments.Remove(PassValue.payments.Where(payment => payment.method == "bankcard").FirstOrDefault());
                }
                else
                {
                    mb.lbReceiveActual.Text = (mb.Price_Recive + double.Parse(price_fixed)).ToString("0.00");
                }

                //银联卡支付
                Payment pm = new Payment();
                pm.amount = this.TxtDiscount.Text;
                pm.method = "bankcard";
                PassValue.payments.Add(pm);
                mb.panelChildren.Visible = true;  
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
            mb.panelInfor.Visible = true;
            mb.lbTitle.Text = "支付信息";
            mb.Btn_BankCard.Image = Properties.Resources.银联卡;
            mb.panelChildren.Visible = true;
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
