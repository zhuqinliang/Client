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
using System.Net;
using System.Collections;
using WpfControls;

namespace Client
{
    public partial class Member : Form
    {
        public Member()
        {
            InitializeComponent();
        }

        private HttpAskfor httpReq = new HttpAskfor();
        public string memberConsumptionsid;//消费ID
        public double Price_Recive = 0.00;//实际收的现金
        public Member(string p_ConsumptionsId)
            : this()
        {
            PassValue.consumptionid = p_ConsumptionsId;
            memberConsumptionsid = p_ConsumptionsId;
        }

        ILoadData m_ILoadData;
        public Member(string p_ConsumptionsId, ILoadData p_LoadData)
            : this(p_ConsumptionsId)
        {
            m_ILoadData = p_LoadData;
        }

        #region 窗体的加载
        /// <summary>
        /// 窗体的加载
        /// </summary>
        private void Member_Load(object sender, EventArgs e)
        {
            SetUp();
            this.KeyPreview = true;
            this.Btn_All.Enabled = this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = this.Btn_Plan.Enabled = true;
            AddInformation();
            foreach (Control ctl in this.Controls)
            {
                if (ctl is LineControl.LineControl)
                {
                    if ((ctl as LineControl.LineControl).lbInformation.ToString().Substring(0, 4) == "整单打折")
                    {
                        this.Btn_All.Image = Properties.Resources.整单打折3;
                        this.Btn_Part.Image = Properties.Resources.部分打折2;
                        this.Btn_Plan.Image = Properties.Resources.方案打折2;
                        this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                        this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = this.Btn_Plan.Enabled = false;
                    }
                    else if ((ctl as LineControl.LineControl).lbInformation.ToString().Substring(0, 4) == "部分打折")
                    {
                        this.Btn_All.Image = Properties.Resources.整单打折2;
                        this.Btn_Part.Image = Properties.Resources.部分打折3;
                        this.Btn_Plan.Image = Properties.Resources.方案打折2;
                        this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                        this.Btn_All.Enabled = this.Btn_Fixed.Enabled = this.Btn_Plan.Enabled = false;
                    }
                    else if ((ctl as LineControl.LineControl).lbInformation.ToString().Substring(0, 4) == "方案打折")
                    {
                        this.Btn_All.Image = Properties.Resources.整单打折2;
                        this.Btn_Part.Image = Properties.Resources.部分打折2;
                        this.Btn_Plan.Image = Properties.Resources.方案打折3;
                        this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                        this.Btn_All.Enabled = this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = false;
                    }
                    else if ((ctl as LineControl.LineControl).lbInformation.ToString().Substring(0, 4) == "定额打折")
                    {
                        this.Btn_All.Image = Properties.Resources.整单打折2;
                        this.Btn_Part.Image = Properties.Resources.部分打折2;
                        this.Btn_Plan.Image = Properties.Resources.方案打折2;
                        this.Btn_Fixed.Image = Properties.Resources.定额打折3;
                        this.Btn_All.Enabled = this.Btn_Part.Enabled = this.Btn_Plan.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void SetUp()
        {
            this.Btn_All.Image = Properties.Resources.整单打折;
            this.Btn_Part.Image = Properties.Resources.部分打折;
            this.Btn_Plan.Image = Properties.Resources.方案打折;
            this.Btn_Fixed.Image = Properties.Resources.定额打折;
            this.Btn_Maling.Image = Properties.Resources.摸零;
            this.Btn_Free.Image = Properties.Resources.免单;
            this.Btn_MemberCard.Image = Properties.Resources.会员卡;
            this.Btn_BankCard.Image = Properties.Resources.银联卡;
            this.Btn_Sign.Image = Properties.Resources.签单;
            this.Btn_Other.Image = Properties.Resources.其他;
            this.Btn_Cash.Image = Properties.Resources.现金支付;
            this.Btn_Print.Image = Properties.Resources.打印对账单;
            this.Btn_Enter.Image = Properties.Resources.确定2;
        }
        /// <summary>
        /// 加载信息
        /// </summary>
        public void AddInformation()
        {
            var personsConsumption = httpReq.HttpGet<Consumption>(string.Format("consumptions/{0}", PassValue.consumptionid));

            int lccount = 0;
            if (personsConsumption != null)
            {
                this.lbSubtotal.Text = personsConsumption.subtotal;
                this.lbSaving.Text = personsConsumption.savings;
                this.lbTotal.Text = personsConsumption.total;
                PassValue.Price_All = this.lbSubtotal.Text;//总金额传递给全局变量
                PassValue.Price_Now = (double.Parse(this.lbTotal.Text) - double.Parse(this.lbReceiveActual.Text)).ToString("0.00"); ;//应付价格传递给全局变量

                for (int j = 0; j < personsConsumption.details.Count(); j++)
                {
                    if (personsConsumption.details[j].discountable == true)
                    {
                        if (!PassValue.listItemIDBefore.Contains(personsConsumption.details[j].item.id)) 
                        {
                            PassValue.listItemIDBefore.Add(personsConsumption.details[j].item.id);
                        }
                    }
                }


                if (personsConsumption.discounts != null)
                {
                    for (int i = 0; i < personsConsumption.discounts.Count(); i++)
                    {
                        LineControl.LineControl lc = new LineControl.LineControl();
                        switch (personsConsumption.discounts[i].type)
                        {
                            case "whole":
                                lc.lbInformation.Text = "整单打折:" + "   " + personsConsumption.discounts[i].percent.ToString() + "%";
                                break;
                            case "partial":
                                //获取目前的打折菜品
                                PassValue.listItemID.Clear();
                                string percent="";
                                if (personsConsumption.discounts[i].items != null)
                                {
                                    foreach (DiscountItem di in personsConsumption.discounts[i].items)
                                    {
                                        percent = di.percent.ToString();
                                        PassValue.Percent = int.Parse(percent);
                                        PassValue.listItemID.Add(di.id);
                                    }
                                }

                                lc.lbInformation.Text = "部分打折:" + "   " + PassValue.listItemID.Count.ToString() + "份菜 折扣:" + percent + "%";

                                break;
                            case "scheme":
                                int intpercent = personsConsumption.discounts[i].scheme.percent;
                                lc.lbInformation.Text = "方案打折:" + "   " + intpercent.ToString() + "折";
                                break;
                            case "quota":
                                lc.lbInformation.Text = "定额打折:" + "   " + personsConsumption.discounts[i].amount.ToString();
                                break;
                            case "round":
                                lc.lbInformation.Text = "抹零:" + "   " + personsConsumption.discounts[i].amount.ToString();
                                break;
                            case "free":
                                lc.lbInformation.Text = "免单:" + "   " + personsConsumption.discounts[i].amount.ToString();
                                break;
                        }

                        lc.picClose.Click += new EventHandler(addorderclose_Click);//去除事件
                        this.panelChildren.Controls.Add(lc);
                        lc.Location = new System.Drawing.Point(0, lc.Height * lccount + this.panelInfor.Height + 20);
                        lccount++;
                    }
                }
            }
            if (PassValue.payments.Count != 0)
            {
                Price_Recive = 0;//初始化应收的价格
                for (int c = 0; c < PassValue.payments.Count(); c++)
                {
                    LineControl.LineControl lc = new LineControl.LineControl();

                    switch (PassValue.payments[c].method)
                    {
                        case "bankcard":
                            lc.lbInformation.Text = "银联卡:" + "   " + PassValue.payments[c].amount;
                            break;
                        case "sign":
                            lc.lbInformation.Text = "签单:" + "   " + PassValue.payments[c].amount;
                            break;
                        case "other":
                            lc.lbInformation.Text = "其他:" + "   " + PassValue.payments[c].amount;
                            break;
                        case "cash":
                            lc.lbInformation.Text = "现金支付:" + "   " + PassValue.payments[c].amount;
                            break;
                        case "member":
                            lc.lbInformation.Text = "会员卡:" + "   " + PassValue.payments[c].amount;
                            break;
                    }
                    Price_Recive += double.Parse(PassValue.payments[c].amount);
                    lc.Location = new System.Drawing.Point(0, lc.Height * lccount + this.panelInfor.Height + 20);
                    lc.picClose.Click += new EventHandler(addorderclose_Click);//去除事件
                    this.panelChildren.Controls.Add(lc);
                    lccount++;
                }
            }

            //加载完毕之后判断是否要屏蔽现金支付方式
            if (double.Parse(lbTotal.Text) <= double.Parse(lbReceiveActual.Text))
            {
                this.Btn_All.Enabled = false;
                this.Btn_Part.Enabled = false;
                this.Btn_Plan.Enabled = false;
                this.Btn_Fixed.Enabled = false;
                this.Btn_Maling.Enabled = false;
                this.Btn_Free.Enabled = false;
                this.Btn_MemberCard.Enabled = false;
                this.Btn_BankCard.Enabled = false;
                this.Btn_Sign.Enabled = false;
                this.Btn_Other.Enabled = false;
                this.Btn_Cash.Enabled = false;


                this.Btn_All.Image = Properties.Resources.整单打折2;
                this.Btn_Part.Image = Properties.Resources.部分打折2;
                this.Btn_Plan.Image = Properties.Resources.方案打折2;
                this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                this.Btn_Maling.Image = Properties.Resources.抹零2;
                this.Btn_Free.Image = Properties.Resources.免单2;
                this.Btn_MemberCard.Image = Properties.Resources.会员卡2;
                this.Btn_BankCard.Image = Properties.Resources.银联卡2;
                this.Btn_Sign.Image = Properties.Resources.签单2;
                this.Btn_Other.Image = Properties.Resources.其他2;
                this.Btn_Cash.Image = Properties.Resources.现金支付2;

                if (PassValue.payments == null) //如果没有支付信息，开放其他按钮
                {
                    this.Btn_Other.Enabled = true;
                    this.Btn_Other.Image = Properties.Resources.其他;
                }
                else if (PassValue.payments.Count() == 0)
                {
                    this.Btn_Other.Enabled = true;
                    this.Btn_Other.Image = Properties.Resources.其他;
                }
            }
            else
            {
                //初始化
                this.Btn_All.Enabled = true;
                this.Btn_Part.Enabled = true;
                this.Btn_Plan.Enabled = true;
                this.Btn_Fixed.Enabled = true;
                this.Btn_Maling.Enabled = true;
                this.Btn_Free.Enabled = true;
                this.Btn_MemberCard.Enabled = true;
                this.Btn_BankCard.Enabled = true;
                this.Btn_Sign.Enabled = true;
                this.Btn_Other.Enabled = true;
                this.Btn_Cash.Enabled = true;

                this.Btn_All.Image = Properties.Resources.整单打折;
                this.Btn_Part.Image = Properties.Resources.部分打折;
                this.Btn_Plan.Image = Properties.Resources.方案打折;
                this.Btn_Fixed.Image = Properties.Resources.定额打折;
                this.Btn_Maling.Image = Properties.Resources.摸零;
                this.Btn_Free.Image = Properties.Resources.免单;
                this.Btn_MemberCard.Image = Properties.Resources.会员卡;
                this.Btn_BankCard.Image = Properties.Resources.银联卡;
                this.Btn_Sign.Image = Properties.Resources.签单;
                this.Btn_Other.Image = Properties.Resources.其他;
                this.Btn_Cash.Image = Properties.Resources.现金支付;
                this.Btn_Print.Image = Properties.Resources.打印对账单;
                this.Btn_Enter.Image = Properties.Resources.确定2;

                //要根据现有的打折和折扣信息进行按钮的控制
                //①只要有现金的支付的话，打折信息就不能使用了
                if (PassValue.payments.Count > 0)
                {
                    this.Btn_All.Enabled = false;
                    this.Btn_Part.Enabled = false;
                    this.Btn_Plan.Enabled = false;
                    this.Btn_Fixed.Enabled = false;
                    this.Btn_Maling.Enabled = false;
                    this.Btn_Free.Enabled = false;
                    this.Btn_All.Image = Properties.Resources.整单打折2;
                    this.Btn_Part.Image = Properties.Resources.部分打折2;
                    this.Btn_Plan.Image = Properties.Resources.方案打折2;
                    this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                    this.Btn_Maling.Image = Properties.Resources.抹零2;
                    this.Btn_Free.Image = Properties.Resources.免单2;
                }

                //②根据折扣信息来处理
                if (personsConsumption.discounts == null)
                {
                    return;
                }
                if (personsConsumption.discounts.Count() > 0)
                {
                    foreach (Discount item in personsConsumption.discounts)
                    {
                        switch (item.type)
                        {
                            case "whole":
                                this.Btn_Part.Image = Properties.Resources.部分打折2;
                                this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                                this.Btn_Plan.Image = Properties.Resources.方案打折2;
                                this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = this.Btn_Plan.Enabled = false;
                                break;
                            case "partial":
                                this.Btn_All.Image = Properties.Resources.整单打折2;
                                this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                                this.Btn_Plan.Image = Properties.Resources.方案打折2;
                                this.Btn_All.Enabled = this.Btn_Fixed.Enabled = this.Btn_Plan.Enabled = false;
                                break;
                            case "scheme":
                                this.Btn_All.Image = Properties.Resources.整单打折2;
                                this.Btn_Part.Image = Properties.Resources.部分打折2;
                                this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                                this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = this.Btn_All.Enabled = false;
                                break;
                            case "quota":
                                this.Btn_All.Image = Properties.Resources.整单打折2;
                                this.Btn_Part.Image = Properties.Resources.部分打折2;
                                this.Btn_Plan.Image = Properties.Resources.方案打折2;
                                this.Btn_All.Enabled = this.Btn_Part.Enabled = this.Btn_Plan.Enabled = false;
                                break;
                            case "round":
                                this.Btn_All.Image = Properties.Resources.整单打折2;
                                this.Btn_Part.Image = Properties.Resources.部分打折2;
                                this.Btn_Plan.Image = Properties.Resources.方案打折2;
                                this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                                this.Btn_All.Enabled = this.Btn_Part.Enabled = this.Btn_Plan.Enabled = this.Btn_Fixed.Enabled = false;
                                break;
                            case "free":
                                this.Btn_All.Image = Properties.Resources.整单打折2;
                                this.Btn_Part.Image = Properties.Resources.部分打折2;
                                this.Btn_Plan.Image = Properties.Resources.方案打折2;
                                this.Btn_Fixed.Image = Properties.Resources.定额打折2;
                                this.Btn_All.Enabled = this.Btn_Part.Enabled = this.Btn_Plan.Enabled = this.Btn_Fixed.Enabled = false;
                                break;
                        }
                    }

                }
            }
        }
        #endregion

        #region 窗体的关闭
        /// <summary>
        /// 窗体的关闭
        /// </summary>
        private void picClose_Click(object sender, EventArgs e)
        {
            PassValue.payments.Clear();
            this.Close();
        }

        public void addorderclose_Click(object sender, EventArgs e)
        {
            SetUp();
            var personsConsumption = httpReq.HttpGet<Consumption>(string.Format("consumptions/{0}", PassValue.consumptionid));

            PassValue.discounts.Clear();
            PassValue.Infor_payment = new PatchOrders();
            if (personsConsumption.discounts != null)
            {
                PassValue.discounts = personsConsumption.discounts.ToList();
            }
            Discount discount = new Discount();
            Payment payment = new Payment();
            LineControl.LineControl lclc = ((PictureBox)sender).Parent as LineControl.LineControl;
            switch (lclc.lbInformation.Text.Split(':')[0].Trim())
            {
                case "整单打折":
                    discount = personsConsumption.discounts.Where(persons => persons.type.Equals("whole")).FirstOrDefault();
                    PassValue.discounts.Remove(discount);
                    this.Btn_Part.Enabled = this.Btn_Plan.Enabled = this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = true;
                    break;
                case "部分打折":
                    discount = personsConsumption.discounts.Where(persons => persons.type.Equals("partial")).FirstOrDefault();
                    PassValue.discounts.Remove(discount);
                    this.Btn_Part.Enabled = this.Btn_Plan.Enabled = this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = true;
                    PassValue.listItemID.Clear();
                    PassValue.listItemIDBefore.Clear();
                    PassValue.Percent = 0;
                    break;
                case "方案打折":
                    discount = personsConsumption.discounts.Where(persons => persons.type.Equals("scheme")).FirstOrDefault();
                    PassValue.discounts.Remove(discount);
                    this.Btn_Part.Enabled = this.Btn_Plan.Enabled = this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = true;
                    break;
                case "定额打折":
                    discount = personsConsumption.discounts.Where(persons => persons.type.Equals("quota")).FirstOrDefault();
                    PassValue.discounts.Remove(discount);
                    this.Btn_Part.Enabled = this.Btn_Plan.Enabled = this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = true;
                    break;
                case "抹零":
                    discount = personsConsumption.discounts.Where(persons => persons.type.Equals("round")).FirstOrDefault();
                    PassValue.discounts.Remove(discount);
                    this.Btn_Part.Enabled = this.Btn_Plan.Enabled = this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = true;
                    break;
                case "免单":
                    discount = personsConsumption.discounts.Where(persons => persons.type.Equals("free")).FirstOrDefault();
                    PassValue.discounts.Remove(discount);
                    this.Btn_Part.Enabled = this.Btn_Plan.Enabled = this.Btn_Part.Enabled = this.Btn_Fixed.Enabled = true;
                    break;



                case "银联卡":
                    payment = PassValue.payments.Where(payments => payments.method.Equals("bankcard")).FirstOrDefault();
                    PassValue.payments.Remove(payment);
                    PassValue.Price_Now = (double.Parse(this.lbTotal.Text) + double.Parse(payment.amount)).ToString("0.00"); //应付价格传递给全局变量
                    Price_Recive = Price_Recive - double.Parse(payment.amount);
                    this.lbReceiveActual.Text = Price_Recive.ToString();
                    break;
                case "现金支付":
                    payment = PassValue.payments.Where(payments => payments.method.Equals("cash")).FirstOrDefault();
                    PassValue.payments.Remove(payment);
                    PassValue.Price_Now = (double.Parse(this.lbTotal.Text) + double.Parse(payment.amount)).ToString("0.00"); //应付价格传递给全局变量
                    Price_Recive = Price_Recive - double.Parse(payment.amount);
                    this.lbReceiveActual.Text = Price_Recive.ToString();
                    break;
                case "会员卡":
                    payment = PassValue.payments.Where(payments => payments.method.Equals("member")).FirstOrDefault();
                    PassValue.payments.Remove(payment);
                    PassValue.Price_Now = (double.Parse(this.lbTotal.Text) + double.Parse(payment.amount)).ToString("0.00"); //应付价格传递给全局变量
                    Price_Recive = Price_Recive - double.Parse(payment.amount);
                    this.lbReceiveActual.Text = Price_Recive.ToString();
                    break;
                case "签单":
                    payment = PassValue.payments.Where(payments => payments.method.Equals("sign")).FirstOrDefault();
                    PassValue.payments.Remove(payment);
                    PassValue.Price_Now = (double.Parse(this.lbTotal.Text) + double.Parse(payment.amount)).ToString("0.00"); //应付价格传递给全局变量
                    Price_Recive = Price_Recive - double.Parse(payment.amount);
                    this.lbReceiveActual.Text = Price_Recive.ToString();
                    break;
                case "其他":
                    payment = PassValue.payments.Where(payments => payments.method.Equals("other")).FirstOrDefault();
                    PassValue.payments.Remove(payment);
                    PassValue.Price_Now = (double.Parse(this.lbTotal.Text) + double.Parse(payment.amount)).ToString("0.00"); //应付价格传递给全局变量
                    Price_Recive = Price_Recive - double.Parse(payment.amount);
                    this.lbReceiveActual.Text = Price_Recive.ToString();
                    break;
            }
            if (PassValue.discounts != null)
            {
                PassValue.Infor_payment.discounts = PassValue.discounts.ToArray();
                HttpResult httpResult = httpReq.HttpPatch(string.Format("consumptions/{0}", PassValue.consumptionid), PassValue.Infor_payment);
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
            }
            this.panelChildren.Controls.Clear();
            AddInformation();
        }
        #endregion

        #region 整单打折
        /// <summary>
        /// 整单打折
        /// </summary>
        public void Btn_All_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_All.Image = Properties.Resources.整单打折3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
            
            AllOrder ao = new AllOrder(memberConsumptionsid); 
            ao.Owner = this;
            ao.TopLevel = false;
            ao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(ao);
            ao.Show();
            this.lbTitle.Text = "整单打折";
        }
        #endregion

        private void Member_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.DarkGray, 1, ButtonBorderStyle.Solid, Color.DarkGray, 1, ButtonBorderStyle.Solid, Color.DarkGray, 1, ButtonBorderStyle.Solid, Color.DarkGray, 1, ButtonBorderStyle.Solid);//绘制边框颜色
        }

        #region 部分打折
        /// <summary>
        /// 部分打折
        /// </summary>
        public void Btn_Part_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_Part.Image = Properties.Resources.部分打折3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
          
            PartOrder po = new PartOrder(memberConsumptionsid);
            po.Owner = this;
            po.TopLevel = false;
            po.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(po);
            po.Show();
            this.lbTitle.Text = "部分打折";
        }
        #endregion

