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
    public partial class frmWebWarehouse : Form, IView
    {
        WebKit.WebKitBrowser m_WebKitBrowser = new WebKit.WebKitBrowser();
        public frmWebWarehouse()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            m_WebKitBrowser.Dock = DockStyle.Fill;
            this.Controls.Add(m_WebKitBrowser);

            this.Load += new EventHandler(frmWebWarehouse_Load);
        }

        void frmWebWarehouse_Load(object sender, EventArgs e)
        {
            string url = Global.GetConfig().GetConfigString("system", "WarehouseUrl");

            m_WebKitBrowser.Navigate(url);
        }

        public void Active()
        {

        }

        public string GetName()
        {
            return "库管";
        }
    }
}
