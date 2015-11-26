using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControls;
using System.Web.Script.Serialization;
using Business;
using Kernel;

namespace Client
{
    public partial class frmOrder : Form, IView
    {
        ConsumptionObj m_ConsumptionObj;
        public frmOrder(bool p_IsNeedUpload,ConsumptionObj p_ConsumptionObj)
        {
            m_ConsumptionObj = p_ConsumptionObj;

            InitializeComponent();

            //FormBorderStyle = FormBorderStyle.None;

            orderControl.SetConsumptionObj(p_ConsumptionObj);
            orderControl.SetParent(this);
            orderControl.IsNeedUpload = p_IsNeedUpload;
        }

        string m_ConsumptionId;
        public frmOrder(string p_ConsumptionId)
        {
            m_ConsumptionId = p_ConsumptionId;

            ConsumptionObj obj = GetConsumptionObj(m_ConsumptionId);

            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;

            orderControl.SetConsumptionObj(obj);
            orderControl.SetParent(this);

            orderControl.SetbtnCancelText("关闭(Esc)");

            orderControl.IsNeedUpload = true;
        }

        private ConsumptionObj GetConsumptionObj(string p_ConsumptionId)
        {
            GetInformation.address = "consumptions/" + p_ConsumptionId;
            GetInformation gc = new GetInformation();
            string result = gc.GetHTTPInfo();                                           //接收JSON数据

            var consumption = new JavaScriptSerializer();
            var personsConsumption = consumption.Deserialize<Consumption>(result);  //解析json数据

            ConsumptionObj obj = new ConsumptionObj();
            obj.Consumption = personsConsumption;
            return obj;
        }

        public void Active()
        {
            orderControl.Focusable = true;
            bool result = orderControl.Focus();
        }

        public string GetName()
        {
            return "点菜";
        }
    }
}
