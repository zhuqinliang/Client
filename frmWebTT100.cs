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
    public partial class frmWebTT100 : Form, IView
    {
        WebKit.WebKitBrowser m_WebKitBrowser = new WebKit.WebKitBrowser();
        public frmWebTT100()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            m_WebKitBrowser.Dock = DockStyle.Fill;
            this.Controls.Add(m_WebKitBrowser);

            this.Load += new EventHandler(frmWebTT100_Load);
        }

        void frmWebTT100_Load(object sender, EventArgs e)
        {
            string url = Global.GetConfig().GetConfigString("system", "TT100Url");
            m_WebKitBrowser.Navigate(url);
        }

        public void Active()
        {

        }

        public string GetName()
        {
            return "TT100";
        }
    }
}
