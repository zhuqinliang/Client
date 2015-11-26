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
using Kernel;

namespace Client
{
    public partial class frmReserveDishes : Form, IView
    {
        public frmReserveDishes()
        {
            InitializeComponent();
            this.rdControl.SearchBookingObjs += new WpfControls.SearchBookingObjsHandler(rdControl_SearchBookingObjs);
            this.rdControl.SelectedBookingObjChange += new WpfControls.SelectedBookingObjChangeHandler(rdControl_SelectedBookingObjChange);
        }

        void rdControl_SearchBookingObjs(string p_Date, string p_DinnerNameOrMoblie, string p_State)
        {
            List<Booking> resultlist = SearchBookingObjs(p_Date, p_DinnerNameOrMoblie, p_State);
            rdControl.LoadBookingObjList(resultlist);
        }

        void rdControl_SelectedBookingObjChange(WpfControls.BookingObj p_BookingObj)
        {
            if (p_BookingObj == null)
            {
                rdControl.LoadBookingObjDetail(null);
            }
            else
            {
                Booking booking = SearchBooking(p_BookingObj.BookingId);
                rdControl.LoadBookingObjDetail(booking);
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

        //预定信息查询
        public string getstrbookings(string p_Date)
        {
            GetInformation.address = string.Format("bookings?date={0}", p_Date);//预定状态信息
            GetInformation gd = new GetInformation();
            string strresult = gd.GetHTTPInfo();//接收JSON数据
            return strresult;
        }

        //单个预定信息查询
        public string getstrbooking(string p_Id)
        {

            GetInformation.address = "bookings/" + p_Id;//单个预定状态信息
            GetInformation gd = new GetInformation();
            string strresult = gd.GetHTTPInfo();//接收JSON数据
            return strresult;
        }

        //窗体加载事件
        private void frmReserveDishes_Load(object sender, EventArgs e)
        {
            
        }

        public List<Booking> SearchBookingObjs(string p_Date, string p_DinnerNameOrMoblie, string p_State)
        {
            List<Booking> resultlist = new List<Booking>();
            string Str_Bookings = getstrbookings(p_Date);
            var jserConsumption = new JavaScriptSerializer();
            var personsBooking = jserConsumption.Deserialize<List<Booking>>(Str_Bookings);//解析json数据
            if (personsBooking != null)
            {
                for (int i = 0; i < personsBooking.Count(); i++)
                {
                    bool result = true;

                    if (!string.IsNullOrEmpty(p_State.Trim()))
                    {
                        if (personsBooking[i].state.Contains(p_State))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }

                    if (result && !string.IsNullOrEmpty(p_DinnerNameOrMoblie))
                    {
                        if (personsBooking[i].diner.name.Contains(p_DinnerNameOrMoblie) || personsBooking[i].diner.mobile.Contains(p_DinnerNameOrMoblie))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }

                    if (result)
                    {
                        resultlist.Add(personsBooking[i]);
                    }
                }
            }


            resultlist.Sort((x, y) =>
                       {
                           int value = x.submitted.CompareTo(y.submitted);
                           return value*(-1);
                       });

            return resultlist;
        }

        public Booking SearchBooking(string p_BookingId)
        {
            string Str_Booking=getstrbooking(p_BookingId);
            var jserConsumption = new JavaScriptSerializer();
            Booking booking = jserConsumption.Deserialize<Booking>(Str_Booking);//解析json数据
            return booking;
        }

        public void Active()
        {

        }

        public string GetName()
        {
            return "预定";
        }
    }
}
