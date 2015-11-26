using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business;
using System.Net;

namespace Client
{
    public partial class OpenTables : Form
    {
        private HttpAskfor httpReq = new HttpAskfor();

        public OpenTables()
        {
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OpenTables_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Properties.Resources.down;
            this.button1.Image = Properties.Resources.确定2;
            this.numericUpDown1.MaxLength = 4;
        }

        //开桌操作
        private void button1_Click(object sender, EventArgs e)
        {
            OpenDesk();
        }

        public void OpenDesk()
        {
            if (!string.IsNullOrEmpty(this.numericUpDown1.Text))
            {
                if (Int32.Parse(this.numericUpDown1.Text) > 0)
                {
                    Desk d;
                    d = (Desk)this.Owner;

                    string[] tables = PassValue.desk;
                    Consumption cp = new Consumption();
                    int count = tables.Count();
                    cp.tables = new Table[count];
                    for (int i = 0; i < count; i++)
                    {
                        cp.tables[i] = new Table();
                        cp.tables[i].id = tables[i];
                    }
                    cp.people = int.Parse(this.numericUpDown1.Text.ToString());

                    HttpResult httpResult = httpReq.HttpPost("consumptions", cp);
                    if ((int)httpResult.StatusCode == 409)
                    {
                        d.CurrentChooseDesk.Clear();
                        MessageBox.Show("有桌子已被操作，请重新选择！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

                  
                    d.Refresh_Method();
                    d.ChooseCurrent();
                    this.Close();
                    PassValue.count_select_idle = 0;//被选中的桌子数量为0
                    PassValue.count_select_ordering = count;
                    PassValue.selectedtableid.Clear();
                    this.DialogResult = DialogResult.OK;
                }
                else if (Int32.Parse(this.numericUpDown1.Text) == 0)
                {
                    MessageBox.Show("开桌人数不能为0！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
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
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                OpenDesk();
            }
        }
    }
}