        #region 方案打折
        /// <summary>
        /// 方案打折
        /// </summary>
        public void Btn_Plan_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_Plan.Image = Properties.Resources.方案打折3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
         
            PlanOrder po = new PlanOrder(memberConsumptionsid);
            po.Owner = this;
            po.TopLevel = false;
            po.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(po);
            po.Show();
            this.lbTitle.Text = "方案打折";
        }
        #endregion

        #region 定额打折
        /// <summary>
        /// 定额打折
        /// </summary>
        public void Btn_Fixed_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_Fixed.Image = Properties.Resources.定额打折3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
            
            FixedOrder fo = new FixedOrder(memberConsumptionsid);
            fo.Owner = this;
            fo.TopLevel = false;
            fo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(fo);
            fo.Show();
            this.lbTitle.Text = "定额打折";
        }
        #endregion

        #region 抹零
        /// <summary>
        /// 抹零
        /// </summary>
        public void Btn_Maling_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_Maling.Image = Properties.Resources.抹零3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
            

            MalingOrder mo = new MalingOrder(memberConsumptionsid);
            mo.Owner = this;
            mo.TopLevel = false;
            mo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(mo);
            mo.Show();
            this.lbTitle.Text = "抹零";
        }
        #endregion

        #region 免单
        /// <summary>
        /// 免单
        /// </summary>
        public void Btn_Free_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_Free.Image = Properties.Resources.免单3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
            

            FreeOrder fo = new FreeOrder(memberConsumptionsid);
            fo.Owner = this;
            fo.TopLevel = false;
            fo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(fo);
            fo.Show();
            this.lbTitle.Text = "免单";
        }
        #endregion

        #region 现金支付
        /// <summary>
        /// 现金支付
        /// </summary>
        private void Btn_Cash_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_Cash.Image = Properties.Resources.现金支付3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
            

            CashPayment cp = new CashPayment(memberConsumptionsid);
            cp.Owner = this;
            cp.TopLevel = false;
            cp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(cp);
            cp.Show();
            this.lbTitle.Text = "现金支付";
        }
        #endregion

        #region 数字快捷键
        /// <summary>
        /// 数字快捷键
        /// </summary>
        private void Member_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D0)
            {
                Btn_Cash_Click(sender,e);
            }
            if (e.Control && e.KeyCode == Keys.D1)
            {
                Btn_All_Click(sender, e);
            } 
            if (e.Control && e.KeyCode == Keys.D2)
            {
                Btn_Part_Click(sender, e);
            } 
            if (e.Control && e.KeyCode == Keys.D3)
            {
                Btn_Plan_Click(sender, e);
            } 
            if (e.Control && e.KeyCode == Keys.D4)
            {
                Btn_Fixed_Click(sender, e);
            } 
            if (e.Control && e.KeyCode == Keys.D5)
            {
                Btn_Maling_Click(sender, e);
            } 
            if (e.Control && e.KeyCode == Keys.D6)
            {
                Btn_Free_Click(sender, e);
            } 
            if (e.Control && e.KeyCode == Keys.D7)
            {
                Btn_BankCard_Click(sender, e);
            } 
            if (e.Control && e.KeyCode == Keys.D8)
            {
                Btn_Sign_Click(sender, e);
            } 
            if (e.Control && e.KeyCode == Keys.D9)
            {
                Btn_Other_Click(sender, e);
            }
        }

        #endregion

        #region 会员卡
        /// <summary>
        /// 会员卡
        /// </summary>
        private void Btn_MemberCard_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_MemberCard.Image = Properties.Resources.会员卡3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
           

            MemberCard mc = new MemberCard(memberConsumptionsid);
            mc.Owner = this;
            mc.TopLevel = false;
            mc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(mc);
            mc.Show();
            this.lbTitle.Text = "会员卡";
        }
        #endregion

        #region 银联卡
        /// <summary>
        /// 银联卡
        /// </summary>
        private void Btn_BankCard_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_BankCard.Image = Properties.Resources.银联卡3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
            
            BankCard bc = new BankCard(memberConsumptionsid);
            bc.Owner = this;
            bc.TopLevel = false;
            bc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(bc);
            bc.Show();
            this.lbTitle.Text = "银联卡";
        }
        #endregion

        #region 提交支付信息(完成结账)
        /// <summary>
        /// 确认按钮
        /// </summary>
        private void Btn_Enter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认结账吗？", "订单操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var personsConsumption = httpReq.HttpGet<Consumption>(string.Format("consumptions/{0}", PassValue.consumptionid));

                //实收金额
                string price_fixed = this.lbReceiveActual.Text;
                //应收金额
                string total = this.lbTotal.Text;
                PassValue.Infor_payment.paid = PassValue.Infor_payment.closed = true;
                PassValue.Infor_payment.payments = PassValue.payments.ToArray();
                PassValue.Infor_payment.discounts = personsConsumption.discounts;
                if (double.Parse(total) - double.Parse(price_fixed) <= 0.00)
                {
                    //判断有没有支付信息
                    if (PassValue.payments.Count() == 0)
                    {
                        MessageBox.Show("请输入支付信息，您不能结账，如支付金额为0，请选择其他？", "订单操作", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                    else
                    {
                        //判断支付信息有没有会员卡信息
                        Payment payment = PassValue.payments.Where(payments => payments.method.Equals("member")).FirstOrDefault();
                        if (payment != null)
                        {
                            //弹出密码输入框
                            InputPassWord ip = new InputPassWord();
                            if (ip.ShowDialog() == DialogResult.Cancel)
                            {
                                return;
                            }
                        }
                    }

                    HttpResult httpResult = httpReq.HttpPatch(string.Format("consumptions/{0}", PassValue.consumptionid), PassValue.Infor_payment);
                    if ((int)httpResult.StatusCode == 410)
                    {
                        MessageBox.Show("请检查打印机是否连接！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                    else if ((int)httpResult.StatusCode == 403)
                    {
                        MessageBox.Show("操作被禁止！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    else if ((int)httpResult.StatusCode == 500)
                    {
                        MessageBox.Show("操作错误（500）！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    else if ((int)httpResult.StatusCode == 400)
                    {
                        MessageBox.Show("请求错误（400）！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                    PassValue.Infor_payment = new PatchOrders();
                    PassValue.discounts.Clear();
                    PassValue.payments.Clear();//清空
                    Messagebox mb = new Messagebox();
                    PassValue.MessageInfor = "结账完成";
                    mb.ShowDialog();
                    PassValue.consumptionid = "";
                    PassValue.MemberCardPwd = "";
                    PassValue.listItemID.Clear();
                    PassValue.listItemIDBefore.Clear();
                    PassValue.Percent = 0;

                    Desk d = this.Owner as Desk;
                    if (d == null)
                    {
                        if (m_ILoadData != null)
                        {
                            m_ILoadData.LoadData();
                        }
                    }
                    else
                    {
                        d.CurrentChooseDesk.Clear();
                        d.Refresh_Method();
                        d.CurrentChooseDesk = new Dictionary<string, string>();
                    }

                    this.Close();
                }
                else
                {
                    PassValue.MessageInfor = "应收金额:" + this.lbTotal.Text + "实际结账的金额是:" + this.lbReceiveActual.Text + ",您不能结账！";
                    MessageBox.Show(PassValue.MessageInfor);
                }
            }
        }

        /// <summary>
        /// 鼠标悬停
        /// </summary>
        private void Btn_Enter_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_Enter.Image = Properties.Resources.确定;
        }

        private void Btn_Enter_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_Enter.Image = Properties.Resources.确定2;
        }
        #endregion

        #region 签单
        /// <summary>
        /// 签单
        /// </summary>
        private void Btn_Sign_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_Sign.Image = Properties.Resources.签单3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
           

            Sign sg = new Sign(memberConsumptionsid);
            sg.Owner = this;
            sg.TopLevel = false;
            sg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(sg);
            sg.Show();
            this.lbTitle.Text = "签单";
        }
        #endregion

        #region 其他结账方式
        /// <summary>
        /// 其他
        /// </summary>
        private void Btn_Other_Click(object sender, EventArgs e)
        {
            initPic();
            this.Btn_Other.Image = Properties.Resources.其他3;
            this.panelInfor.Visible = false;
            CloseMemberCard();
            this.panelChildren.Controls.Clear();
            

            OtherOrder oo = new OtherOrder(memberConsumptionsid);
            oo.Owner = this;
            oo.TopLevel = false;
            oo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildren.Controls.Add(oo);
            oo.Show();
            this.lbTitle.Text = "其他";
        }
        #endregion

        private void Member_FormClosing(object sender, FormClosingEventArgs e)
        {
            PassValue.Percent = 0;
            PassValue.listItemID.Clear();
            PassValue.listItemIDBefore.Clear();

            CloseMemberCard();
        }

        private void CloseMemberCard()
        {
            if (this.panelChildren.Controls.Count > 0)
            {
               MemberCard mc = this.panelChildren.Controls[0] as MemberCard;
               if(mc!=null)
               {
                   try
                   {
                       mc.Close();
                   }
                   catch
                   {
                   }
               }
            }
        }

        private void initPic()
        {
            if (this.Btn_All.Enabled == true)
                this.Btn_All.Image = Properties.Resources.整单打折;
            if (this.Btn_Part.Enabled == true)
                this.Btn_Part.Image = Properties.Resources.部分打折;
            if (this.Btn_Plan.Enabled == true)
                this.Btn_Plan.Image = Properties.Resources.方案打折;
            if (this.Btn_Fixed.Enabled == true)
                this.Btn_Fixed.Image = Properties.Resources.定额打折;
            if (this.Btn_Maling.Enabled == true)
                this.Btn_Maling.Image = Properties.Resources.摸零;
            if (this.Btn_Free.Enabled == true)
                this.Btn_Free.Image = Properties.Resources.免单;
            if (this.Btn_MemberCard.Enabled == true)
                this.Btn_MemberCard.Image = Properties.Resources.会员卡;
            if (this.Btn_BankCard.Enabled == true)
                this.Btn_BankCard.Image = Properties.Resources.银联卡;
            if (this.Btn_Sign.Enabled == true)
                this.Btn_Sign.Image = Properties.Resources.签单;
            if (this.Btn_Other.Enabled == true)
                this.Btn_Other.Image = Properties.Resources.其他;
            if (this.Btn_Cash.Enabled == true)
                this.Btn_Cash.Image = Properties.Resources.现金支付;
            if (this.Btn_Print.Enabled == true)
                this.Btn_Print.Image = Properties.Resources.打印对账单;
            if (this.Btn_Enter.Enabled == true)
                this.Btn_Enter.Image = Properties.Resources.确定2;
        }

        #region 打印对账单
        /// <summary>
        /// 打印对账单
        /// </summary>
        private void Btn_Print_Click(object sender, EventArgs e)
        {
            Task task = new Task();
            task.kind = "bill";
            Consumption consumption = new Consumption();
            consumption.id = PassValue.consumptionid;
            task.consumption = consumption;

            HttpResult httpResult = httpReq.HttpPost("printing/tasks", task);

            if ((int)httpResult.StatusCode >= 200 && (int)httpResult.StatusCode < 300)
            {
                MessageBox.Show("打印成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            else if ((int)httpResult.StatusCode == 410)
            {
                MessageBox.Show("请确定连接打印机！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                MessageBox.Show("打印失败！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }
        #endregion

        private void Member_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==27)
            {
                this.Close();
            }

        }
    }
}
