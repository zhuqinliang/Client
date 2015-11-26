using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business;
using e7;
using System.Web.Script.Serialization;
using System.Threading;

namespace Client
{
    public partial class MemberCard : Form
    {
        private HttpAskfor httpReq = new HttpAskfor();
        private HttpAskfor httpReqMain = new HttpAskfor();
        DeivceE7Impl E7 = new DeivceE7Impl();
        E7Device e7d = new E7Device();
        GetInformation gi = new GetInformation();
        Thread t = null;
        private string currentBalance = "0";
        private MMember card = null;
        public delegate void SetBotton(Button btn, bool isenable);
        public delegate void SetTextLabel(Label lb, string str);
        public delegate void SetTextTextBox(TextBox tb, string str);
        private string memberConsumptionid;
        private bool isReadSuccess = false;
        private string _memberid = "";

        public MemberCard(string _consumptionid)
        {
            InitializeComponent();
            memberConsumptionid = _consumptionid;
        }

        public void SetButtonn(Button btn, bool isenable)
        {
            try
            {
                SetBotton mi = new SetBotton(SetButton);
                Invoke(mi, new object[] { btn, isenable });
            }
            catch (Exception)
            {

            }
        }

        public void SetLabel(Label lb, string labelText)
        {
            try
            {
                SetTextLabel mi = new SetTextLabel(SetLbText);
                Invoke(mi, new object[] { lb, labelText });
            }
            catch (Exception)
            {

            }
        }

        public void SetTextBox(TextBox tb, string textText)
        {
            try
            {
                SetTextTextBox mi = new SetTextTextBox(SetTxtText);
                Invoke(mi, new object[] { tb, textText });
            }
            catch (Exception)
            {

            }
        }

        public void SetButton(Button btn, bool isenable)
        {
            this.Btn_Member.Enabled = isenable;
        }

        public void SetTxtText(TextBox tb, string textText)
        {
            tb.Text = textText;
        }

        public void SetLbText(Label lb, string labelText)
        {
            lb.Text = labelText;
        }

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

