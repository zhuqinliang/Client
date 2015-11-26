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
using System.Collections;

namespace Client
{
    public partial class OtherOrder : Form
    {
        private string Otherconsumptionid;
        public OtherOrder(string consumptionid)
        {
            InitializeComponent();
            Otherconsumptionid = consumptionid;
        }

        public ArrayList listreasons = new ArrayList();
        public List<string> reasonid =new List<string>();
        #region 窗体操作和控件的设置
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void OtherOrder_Load(object sender, EventArgs e)
        {
            this.TxtDiscount.Focus();
            this.lbReceiveShould.Text = PassValue.Price_Now;
            this.TxtDiscount.Text = this.lbReceiveShould.Text;
            this.Btn_Ok.Image = Properties.Resources.确定2;
            this.Btn_Ok.Enabled = true;
            this.Btn_Canel.Image = Properties.Resources.取消;
            Payment otherPay = PassValue.payments.Where(p => p.method == "other").FirstOrDefault();
            if (PassValue.payments.Count > 0 && otherPay != null)
            {
                this.TxtDiscount.Text = PassValue.payments.Where(p => p.method == "other").FirstOrDefault().amount;
                this.lbReasons.Text = PassValue.payments.Where(p => p.method == "other").FirstOrDefault().reason.description;
                this.lbReasons.Visible = true;
                this.label4.Visible = false;
            }
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
            mb.Btn_BankCard.Image = Properties.Resources.其他;
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
        #endregion

        #region 确认按钮
        /// <summary>
        /// 确认
        /// </summary>
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtDiscount.Text))
            {
                if (string.IsNullOrEmpty(this.lbReasons.Text))
                {
                    MessageBox.Show("请选择原因!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                if (double.Parse(this.TxtDiscount.Text) > double.Parse(this.lbReceiveShould.Text))
                {
                    MessageBox.Show("输入金额大于应收金额，请确认!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                button_ok();
            }
            else 
            {
                MessageBox.Show("不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        // <summary>
        /// 确认提交方法
        /// </summary>
        public void button_ok()
        {
            if (this.TxtDiscount.Text != null)
            {
                string price_fixed = double.Parse(this.TxtDiscount.Text).ToString("0.00");
                Member mb = new Member();
                mb = (Member)this.Owner;
                mb.KeyPreview = true;
                mb.panelChildren.Controls.Remove(this);
                mb.panelInfor.Visible = true;
                mb.lbTitle.Text = "支付信息";


                if (PassValue.payments.Count != 0 && PassValue.payments.Where(payment => payment.method == "other").FirstOrDefault() != null)
                {
                    mb.lbReceiveActual.Text = (mb.Price_Recive + double.Parse(price_fixed) - double.Parse(PassValue.payments.Where(payment => payment.method == "other").FirstOrDefault().amount)).ToString("0.00");
                    PassValue.payments.Remove(PassValue.payments.Where(payment => payment.method == "other").FirstOrDefault());
                }
                else
                {
                    mb.lbReceiveActual.Text = (mb.Price_Recive + double.Parse(price_fixed)).ToString("0.00");
                }

                //银联卡支付
                Payment pm = new Payment();
                pm.amount = double.Parse(this.TxtDiscount.Text).ToString("0.00");
                pm.method = "other";
                Reasons rs = new Reasons();
                rs.description = this.lbReasons.Text;
                rs.id = reasonid[0];
                pm.reason = rs;
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
                Btn_Ok_Click(sender, e);
            }
        }

        #region 选择其他原因
        /// <summary>
        /// 选择原因
        /// </summary>
        private void panelChoose_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtDiscount.Text))
            {
                Member mb = new Member();
                mb = (Member)this.Owner;
                this.Visible = false;
                PaymentReasons pr = new PaymentReasons();
                pr.Owner = this;
                this.Visible = false;
                pr.TopLevel = false;
                mb.panelChildren.Controls.Add(pr);
                pr.Show();
            }
            else
            {
                Messagebox mb = new Messagebox();
                PassValue.MessageInfor = "其他支付金额不能为空！";
                mb.ShowDialog();
            }
        }
        #endregion

    }
}
