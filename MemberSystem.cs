using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebKit;
using System.Threading;
using e7;
using Kernel;
using System.Web.Script.Serialization;
using Business;
using WpfControls;

namespace Client
{
    public partial class MemberSystem : Form, IView
    {
        WebKitBrowser m_Browser = new WebKitBrowser();

        E7Device m_E7Device = new E7Device();

        public MemberSystem()
        {
            InitializeComponent();

            m_Browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            this.Controls.Add(m_Browser);

            this.Load+=new EventHandler(MemberSystem_Load);
            this.FormClosed += new FormClosedEventHandler(MemberSystem_FormClosed);

            this.SizeChanged += new EventHandler(MemberSystem_SizeChanged);

            CreateButtons();

            SetButtonsVisible("default");
        }

        void MemberSystem_Load(object sender, EventArgs e)
        {
            m_Browser.Navigate(GetHomeUrl());
        }

        void MemberSystem_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        void MemberSystem_SizeChanged(object sender, EventArgs e)
        {
            m_Browser.Size = new Size(this.Width - 10 - 70 - 10, this.Height);
        }

        ImageButtonEx btnOpenCard = new ImageButtonEx();
        ImageButtonEx btnCardPSW1 = new ImageButtonEx();
        ImageButtonEx btnCardPSW2 = new ImageButtonEx();
        ImageButtonEx btnReadCard = new ImageButtonEx();
        private void CreateButtons()
        {
            btnOpenCard = new ImageButtonEx();
            btnOpenCard.NormalImage = Client.Properties.Resources.btn_opencard_nor;
            btnOpenCard.HoverImage = Client.Properties.Resources.btn_opencard_pre;
            btnOpenCard.ActiveImage = Client.Properties.Resources.btn_opencard_pre;
            btnOpenCard.Height = btnOpenCard.Height;
            btnOpenCard.Click += new EventHandler(btnOpenCard_Click);
            this.Controls.Add(btnOpenCard);

            btnCardPSW1 = new ImageButtonEx();
            btnCardPSW1.NormalImage = Client.Properties.Resources.btn_cardpsw1_nor;
            btnCardPSW1.HoverImage = Client.Properties.Resources.btn_cardpsw1_pre;
            btnCardPSW1.ActiveImage = Client.Properties.Resources.btn_cardpsw1_pre;
            btnCardPSW1.Height = btnCardPSW1.Height;
            btnCardPSW1.Click += new EventHandler(btnCardPSW1_Click);
            this.Controls.Add(btnCardPSW1);

            btnCardPSW2 = new ImageButtonEx();
            btnCardPSW2.NormalImage = Client.Properties.Resources.btn_cardpsw2_nor;
            btnCardPSW2.HoverImage = Client.Properties.Resources.btn_cardpsw2_pre;
            btnCardPSW2.ActiveImage = Client.Properties.Resources.btn_cardpsw2_pre;
            btnCardPSW2.Height = btnCardPSW2.Height;
            btnCardPSW2.Click += new EventHandler(btnCardPSW2_Click);
            this.Controls.Add(btnCardPSW2);

            btnReadCard = new ImageButtonEx();
            btnReadCard.NormalImage = Client.Properties.Resources.btn_readcard_nor;
            btnReadCard.HoverImage = Client.Properties.Resources.btn_readcard_pre;
            btnReadCard.ActiveImage = Client.Properties.Resources.btn_readcard_pre;
            btnReadCard.Height = btnReadCard.Height;
            btnReadCard.Click += new EventHandler(btnReadCard_Click);
            this.Controls.Add(btnReadCard);
        }

