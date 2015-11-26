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
    public partial class frmWebDishes : Form, IView
    {
        WebKit.WebKitBrowser m_WebKitBrowser = new WebKit.WebKitBrowser();
        public frmWebDishes()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            m_WebKitBrowser.Dock = DockStyle.Fill;
            this.Controls.Add(m_WebKitBrowser);
          
            this.Load += new EventHandler(frmWebDishes_Load);
        }

        void frmWebDishes_Load(object sender, EventArgs e)
        {
            string url = Global.GetConfig().GetConfigString("system", "DishesUrl");
            m_WebKitBrowser.Navigate(url);
        }

        public void Active()
        {

        }

        public string GetName()
        {
            return "菜品";
        }
    }
}
