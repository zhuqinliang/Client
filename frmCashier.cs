using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControls;
using Kernel;
using Business;
using System.Web.Script.Serialization;
using System.Net;

namespace Client
{
    public partial class frmCashier : Form, IView, ILoadData
    {
        private frmCashier()
        {
            InitializeComponent();

            cqControl.SetParent(this);
            cqControl.ControlType = ConsumptionQueryControlType.Cashier;
            cqControl.ButtonClick += new System.Windows.RoutedEventHandler(cqControl_ButtonClick);
        }

        string m_ConsumptionId;
        public frmCashier(string p_ConsumptionId)
            : this()
        {
            m_ConsumptionId = p_ConsumptionId;
            cqControl.SetConsumptionId(m_ConsumptionId);
        }

        protected ConsumptionQueryControl GetcqControl()
        {
            return cqControl;
        }

        void cqControl_ButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;

            if (!button.IsEnabled)
            {
                return;
            }

            string buttontext = button.Content.ToString();
            if (buttontext.Contains("批量清空"))
            {
                ClearTemp();
            }
            else if (buttontext.Contains("添加菜"))
            {
                AddDishes();
            }
            else if (buttontext.Contains("确定提交"))
            {
                //已经在 ConsumptionQueryControl 内部处理
            }
            else if (buttontext.Contains("打印结账单"))
            {
                PrintCashier();
            }
            else if (buttontext.Contains("结账"))
            {
                Checkout();
            }
            else if (buttontext.Contains("反结算"))
            {
                CounterBalance();
            }
            else if (buttontext.Contains("显示账单信息"))
            {
                ShowCashier();
            }
            else if (buttontext.Contains("打印对账单"))
            {
                PrintOrder();
            }
           
        }

        private void ClearTemp()
        {
            cqControl.GetDishesControl().ClearLocalDishesObjs();
        }

        private void AddDishes()
        {
            ConsumptionObj obj = cqControl.GetlvConsumption().GetCurrentObj(); 
            if (obj != null)
            {
                frmOrder frmOrder = new frmOrder(false, obj);
                Global.GetMainFrame().RegisterView(this);
                Global.GetMainFrame().ShowView(frmOrder as IView);
            }
        }

        private void Checkout()
        {
             ConsumptionObj obj = cqControl.GetlvConsumption().GetCurrentObj();
             if (obj != null)
             {
                 Member mb = new Member(obj.Consumption.id, this);
                 mb.ShowDialog();
             }
        }

        private void CounterBalance()
        {
            ConsumptionObj obj = cqControl.GetlvConsumption().GetCurrentObj();
            if (obj != null)
            {
                CounterBalance cb = new CounterBalance(obj.Consumption.id, obj.PersonNum);
                cb.Owner = this;
                cb.ShowDialog();

                this.LoadData();
            }
        }

        //消费ID的信息查询
        public Consumption GetConsumption(string p_ConsumptionId)
        {
            GetInformation.address = "consumptions/" + p_ConsumptionId;//桌子状态信息
            GetInformation gi = new GetInformation();
            string result = gi.GetHTTPInfo();

            var jserConsumption = new JavaScriptSerializer();
            Consumption personsConsumption = jserConsumption.Deserialize<Consumption>(result);//解析json数据
            return personsConsumption;
        }

        private void ShowCashier()
        {
            ConsumptionObj obj = cqControl.GetlvConsumption().GetCurrentObj();
            if (obj != null)
            {
                string Consumptionid = obj.Consumption.id;
                var jserConsumption = new JavaScriptSerializer();
                Consumption personsConsumption = GetConsumption(Consumptionid);

                frmShowPayDetails window = new frmShowPayDetails();
                window.ShowData = personsConsumption;
                System.Drawing.Point p = new System.Drawing.Point(MousePosition.X, MousePosition.Y);
                window.Left = p.X - window.Width / 2;
                window.Top = p.Y - window.Height;
                window.Show();
            }
        }

        private void PrintOrder()
        {
            ConsumptionObj obj = cqControl.GetlvConsumption().GetCurrentObj();
            if (obj != null)
            {
                string Consumptionid = obj.Consumption.id;

                Task task = new Task();
                task.kind = "bill";
                Consumption consumption = new Consumption();
                consumption.id = Consumptionid;
                task.consumption = consumption;

                System.Net.WebHeaderCollection header = new System.Net.WebHeaderCollection();
                header.Add("Authorization", PassValue.token);
                HttpWebResponse response = Post.PostHttp(header, "printing/tasks", task);
                if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                {
                    var jserConsumption = new JavaScriptSerializer();
                    consumption = jserConsumption.Deserialize<Consumption>(PassValue.statucode);
                }
                MessageBox.Show("打印成功！");
            } 
        }

        private void PrintCashier()
        {
            ConsumptionObj obj = cqControl.GetlvConsumption().GetCurrentObj();
            if (obj != null)
            {
                string Consumptionid = obj.Consumption.id;

                Task task = new Task();
                task.kind = "invoice";
                Consumption consumption = new Consumption();
                consumption.id = Consumptionid;
                task.consumption = consumption;

                System.Net.WebHeaderCollection header = new System.Net.WebHeaderCollection();
                header.Add("Authorization", PassValue.token);
                HttpWebResponse response = Post.PostHttp(header, "printing/tasks", task);
                if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                {
                    var jserConsumption = new JavaScriptSerializer();
                    consumption = jserConsumption.Deserialize<Consumption>(PassValue.statucode);
                }
                MessageBox.Show("打印成功！");
            } 
        }

        public void Active()
        {
            cqControl.Focusable = true;
            bool result = cqControl.Focus();

            cqControl.GetDishesControl().LoadData();
        }

        public string GetName()
        {
            return "收银";
        }

        public void LoadData()
        {
            cqControl.LoadData();
        }

        public void SetBookingObjDetail(BookingObjDetail p_BookingObjDetail)
        {
            cqControl.GetlvConsumption().SetSelectedItem(p_BookingObjDetail.Booking.id);
            cqControl.GetDishesControl().SetLocalDataSource(p_BookingObjDetail.GetDishesObjs());
        }
    }
}
