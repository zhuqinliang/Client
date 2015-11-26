using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Business;

namespace Client
{
    public partial class PlanOrder : Form
    {
        private string planConsumptionid;
        private HttpAskfor httpReq = new HttpAskfor();

        public PlanOrder(string consumptionid)
        {
            InitializeComponent();
            planConsumptionid = consumptionid;
        }

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
            mb.Btn_Part.Enabled = mb.Btn_Plan.Enabled = mb.Btn_Fixed.Enabled = true;
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

        #region 确认按钮
        /// <summary>
        /// 确认
        /// </summary>
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            button_ok();
        }
        /// <summary>
        /// 确认提交方法
        /// </summary>
        public void button_ok()
        {
            foreach (Control ctl in this.flowLayoutPanel1.Controls)
            {
                if (ctl is CheckBox && ((CheckBox)ctl).Checked)
                {
                    PassValue.Percent = Int32.Parse(ctl.Tag.ToString());
                    if (PassValue.Percent != 0)
                    {
                        Discount ds = new Discount();
                        ds.type = "scheme";
                        ds.scheme = new Scheme();
                        ds.scheme.id=ctl.Name;
                        ds.scheme.percent=PassValue.Percent;
                        PassValue.discounts.Add(ds);
                        PassValue.Infor_payment.discounts = new Discount[PassValue.discounts.Count];
                    }
                    int i = 0;
                    foreach (Discount ds in PassValue.discounts)
                    {
                        PassValue.Infor_payment.discounts[i++] = ds;
                    }
                }
            }

            HttpResult httpResult = httpReq.HttpPatch(string.Format("consumptions/{0}", planConsumptionid), PassValue.Infor_payment);
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
        /// <summary>
        /// 确认按钮鼠标悬停
        /// </summary>
        private void Btn_Ok_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_Ok.Image = Properties.Resources.确定;
        }

        private void Btn_Ok_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_Ok.Image = Properties.Resources.确定2;
        }
        #endregion

        #region 窗体操作和控件的设置
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void PlanOrder_Load(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Focus();
            getInformation();
            this.Btn_Ok.Image = Properties.Resources.确定2;
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

        #region checkbox的点击事件
        /// <summary>
        /// 选择打折项
        /// </summary>
        public void checkbox_Click(object sender, EventArgs e)
        {
            if ((sender as CheckBox).CheckState == CheckState.Unchecked)
            {
                (sender as CheckBox).Checked = false;
            }
            else if ((sender as CheckBox).CheckState == CheckState.Checked)
            {
                foreach (Control ctl in this.flowLayoutPanel1.Controls)
                {
                    if (ctl is CheckBox)
                    {
                        CheckBox chk = ctl as CheckBox;
                        chk.Checked = false;
                    }
                }
                (sender as CheckBox).Checked = true;
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        public void getInformation()
        {
            List<DiscountScheme> personsStatus = httpReq.HttpGet<List<DiscountScheme>>("discount-schemes");
            if (personsStatus != null)
            {
                for (int i = 0; i < personsStatus.Count(); i++)
                {
                    CheckBox cb = new CheckBox();
                    this.flowLayoutPanel1.Controls.Add(cb);
                    cb.Text = personsStatus[i].name;
                    cb.Tag = personsStatus[i].percent;//折扣率
                    cb.Name = personsStatus[i].id;//id
                    cb.Click += new EventHandler(checkbox_Click);
                }
            }
        }
        #endregion

        private void PlanOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button_ok();
            }
        }
    }
}
