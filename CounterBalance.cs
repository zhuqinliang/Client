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

namespace Client
{
    public partial class CounterBalance : Form
    {
        public CounterBalance()
        {
            InitializeComponent();
        }

        private string p_ConsumptionsId;
        private int p_peopleCount;
        public CounterBalance(string coid, int peopleCount)
            : this()
        {
            p_ConsumptionsId = coid;
            p_peopleCount = peopleCount;
        }

        //清空
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            this.TxtNote.Clear();
        }
        
        //确认提交反结算
        private void Btn_Enter_Click(object sender, EventArgs e)
        {
            HttpWebResponse response = null;
            if (this.TxtNote.Text != "")
            {
                Consumption cp = new Consumption();
                List<Log> log = new List<Log>();
                Log logs = new Log();
                logs.note = this.TxtNote.Text;
                logs.operation = "RECHECKOUTING";//RECHECKOUTING
                log.Add(logs);
                cp.logs = log.ToArray();
                cp.people = p_peopleCount;
                List<Payment> lps= new List<Payment>();
                cp.payments = lps.ToArray();
                try
                {
                    System.Net.WebHeaderCollection headers = new System.Net.WebHeaderCollection();
                    headers.Add("Authorization", PassValue.token);
                    response = Patch.PatchHttp(headers, "consumptions/" + p_ConsumptionsId, cp);
                    PassValue.consumptionid = "";
                    Messagebox mb = new Messagebox();
                    PassValue.MessageInfor = "反结算成功！";
                    mb.ShowDialog();
                    this.Close();
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                } 
            }
            else
            {
                Messagebox mb = new Messagebox();
                PassValue.MessageInfor = "不能为空！";
                mb.ShowDialog();
            }
            
        }
    }
}