        private void SetButtonsLocation(string p_State)
        {
            int pox = 0;
            int poy = 0;
            switch (p_State)
            {
                case "开卡":
                    {
                        pox = this.Width - 10 - 70;
                        poy = (this.Height - 70 * 3 - 10 * 2) / 2;
                        btnOpenCard.Location = new System.Drawing.Point(pox, poy);
                        btnCardPSW1.Location = new System.Drawing.Point(pox, btnOpenCard.Location.Y + btnOpenCard.Height + 10);
                        btnCardPSW2.Location = new System.Drawing.Point(pox, btnCardPSW1.Location.Y + btnCardPSW1.Height + 10);
                    }
                    break;

                case "充值":
                    {
                        pox = this.Width - 10 - 70;
                        poy = (this.Height - 70) / 2;
                        btnReadCard.Location = new System.Drawing.Point(pox, poy);
                    }
                    break;

                case "消费":
                    {
                        pox = this.Width - 10 - 70;
                        poy = (this.Height - 70 * 2 - 10) / 2;
                        btnReadCard.Location = new System.Drawing.Point(pox, poy);
                        btnCardPSW1.Location = new System.Drawing.Point(pox, btnReadCard.Location.Y + btnReadCard.Height + 10);
                    }
                    break;
            }
        }
             
        private void SetButtonsVisible(string p_State)
        {
            switch (p_State)
            {
                case "开卡":
                    {
                        btnOpenCard.Visible = true;
                        btnCardPSW1.Visible = true;
                        btnCardPSW2.Visible = true;
                        btnReadCard.Visible = false;
                    }
                    break;

                case "充值":
                    {
                        btnOpenCard.Visible = false;
                        btnCardPSW1.Visible = false;
                        btnCardPSW2.Visible = false;
                        btnReadCard.Visible = true;
                    }
                    break;

                case "消费":
                    {
                        btnOpenCard.Visible = false;
                        btnCardPSW1.Visible = true;
                        btnCardPSW2.Visible = false;
                        btnReadCard.Visible = true;
                    }
                    break;

                default:
                    {
                        btnOpenCard.Visible = false;
                        btnCardPSW1.Visible = false;
                        btnCardPSW2.Visible = false;
                        btnReadCard.Visible = false;
                    }
                    break;
            }
        }

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

        public string Str_Restaurant;//餐厅信息
        public string Str_Shift;//登录信息

        public void getstrrestaurant()
        {
            GetInformation.address = "restaurant";//餐厅信息
            GetInformation gd = new GetInformation();
            Str_Restaurant = gd.GetHTTPInfo();//接收JSON数据
        }

        public void getShifts()
        {
            GetInformation.address = "shifts/" + PassValue.shiftId;//桌子状态信息
            if (PassValue.shiftId != "")
            {
                GetInformation gi = new GetInformation();
                Str_Shift = gi.GetHTTPInfo();//接收JSON数据
            }
            else
            {
                Str_Shift = "";
            }
        }
     
        private string GetHomeUrl()
        {
            getstrrestaurant();//接受json数据
            var jserRestaurant = new JavaScriptSerializer();
            var personsRestaurant = jserRestaurant.Deserialize<Restaurant>(Str_Restaurant);//解析json数据
            getShifts();
            var jserShifts = new JavaScriptSerializer();
            var personsShifts = jserShifts.Deserialize<Shift>(Str_Shift);//解析json数据
            m_Browser.Dock = DockStyle.Fill;
            this.Controls.Add(m_Browser);
            string Url = "https://www.dcai100.com/prot/memberCharge/toIndex/" + personsRestaurant.id + "/" + personsShifts.cashier.id;

            return Url;
        }

        private string GetState(string p_Url)
        {
            string state = "主页";
            string url = p_Url.ToLower();

            if (url.Contains("membercharge/toindex"))
            {
                state = "主页";
            }
            else if (url.Contains("usermessage/toopen"))
            {
                state = "开卡";
            }
            else if (url.Contains("usermessage/tolist"))
            {
                state = "查询";
            }
            else if (url.Contains("membercharge/tocharge"))
            {
                state = "充值";
            }
            else if (url.Contains("membercharge/toconsume"))
            {
                state = "消费";
            }
            return state;
        }

        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (m_Browser.Url == null)
            {
            }
            else
            {
                string url = m_Browser.Url.ToString();
                m_State = GetState(url);
                SetButtonsVisible(m_State);
                SetButtonsLocation(m_State);
            }
        }

