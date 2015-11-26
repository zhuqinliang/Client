using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kernel;

namespace Client
{
    public partial class frmWebReport : Form, IView

    {
         WebKit.WebKitBrowser m_WebKitBrowser = new WebKit.WebKitBrowser();
         public frmWebReport()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            m_WebKitBrowser.Dock = DockStyle.Fill;
            this.Controls.Add(m_WebKitBrowser);
          
            this.Load += new EventHandler(frmWebReport_Load);
        }

        void frmWebReport_Load(object sender, EventArgs e)
        {
            string url = Global.GetConfig().GetConfigString("system", "ReportUrl");
            m_WebKitBrowser.Navigate(url);
        }

        public void Active()
        {

        }

        public string GetName()
        {
            return "报表";
        }
    }
}
