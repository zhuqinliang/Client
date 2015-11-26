using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business;

namespace Client
{
    public partial class ChangeNumber : Form
    {
        private string consumeid = "";
        private HttpAskfor httpReq = new HttpAskfor();

        public ChangeNumber(string _consumeid)
        {
            consumeid = _consumeid;
            InitializeComponent();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.pictureBox1.Image = Properties.Resources.downpre;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Properties.Resources.down;
        }

        //关闭按钮
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void OpenTables_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Properties.Resources.down;
            this.button1.Image = Properties.Resources.确定2;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            this.button1.Image = Properties.Resources.确定;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.Image = Properties.Resources.确定2;
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > '0' && e.KeyChar <= '9') || e.KeyChar == ' ' || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Consumption c = new Consumption()
            {
                id = consumeid,
                people=int.Parse( this.numericUpDown1.Text)
            };

            HttpResult httpResult = httpReq.HttpPatch(string.Format("consumptions/{0}", PassValue.consumptionid), c);
            if ((int)httpResult.StatusCode == 409)
            {
                MessageBox.Show("有桌子已被操作，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ((int)httpResult.StatusCode == 401)
            {
                LoginBusiness lg = new LoginBusiness();
                lg.LoginAgain();
                return;
            }
            else if ((int)httpResult.StatusCode == 0)
            {
                MessageBox.Show(string.Format("{0}{1}", httpResult.StatusDescription, httpResult.OtherDescription), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            Desk d;
            d = (Desk)this.Owner;
            d.Refresh_Method();
            this.Close();
        }
    }
}
