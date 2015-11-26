using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Web.Script.Serialization;
using System.Globalization;
using Business;
using System.Net;
using Kernel;

namespace Client
{
    public partial class Transfer : Form, IView
    {
        private HttpAskfor httpReq = new HttpAskfor();

        public Transfer()
        {
            InitializeComponent();
        }

        #region 窗体的适配和UI调整
        /// <summary>
        /// 窗体居中
        /// </summary>
        private void Transfer_Resize(object sender, EventArgs e)
        {
            this.panel1.Left = (this.Width - this.panel1.Width) / 2;
        }

        /// <summary>
        /// GDI画线
        /// </summary>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel2.ClientRectangle, Color.FromArgb(224, 224, 224), 1, ButtonBorderStyle.Solid, Color.FromArgb(224, 224, 224), 1, ButtonBorderStyle.Solid, Color.FromArgb(224, 224, 224), 1, ButtonBorderStyle.Solid, Color.FromArgb(224, 224, 224), 1, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// 按钮的悬停效果
        /// </summary>
        private void Btn_Transfer_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_Transfer.Image = Properties.Resources.确认交接2;
        }

        private void Btn_Transfer_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_Transfer.Image = Properties.Resources.确认交接;
        }

        private void Btn_Print_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_Print.Image = Properties.Resources.打印交接单2;
        }

        private void Btn_Print_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_Print.Image = Properties.Resources.打印交接单;
        }
        #endregion

        #region 窗体的加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void Transfer_Load(object sender, EventArgs e)
        {
            this.Btn_Transfer.Image = Properties.Resources.确认交接;
            this.Btn_Print.Image = Properties.Resources.打印交接单;
            AddInformation();
        }

        /// <summary>
        /// 加载信息
        /// </summary>
        public void AddInformation()
        {
            Shift personsShifts = httpReq.HttpGet<Shift>(string.Format("shifts/{0}", PassValue.shiftId));

            if (personsShifts != null)
            {
                this.lbName.Text = personsShifts.cashier.name;//姓名
                this.lbSales.Text = personsShifts.revenue;//销售额
                this.lbTurnover.Text = personsShifts.actualSales;//营业额
                DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy-MM-dd hh:mm:ss";
                this.lbLoginTime.Text = Convert.ToDateTime(personsShifts.start, dtFormat).ToString("yyyy-MM-dd HH:mm:ss");//登录时间
                this.lbTransferTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//交接时间
                this.lbCashier.Text = personsShifts.cashier.name;//收银员
                for (int i = 0; i < personsShifts.payments.Count(); i++)
                {
                    string name = "";
                    string value = "0.0";
                    switch (personsShifts.payments[i].method)
                    {
                        case "sign":
                            value = double.Parse(personsShifts.payments[i].amount).ToString("0.00");//签单
                            name = "签单:";
                            break;
                        case "member":
                            value = double.Parse(personsShifts.payments[i].amount).ToString("0.00");//会员
                            name = "会员:";
                            break;
                        case "cash":
                            value = double.Parse(personsShifts.payments[i].amount).ToString("0.00");//现金支付
                            name = "现金支付:";
                            break;
                        case "bankcard":
                            value = double.Parse(personsShifts.payments[i].amount).ToString("0.00");//银联卡
                            name = "银联卡:";
                            break;
                        case "other":
                            value = double.Parse(personsShifts.payments[i].amount).ToString("0.00");
                            name = personsShifts.payments[i].reason.description + ":";
                            break;
                    }
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = name;
                    this.dataGridView1.Rows[index].Cells[1].Value = value;
                }

                int k = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[k].Cells[0].Value = "总付款";
                this.dataGridView1.Rows[k].Cells[1].Value = personsShifts.actualSales;

                for (int j = 0; j < personsShifts.salesInfos.Count(); j++)
                {
                    switch (personsShifts.salesInfos[j].type)
                    {
                        case "drink":
                            this.lbWine.Text =double.Parse(personsShifts.salesInfos[j].amount).ToString("0.00");//酒水
                            break;
                        case "hot-dish":
                            this.lbHotDishes.Text =double.Parse(personsShifts.salesInfos[j].amount).ToString("0.00");//热菜
                            break;
                        case "cold-dish":
                            this.lbColdDishes.Text = double.Parse(personsShifts.salesInfos[j].amount).ToString("0.00");;//冷菜
                            break;
                    }
                }
                this.lbTotal.Text = personsShifts.total;//合计
                for (int n = 0; n < personsShifts.discounts.Count(); n++)
                {
                    switch (personsShifts.discounts[n].type)
                    {
                        case "round":
                            this.label29.Text =double.Parse( personsShifts.discounts[n].amount).ToString("0.00");//抹零
                            break;
                        case "partial":
                            this.label26.Text = double.Parse( personsShifts.discounts[n].amount).ToString("0.00");//部分打折
                            break;
                        case "free":
                            this.label21.Text = double.Parse( personsShifts.discounts[n].amount).ToString("0.00");//免单
                            break;
                        case "whole":
                            this.label17.Text = double.Parse( personsShifts.discounts[n].amount).ToString("0.00");//整单打折
                            break;
                        case "quota":
                            this.label31.Text = double.Parse( personsShifts.discounts[n].amount).ToString("0.00");//定额打折
                            break;
                    }
                }
                this.lbTotalDiscount.Text = personsShifts.offerFree;//总折扣
            }
        }
        #endregion

        #region 窗体的缓冲
        /// <summary>
        /// 开启双缓冲
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion

        #region 确认交接按钮
        /// <summary>
        /// 确认交接
        /// </summary>
        private void Btn_Transfer_Click(object sender, EventArgs e)
        {
            if (PassValue.IsPrintshifts == false)
            {
                MessageBox.Show("请先打印交接班单！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            Shift shift = new Shift();
            shift.end = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
            Employee cashier = new Employee();
            cashier.id = PassValue.shiftId;
            shift.cashier = cashier;

            HttpResult httpResult = httpReq.HttpPatch(string.Format("shifts/{0}", PassValue.shiftId), shift);
            if ((int)httpResult.StatusCode == 0)
            {
                MessageBox.Show(string.Format("{0}{1}", httpResult.StatusDescription, httpResult.OtherDescription), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if ((int)httpResult.StatusCode >= 200 && (int)httpResult.StatusCode < 300)
            {
                MessageBox.Show("交接成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                httpResult = httpReq.HttpDelete(string.Format("auth/tokens/{0}", PassValue.tokenid));
                if ((int)httpResult.StatusCode == 0)
                {
                    MessageBox.Show(string.Format("{0}{1}", httpResult.StatusDescription, httpResult.OtherDescription), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                string path = Application.StartupPath + @"/点菜100.exe";
                System.Diagnostics.Process.Start(path);
                Application.Exit();
            }
            else if ((int)httpResult.StatusCode == 401)
            {
                LoginBusiness lg = new LoginBusiness();
                lg.LoginAgain();
                return;
            }
            else
            {
                if ((int)httpResult.StatusCode == 403)
                {
                    MessageBox.Show("有反结算账单未结算，请结算！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("未知错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }
        
        #endregion

        #region 打印
        /// <summary>
        /// 打印交接单
        /// </summary>
        private void Btn_Print_Click(object sender, EventArgs e)
        {
            this.lbMessage.Text = "正在打印请稍后。。。";
            Application.DoEvents();
            Task task = new Task();
            task.kind = "shift";
            Shift shift = new Shift();
            shift.id =PassValue.shiftId;
            task.shift = shift;

            HttpResult httpResult = httpReq.HttpPost("printing/tasks", task);
            if ((int)httpResult.StatusCode == 401)
            {
                this.lbMessage.Text = "";
                LoginBusiness lg = new LoginBusiness();
                lg.LoginAgain();
                return;
            }
            else if ((int)httpResult.StatusCode == 0)
            {
                MessageBox.Show(string.Format("{0}{1}", httpResult.StatusDescription, httpResult.OtherDescription), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if ((int)httpResult.StatusCode >= 200 && (int)httpResult.StatusCode < 300)
            {
                MessageBox.Show("打印成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                if ((int)httpResult.StatusCode == 410)
                {
                    MessageBox.Show("请检查打印机是否连接！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("未知错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            PassValue.IsPrintshifts = true;
            this.lbMessage.Text = "";
        }
        #endregion

        public void Active()
        {

        }

        public string GetName()
        {
            return "交接班";
        }
    }
}
