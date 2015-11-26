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

namespace Client
{
    public partial class PaymentReasons : Form
    {
        public PaymentReasons()
        {
            InitializeComponent();
        }

        #region JOSN数据获取方法
        /// <summary>
        /// 获取JSON数据
        /// </summary>
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

        /// <summary>
        /// 消费ID的信息查询
        /// </summary>
        public string Str_Reasons;
        public void GetPaymentReasons()
        {
            GetInformation.address = "payment/reasons";//桌子状态信息
            GetInformation gi = new GetInformation();
            Str_Reasons = gi.GetHTTPInfo();//接收JSON数据
        }
        #endregion

        #region 窗体的加载
        /// <summary>
        /// 窗体的加载
        /// </summary>
        private void PaymentReasons_Load(object sender, EventArgs e)
        {
            OtherOrder oo;
            oo = (OtherOrder)this.Owner;
            GetPaymentReasons();
            var jserReasons = new JavaScriptSerializer();
            var personsReasons = jserReasons.Deserialize<List<Reasons>>(Str_Reasons);//解析json数据
            if (personsReasons != null)
            {
                for (int i = 0; i < personsReasons.Count(); i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = "";
                    item.SubItems.Add(personsReasons[i].description);
                    item.Tag = personsReasons[i].id;
                    if (string.IsNullOrEmpty(oo.lbReasons.Text))
                    {
                        item.Checked = false;
                    }
                    else
                    {
                        item.Checked = true;
                    }
                    this.lv.Items.Add(item);
                }
            }
        }
        #endregion

        //取消
        private void picCanel_Click(object sender, EventArgs e)
        {
            hide();
        }

        public void hide()
        {
            OtherOrder oo;
            oo = (OtherOrder)this.Owner;
            oo.Visible = true;
            this.Close();
        }

        //确定
        private void picOk_Click(object sender, EventArgs e)
        {
            bool isChoose = false;
            foreach (ListViewItem item in lv.Items)
            {
                if (item.Checked)
                {
                    isChoose=true;
                    break;
                }
            }
            if (isChoose==false)
            {
                MessageBox.Show("请至少选择一个优惠选项!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            add();
        }

        public List<string> reasons = new List<string>();//所有原因的集合
        public List<string> reasonsID = new List<string>();//所有原因ID的集合
        public void add()
        {
            foreach (ListViewItem ctl in this.lv.Items)
            {
                if (ctl.Checked)
                {
                    reasons.Add(ctl.SubItems[1].Text.ToString());
                    reasonsID.Add(ctl.Tag.ToString());
                }
            }
            OtherOrder oo;
            oo = (OtherOrder)this.Owner;
            oo.listreasons = new System.Collections.ArrayList(reasons.ToArray());
            oo.lbReasons.Text = oo.listreasons[0].ToString();
            oo.reasonid = reasonsID;
            oo.lbReasons.Visible = true;
            oo.label4.Visible = false;
            hide();
        }
    }
}