        string m_State = "主页";
        public string State
        {
            get { return m_State; }
        }


        Thread m_OpenCardThread;
        frmWaitDialog frmOpenCardDialog;
        void btnOpenCard_Click(object sender, EventArgs e)
        {
            bool? yesorno = frmMessageBox.ShowMessageBox(System.Windows.MessageBoxButton.YesNo, "开卡会自动覆盖原有的卡号,是否继续？");
            if (yesorno == true)
            {
                m_OpenCardThread = new Thread(new ThreadStart(DoOpenCard));
                m_OpenCardThread.Start();

                frmOpenCardDialog = new frmWaitDialog(StopOpenCardThread, frmWaitDialogType.WaitInitDevice);
                frmOpenCardDialog.WindowState = FormWindowState.Maximized;
                frmOpenCardDialog.ShowDialog();
            }
        }

        protected void DoOpenCard()
        {
            try
            {
                //连接设备
                bool result = m_E7Device.OpenDevice();
                while (!result)
                {
                    result = m_E7Device.OpenDevice();
                    Thread.Sleep(1000);
                }

                //读卡
                this.Invoke(new Action(() =>
                    {
                        frmOpenCardDialog.SetfrmWaitDialogType(frmWaitDialogType.WaitReadCard);
                    }));

                m_E7Device.DisplayLcd("请刷卡或把卡放在卡槽上");

                result = m_E7Device.FindCard();
                while (!result)
                {
                    Thread.Sleep(1000);
                    result = m_E7Device.FindCard();
                }


                //写卡
                string newcardid = m_E7Device.CreateNewCardId();
                result = m_E7Device.WriteLogicCardId(newcardid); 

                if (result)
                {
                    this.Invoke(new Action(() =>
                    {
                        frmOpenCardDialog.Close();
                        m_Browser.StringByEvaluatingJavaScriptFromString(string.Format("addStoreCardId ('{0}')", newcardid));
                    }));

                    m_E7Device.Beep();
                    //m_E7Device.DisplayLcd("      点菜100", "餐饮管理服务云平台");
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        frmOpenCardDialog.SetfrmWaitDialogType(frmWaitDialogType.WriteCardFailed);
                    }));
                }
            }
            catch (Exception E)
            {
                Global.GetLogger().Error("MemberSystem", "DoOpenCard", E);
            }
            finally
            {
                m_E7Device.CloseDevice();
            }
        }

        private void StopOpenCardThread()
        {
            try
            {
                m_OpenCardThread.Abort();
            }
            catch
            {
            }
        }

       
        Thread m_InputPSWThread;
        frmWaitDialog frmInputPSWDialog;
        void btnCardPSW1_Click(object sender, EventArgs e)
        {
            m_InputPSWThread = new Thread(new ThreadStart(DoReadUserPSW));
            m_InputPSWThread.Start();

            frmInputPSWDialog = new frmWaitDialog(StopWaitInputPSWThread,frmWaitDialogType.WaitInitDevice);
            frmInputPSWDialog.WindowState = FormWindowState.Maximized;
            frmInputPSWDialog.ShowDialog();
        }

       
        void btnCardPSW2_Click(object sender, EventArgs e)
        {
            m_InputPSWThread = new Thread(new ThreadStart(DoReadUserPSWAgain));
            m_InputPSWThread.Start();

            frmInputPSWDialog = new frmWaitDialog(StopWaitInputPSWThread, frmWaitDialogType.WaitInitDevice);
            frmInputPSWDialog.WindowState = FormWindowState.Maximized;
            frmInputPSWDialog.ShowDialog();
        }

        private void DoReadUserPSW()
        {
            try
            {
                //连接设备
                bool result = m_E7Device.OpenDevice();
                while (!result)
                {
                    result = m_E7Device.OpenDevice();
                    Thread.Sleep(1000);
                }


                //输入密码
                this.Invoke(new Action(() =>
                {
                    frmInputPSWDialog.SetfrmWaitDialogType(frmWaitDialogType.WaitInputPSW);
                }));

                m_E7Device.DisplayLcd("请输入密码！");

                string userpsw = m_E7Device.ReadUserPSW(); 
                this.Invoke(new Action(() =>
                {
                    m_Browser.StringByEvaluatingJavaScriptFromString(string.Format("addPassword ('{0}')", userpsw));
                }));
            }
            catch
            {
            }
            finally
            {
                try
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        frmInputPSWDialog.Close();
                    }));
                }
                catch
                {
                }
               
                m_E7Device.CloseDevice();
            }
        }

        private void DoReadUserPSWAgain()
        {
            try
            {

                //连接设备
                bool result = m_E7Device.OpenDevice();
                while (!result)
                {
                    result = m_E7Device.OpenDevice();
                    Thread.Sleep(1000);
                }


                //输入密码
                this.Invoke(new Action(() =>
                {
                    frmInputPSWDialog.SetfrmWaitDialogType(frmWaitDialogType.WaitInputPSW);
                }));

                m_E7Device.DisplayLcd("请输入密码！");

                string userpsw = m_E7Device.ReadUserPSW();
                this.Invoke(new Action(() =>
                {
                    m_Browser.StringByEvaluatingJavaScriptFromString(string.Format("addPasswordAgain ('{0}')", userpsw));

                    
                }));
            }
            catch
            {
            }
            finally
            {
                try
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        frmInputPSWDialog.Close();
                    }));
                }
                catch
                {
                }

                m_E7Device.CloseDevice();
            }
        }

        private void StopWaitInputPSWThread()
        {
            try
            {
                m_InputPSWThread.Abort();
            }
            catch
            {
            }
        }

        Thread m_ReadCardThread;
        frmWaitDialog frmReadCardDialog;
        void btnReadCard_Click(object sender, EventArgs e)
        {
            m_ReadCardThread = new Thread(new ThreadStart(DoReadCard));
            m_ReadCardThread.Start();

            frmReadCardDialog = new frmWaitDialog(StopReadCardThread, frmWaitDialogType.WaitInitDevice);
            frmReadCardDialog.WindowState = FormWindowState.Maximized;
            frmReadCardDialog.ShowDialog();
        }

        private void DoReadCard()
        {
            try
            {
                //连接设备
                bool result = m_E7Device.OpenDevice();
                while (!result)
                {
                    result = m_E7Device.OpenDevice();
                    Thread.Sleep(1000);
                }


                //读卡
                this.Invoke(new Action(() =>
                {
                    frmReadCardDialog.SetfrmWaitDialogType(frmWaitDialogType.WaitReadCard);
                }));

                m_E7Device.DisplayLcd("请刷卡或把卡放在卡槽上");

                result = m_E7Device.FindCard();
                while (!result)
                {
                    Thread.Sleep(1000);
                    result = m_E7Device.FindCard();
                }

                string cardid = m_E7Device.ReadLogicCardId();

                this.Invoke(new Action(() =>
                {
                    frmReadCardDialog.Close();
                    m_Browser.StringByEvaluatingJavaScriptFromString(string.Format("addStoreCardId ('{0}')", cardid));
                }));
                m_E7Device.Beep();

                //m_E7Device.DisplayLcd("      点菜100", "餐饮管理服务云平台");
            }
            catch (Exception E)
            {
                Global.GetLogger().Error("MemberSystem", "DoReadCard", E);
            }
            finally
            {
                m_E7Device.CloseDevice();
            }
        }

        private void StopReadCardThread()
        {
            try
            {
                m_InputPSWThread.Abort();
            }
            catch
            {
            }
        }

        public void Active()
        {
           
        }

        public string GetName()
        {
            return "会员";
        }
    }
}