        #region 窗体操作和控件的设置
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void MemberCard_Load(object sender, EventArgs e)
        {
            this.lbReceiveShould.Text = PassValue.Price_Now;
            this.TxtDiscount.Text = PassValue.Price_Now;
            this.Btn_Ok.Image = Properties.Resources.确定3;
            this.Btn_Ok.Enabled = false;
            this.Btn_Canel.Image = Properties.Resources.取消;
            t = new Thread(new ThreadStart(ReadCard));
            isReadSuccess = true;
            t.Start();

            if (this.TxtDiscount.Text.Length > 0)
            {
                this.Btn_Ok.Image = Properties.Resources.确定2;
                this.Btn_Ok.Enabled = true;
            }

            this.TxtDiscount.Focus();
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
            mb.Btn_MemberCard.Image = Properties.Resources.会员卡;
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
            mb.Btn_MemberCard.Image = Properties.Resources.会员卡;
            mb.panelChildren.Visible = true;

            //try
            //{
            //    t.Suspend();
            //    t.Abort();
            //}
            //catch { }
            //E7.closeDevice();

            mb.AddInformation();
            this.Close();

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

        private void Btn_Member_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Txt_MemberCard.Text))
            {
                MMember consumption = httpReqMain.HttpGet<MMember>(string.Format("member/card?key={0}", this.Txt_MemberCard.Text));
                if (consumption != null)
                {
                    if (consumption.holder != null && consumption.balance != null)
                    {
                        currentBalance = consumption.balance;
                        PassValue.MemberCardPwd = consumption.cardPassword;
                        _memberid = consumption.memberid;
                        this.txtAmount.Text = consumption.balance;
                        this.txtDis.Text = consumption.discount + "折";
                        this.txtMobile.Text = consumption.phone;
                        this.txtUserName.Text = consumption.holder.name;
                    }
                    else
                    {
                        MessageBox.Show("无会员卡信息！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("无会员卡信息！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("没有找到会员信息！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        //读卡
        private void ReadCard()
        {
            try
            {
                //while (isReadSuccess)
                //{
                SetLabel(this.label2, "");
                //SetButtonn(this.Btn_Member, false);
                //打开设备
                string openResult = E7.openDevice();
                while (int.Parse(openResult) <= 0)
                {
                    SetLabel(this.label2, "没有找到设备！");
                    //SetButtonn(this.Btn_Member, true);
                    Thread.Sleep(1000);
                    openResult = E7.openDevice();
                }

                //找卡
                string cardnum = E7.findCard();
                while (string.IsNullOrEmpty(cardnum))
                {
                    SetLabel(this.label2, "没有找到卡片！");
                    E7.DisplayLcd("请刷卡或把卡放在卡槽上");
                    //SetButtonn(this.Btn_Member, true);
                    Thread.Sleep(1000);
                    cardnum = E7.findCard();
                }
                SetLabel(this.label2, "");

                //读卡
                string cardid = E7.readCard().Trim();
                while (string.IsNullOrEmpty(cardid))
                {
                    Thread.Sleep(1000);
                    cardid = E7.readCard().Trim();
                }

                cardid = e7d.Encode(cardid);
                //鸣叫
                E7.beep(6);
                SetTextBox(this.Txt_MemberCard, cardid);
                E7.DisplayLcd("读卡成功！");
                MMember consumption = httpReq.HttpGet<MMember>(string.Format("member/card?key={0}", cardid));
                if (consumption != null)
                {
                    if (consumption.holder != null && consumption.balance != null)
                    {
                        currentBalance = consumption.balance;
                        _memberid = consumption.memberid;
                        PassValue.MemberCardPwd = consumption.cardPassword;
                        SetTextBox(this.txtAmount, consumption.balance);
                        SetTextBox(this.txtDis, consumption.discount + "折");
                        SetTextBox(this.txtMobile, consumption.phone);
                        SetTextBox(this.txtUserName, consumption.holder.name);
                        SetButtonn(this.Btn_Member, true);
                        //break;
                    }
                    else
                    {
                        SetLabel(this.label2, "没有找到卡信息！");
                    }
                }
                else
                {
                    SetLabel(this.label2, "没有找到卡信息！");
                }


                //SetButtonn(this.Btn_Member, true);

                //Thread.Sleep(2000);
                //}
            }
            catch (Exception E)
            {
            }
            finally
            {
                E7.closeDevice();
            }

        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (double.Parse(this.TxtDiscount.Text) > double.Parse(this.lbReceiveShould.Text))
            {
                MessageBox.Show("输入金额大于应收金额，请确认!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //提交结账信息
            if (string.IsNullOrEmpty(this.TxtDiscount.Text))
            {
                MessageBox.Show("金额未输入请确认！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (string.IsNullOrEmpty(this.Txt_MemberCard.Text))
            {
                MessageBox.Show("会员卡号未输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (double.Parse(currentBalance) < double.Parse(this.TxtDiscount.Text))
            {
                MessageBox.Show("会员卡账户余额不足，请充值，余额为" + currentBalance + "!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            PassValue.Infor_payment.card = new MMember() { memberid = _memberid };
            string price_fixed = double.Parse(this.TxtDiscount.Text).ToString("0.00");
            Member mb = new Member();
            mb = (Member)this.Owner;
            mb.KeyPreview = true;
            mb.panelChildren.Controls.Remove(this);
            mb.panelInfor.Visible = true;
            mb.lbTitle.Text = "支付信息";
            //会员卡支付
            //如果前面已经含有会员卡的支付信息需要把其移除掉
            if (PassValue.payments.Count != 0 && PassValue.payments.Where(payment => payment.method == "member").FirstOrDefault() != null)
            {
                mb.lbReceiveActual.Text = (mb.Price_Recive - double.Parse(PassValue.payments.Where(payment => payment.method == "member").FirstOrDefault().amount) + double.Parse(price_fixed)).ToString("0.00");
                PassValue.payments.Remove(PassValue.payments.Where(payment => payment.method == "member").FirstOrDefault());
            }

            Payment pm = new Payment();
            pm.amount = this.TxtDiscount.Text;
            pm.method = "member";
            PassValue.payments.Add(pm);

            
            mb.lbReceiveActual.Text = (mb.Price_Recive + double.Parse(price_fixed)).ToString("0.00");
            mb.panelChildren.Visible = true;
            Form_Esc();
        }

        private void TxtDiscount_Enter(object sender, EventArgs e)
        {
            //Member mb = new Member();
            //mb = (Member)this.Owner;
            //mb.KeyPreview = false;
        }

        private void TxtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (this.TxtDiscount.Text != "" && this.TxtDiscount.Text != null )
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

        private void MemberCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            //isReadSuccess = false;
            //E7.clearLCDShow();
            //E7.closeDevice();
            try
            {
                //t.Suspend();
                t.Abort();
            }
            catch
            {
            }
            //finally
            //{
            //    E7.closeDevice();
            //}

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
        #endregion

        private void Txt_MemberCard_TextChanged(object sender, EventArgs e)
        {
            currentBalance = "0";
            PassValue.MemberCardPwd = "";
            this.txtAmount.Text = "";
            this.txtDis.Text = "";
            this.txtMobile.Text = "";
            this.txtUserName.Text = "";
        }

        private void Txt_MemberCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Btn_Member_Click(sender, e);
            }
}
    }
}
