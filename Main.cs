using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Threading;
using System.Collections;//要调用哈希表
using System.Text.RegularExpressions;
using DevComponents.DotNetBar;
using System.Runtime.InteropServices;
using Business;
using Kernel;
using WpfControls;

namespace Client
{
    public partial class Main : Form, IMainFrame
    {
        Login m_frmLogin;
        public Main(Login p_Login)
        {
            InitializeComponent();

            m_frmLogin = p_Login;
            m_frmLogin.Hide();

            Global.SetMainFrame(this);

            this.FormClosed += new FormClosedEventHandler(Main_FormClosed);
        }

        void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);  
        }

        #region 窗体的加载
        /// <summary>
        /// 登录窗体的加载事件
        /// </summary>
        private void Main_Load(object sender, EventArgs e)
        {

            if (m_frmLogin.needHandOver)
            {
                if (MessageBox.Show("检测到你上次未交接班，请确认", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK) 
                {
                    Global.GetMainFrame().ShowView("交接班");
                    return;
                }
            }
            Global.GetMainFrame().ShowView("开桌");
        }

        /// <summary>
        /// 标签初始化
        /// </summary>
        public void SetUp()
        {
            this.BtnTable.Image = Properties.Resources.开桌;//开桌
            this.BtnOrders.Image = Properties.Resources.点菜;//点菜
            this.BtnCashier.Image = Properties.Resources.收银;//收银
            this.BtnBooking.Image = Properties.Resources.预定;//预定
            this.BtnMember.Image = Properties.Resources.会员;//会员
            this.BtnClass.Image = Properties.Resources.交接班;//交接班
            this.BtnQuery.Image = Properties.Resources.历史查询;//历史查询
            this.BtnOrderName.Image = Properties.Resources.菜品;//菜品
            this.BtnWarehouse.Image = Properties.Resources.库管;//库管
            this.BtnReport.Image = Properties.Resources.报表;//报表
            this.BtnSetting.Image = Properties.Resources.设置1;//设置
            this.Btn100.Image = Properties.Resources.点菜100;//点菜100
            this.Btn_Lock.Image = Properties.Resources.锁屏1;//锁屏
            this.BtnFp.Image = Properties.Resources.fapiao;//发票
        }
        #endregion

        #region 结账
        /// <summary>
        /// 结账按钮
        /// </summary>
        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (PassValue.consumptionid != "")
            {
                Member mb = new Member();
                mb.Owner = this;
                mb.Show();
            }
        }
        #endregion

        public int TitleId;//标题栏的标识
        #region 点菜
        /// <summary>
        /// 按钮点菜操作
        /// </summary>
        private void BtnOrders_Click(object sender, EventArgs e)
        {
            //btnorders();

            Global.GetMainFrame().ShowView("点菜");
        }
        public void btnorders()
        {
            if (TitleId == 1)//当前为开桌界面
            {
                if (!string.IsNullOrEmpty(PassValue.consumptionid))
                {
                    TitleId = 2;
                    SetUp();
                    this.BtnOrders.Image = Properties.Resources.点菜2;//选中点菜
                    frmOrder frmorder = new frmOrder(PassValue.consumptionid);
                    frmorder.Owner = this;
                    frmorder.TopLevel = false;
                    frmorder.FormBorderStyle = FormBorderStyle.None;
                    frmorder.Dock = DockStyle.Fill;
                    this.panelMain.Controls.Clear();
                    this.panelMain.Controls.Add(frmorder);
                    frmorder.Show();
                    PassValue.consumptionid = "";
                }
                else
                {
                    Messagebox mb = new Messagebox();
                    PassValue.MessageInfor = "请先选择已开台的桌子！";
                    mb.Show();
                }
            }
            else
            {
                if (TitleId != 2)
                {
                    Messagebox mb = new Messagebox();
                    PassValue.MessageInfor = "您还未选桌,不能点菜！";
                    mb.Show();
                }
            }
        }
        #endregion

        #region 交接班
        /// <summary>
        /// 交接班按钮
        /// </summary>
        private void BtnClass_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("交接班");
        }
        #endregion

        #region 收银
        /// <summary>
        /// 打开收银窗体
        /// </summary>
        private void BtnCashier_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("收银");
        }
        #endregion

        #region 历史查询
        /// <summary>
        /// 历史单查询
        /// </summary>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("历史查询");
        }
        #endregion

        #region 预定
        /// <summary>
        /// 预定
        /// </summary>
        private void BtnBooking_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("预定");
        }
        #endregion

        #region 开桌
        /// <summary>
        /// 开桌
        /// </summary>
        private void BtnTable_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("开桌");
        }
        #endregion

        #region 会员
        /// <summary>
        /// 会员系统
        /// </summary>
        private void BtnMember_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("会员");
        }
        #endregion

        #region 设置
        /// <summary>
        /// 设置
        /// </summary>
        private void BtnSetting_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("设置");
        }
        #endregion

        #region  定义打开窗体
        //菜品
        private void BtnOrderName_Click(object sender, EventArgs e)
        {
            //Global.GetMainFrame().ShowView("菜品");
            //调用IE浏览器  
            string url = string.Format("{0}{1}{2}", "http://", Global.GetConfig().GetConfigString("system", "MainUrlIp"), ":3000");
            //System.Diagnostics.Process.Start("iexplore.exe", Global.GetConfig().GetConfigString("system", "DishesUrl"));  
            System.Diagnostics.Process.Start("iexplore.exe", url);  
        }
        
        //库管
        private void BtnWarehouse_Click(object sender, EventArgs e)
        {
            //Global.GetMainFrame().ShowView("库管");
            //调用IE浏览器  
            string url = string.Format("{0}{1}{2}", "http://", Global.GetConfig().GetConfigString("system", "MainUrlIp"), ":8088/login.jsp");
            //System.Diagnostics.Process.Start("iexplore.exe", Global.GetConfig().GetConfigString("system", "WarehouseUrl"));  
            System.Diagnostics.Process.Start("iexplore.exe", url);  
        }

        //报表
        private void BtnReport_Click(object sender, EventArgs e)
        {
            //Global.GetMainFrame().ShowView("报表");
            //调用IE浏览器  
            string url = string.Format("{0}{1}{2}", "http://", Global.GetConfig().GetConfigString("system", "MainUrlIp"), ":3000");
            //System.Diagnostics.Process.Start("iexplore.exe", Global.GetConfig().GetConfigString("system", "DishesUrl"));  
            System.Diagnostics.Process.Start("iexplore.exe", url);  
        }

        //TT100
        private void Btn100_Click(object sender, EventArgs e)
        {
            //Global.GetMainFrame().ShowView("TT100");
            //调用IE浏览器  
            System.Diagnostics.Process.Start("iexplore.exe", Global.GetConfig().GetConfigString("system", "TT100Url"));  
        }

        private void SetButtonActived(string p_FormName)
        {
            SetUp();
            switch (p_FormName)
            {
                case "开桌":
                     this.BtnTable.Image = Properties.Resources.开桌2;//选中开桌
                    break;

                case "点菜":
                    this.BtnOrders.Image = Properties.Resources.点菜2;//选中点菜
                    break;

                case "收银":
                    this.BtnCashier.Image = Properties.Resources.收银2;//选中收银
                    break;

                case "预定":
                    this.BtnBooking.Image = Properties.Resources.预定2;//选中历史查询
                    break;

                case "会员":
                    this.BtnMember.Image = Properties.Resources.会员2;//选中会员
                    break;

                case "交接班":
                      this.BtnClass.Image = Properties.Resources.交接班2;//选中交接班
                    break;

                case "历史查询":
                     this.BtnQuery.Image = Properties.Resources.历史查询2;//选中历史查询
                    break;

                case "菜品":
                    this.BtnOrderName.Image = Properties.Resources.菜品2;
                    break;

                case "库管":
                    this.BtnWarehouse.Image = Properties.Resources.库管2;
                    break;

                case "设置":
                    this.BtnReport.Image = Properties.Resources.设置2;
                    break;

                case "报表":
                    this.BtnReport.Image = Properties.Resources.报表2;
                    break;

                case "TT100":
                    this.Btn100.Image = Properties.Resources.点菜200;
                    break;

                case "锁屏":
                    this.Btn_Lock.Image = Properties.Resources.锁屏2;
                    break;

            }
        }

        List<IView> m_ViewList = new List<IView>();

        private IView FindView(string p_Name)
        {
            foreach (IView view in m_ViewList)
            {
                if (view.GetName() == p_Name)
                {
                    return view;
                }
            }
            return null;
        }

        IView m_CurrentView = null;
        public void ShowView(string p_Name)
        {
            if (m_CurrentView !=null && (m_CurrentView.GetName() == p_Name))  //点击的是当前的,不做处理
            {
                return;
            }

            if (m_RegisteredView != null)
            {
                frmMessageBox frmMessageBox = new frmMessageBox(System.Windows.MessageBoxButton.OK, "请先点击右下方的确定或者取消按钮，才能进行其他操作。");
                frmMessageBox.ShowDialog();
                return;
            }

            bool isneedactive = false;

            IView view= FindView(p_Name);
            switch (p_Name)
            {
                case "开桌":
                    {
                        if (view == null)
                        {
                            view = new Desk();
                            m_ViewList.Add(view);
                        }
                        else
                        {
                            isneedactive = true;
                        }
                    }
                    break;

                case "点菜":
                    {
                        if (m_CurrentView.GetName() == "开桌")
                        {
                            if (string.IsNullOrEmpty(PassValue.consumptionid))
                            {
                                Messagebox mb = new Messagebox();
                                PassValue.MessageInfor = "请先选择已开台的桌子！";
                                mb.Show();
                            }
                            else
                            {
                                view = new frmOrder(PassValue.consumptionid);
                                isneedactive = true;
                            }
                        }
                    }
                    break;

                case "收银":
                    {
                        view = new frmCashier(PassValue.consumptionid);
                        isneedactive = true;
                    }
                    break;

                case "预定":
                    {
                        view = new frmReserveDishes();
                    }
                    break;

                case "会员":
                    {
                        if (view == null)
                        {
                            view = new MemberSystem();
                        }
                    }
                    break;

                case "交接班":
                    {
                        view = new Transfer();
                    }
                    break;

                case "历史查询":
                    {
                        view = new frmQuery(PassValue.consumptionid);
                        isneedactive = true;
                    }
                    break;

                case "菜品":
                    {
                        if (view == null)
                        {
                            view = new frmWebDishes();
                            m_ViewList.Add(view);
                        }
                    }
                    break;

                case "库管":
                    {
                        if (view == null)
                        {
                            view = new frmWebWarehouse();
                            m_ViewList.Add(view);
                        }
                    }
                    break;

                case "设置":
                    {
                        History hs  = new History();
                        hs.ShowDialog();
                    }
                    break;

                case "报表":
                    {
                        if (view == null)
                        {
                            view = new frmWebReport();
                            m_ViewList.Add(view);
                        }
                    }
                    break;

                case "TT100":
                    {
                        if (view == null)
                        {
                            view = new frmWebTT100();
                            m_ViewList.Add(view);
                        }
                    }
                    break;
                case "锁屏":
                    {
                        LockWindows lw = new LockWindows();
                        lw.ShowDialog();
                    }
                    break;
            }

            if (view == null)    //前面没有生成view
            {

            }
            else
            {
                //先关闭之前的view
                if (m_CurrentView != null && FindView(m_CurrentView.GetName()) == null)
                {
                    Form frm = m_CurrentView as Form;
                    frm.Close();
                }

                //显示现在的view
                Form form = view as Form;
                form.Owner = this;
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                this.panelMain.Controls.Clear();
                this.panelMain.Controls.Add(form);
                form.Show();

                if (isneedactive)
                {
                    view.Active();
                }

                m_CurrentView = view;

                //设置按钮状态
                SetButtonActived(p_Name);
            }
        }

        public void ShowView(IView p_View)
        {
            Form form = p_View as Form;
            form.Owner = this;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.panelMain.Controls.Clear();
            this.panelMain.Controls.Add(form);
            form.Show();

            m_CurrentView = p_View;
            m_CurrentView.Active();

            SetButtonActived(m_CurrentView.GetName());
        }

        public void ShowRegisteredView(IView p_View)
        {
            Form form = p_View as Form;
            form.Owner = this;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.panelMain.Controls.Clear();
            this.panelMain.Controls.Add(form);
            form.Show();

            Form frm = m_CurrentView as Form;
            frm.Close();

            m_CurrentView = p_View;

            m_CurrentView.Active();

            SetButtonActived(m_CurrentView.GetName());

            m_RegisteredView = null;
        }

        public IView GetCurrentView()
        {
            return m_CurrentView;
        }

        IView m_RegisteredView = null;
        public void RegisterView(IView p_View)
        {
            m_RegisteredView = p_View;
        }

        public IView GetRegisteredView()
        {
            return m_RegisteredView;
        }

        public string GetName()
        {
            return "MainFrame";
        }

        public void ShowView(ParameterObj p_ParameterObj)
        {
            switch (p_ParameterObj.MethodName)
            {
                case "到店消费":
                    {
                        ShowView("收银");
                        frmCashier frmcashier = m_CurrentView as frmCashier;

                        BookingObjDetail detail = p_ParameterObj.Parameter[0] as BookingObjDetail;

                        frmcashier.SetBookingObjDetail(detail);
                        break;
                    }
            }
        }

        public void BackToLogin()
        {
            string path = Application.StartupPath + @"/点菜100.exe";
            System.Diagnostics.Process.Start(path);
            this.Close();
        }
        #endregion

        #region 锁屏
        private void Btn_Lock_Click(object sender, EventArgs e)
        {
            Global.GetMainFrame().ShowView("锁屏");
        }
        #endregion

        #region 打印发票
        private void BtnFp_Click(object sender, EventArgs e)
        {
            //调用IE浏览器  
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.jsds.gov.cn/kpgl/");    
        }
        #endregion
    }
}
