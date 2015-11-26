using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business;
using System.Web.Script.Serialization;

namespace Client
{
    public partial class CashPayment : Form
    {
        private string CashConsumptionsid;
        public CashPayment(string consumptionsid)
        {
            InitializeComponent();
            CashConsumptionsid = consumptionsid;
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

                //计算找零
                double cashBack = double.Parse(this.TxtDiscount.Text) - double.Parse(this.lbReceiveShould.Text);
                this.label4.Text = cashBack <= 0 ? "0" : Math.Round(cashBack, 2).ToString();
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
        private void CashPayment_Load(object sender, EventArgs e)
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
            mb.Btn_Cash.Image = Properties.Resources.现金支付;
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
                this.Btn_Ok.Image = Properties.Resources.确定3;
            }
        }
        // <summary>
        /// 确认提交方法
        /// </summary>
        public void button_ok()
        {
            if (!string.IsNullOrEmpty(this.TxtDiscount.Text))
            {
                string price_fixed = double.Parse(this.TxtDiscount.Text).ToString("0.00");
                Member mb = new Member();
                mb = (Member)this.Owner;
                mb.KeyPreview = true;
                mb.panelChildren.Controls.Remove(this);
                mb.panelInfor.Visible = true;
                mb.lbTitle.Text = "支付信息";
                if (PassValue.payments.Count != 0 && PassValue.payments.Where(payment => payment.method == "cash").FirstOrDefault() != null)
                {
                    mb.lbReceiveActual.Text = (mb.Price_Recive - double.Parse(PassValue.payments.Where(payment => payment.method == "cash").FirstOrDefault().amount) + double.Parse(price_fixed)).ToString("0.00");
                    PassValue.payments.Remove(PassValue.payments.Where(payment => payment.method == "cash").FirstOrDefault());
                }
                //现金支付
                Payment pm = new Payment();
                if (double.Parse(this.TxtDiscount.Text) <= double.Parse(this.lbReceiveShould.Text))
                {
                    pm.amount = this.TxtDiscount.Text;
                }
                else
                {
                    pm.amount = this.lbReceiveShould.Text;
                }
                pm.method = "cash";
                PassValue.payments.Add(pm);

                mb.lbReceiveActual.Text = (mb.Price_Recive + double.Parse(pm.amount)).ToString("0.00");
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
            mb.Btn_Cash.Image = Properties.Resources.现金支付;
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
