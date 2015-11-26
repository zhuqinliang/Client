using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kernel;
using Business;

namespace Client
{
    public partial class History : Form,IView
    {
        public History()
        {
            InitializeComponent();

            LoadConfig();
        }

        private void LoadConfig()
        {
            this.TxtIp.Text = Global.GetConfig().GetConfigString("system", "MainUrlIp");
            this.TxtPort.Text = Global.GetConfig().GetConfigString("system", "MainUrlPort");

            string mode = Global.GetConfig().GetConfigString("system", "CallMode");
            if (mode=="1")
            {
                this.radioButton1.Checked = true;
            }
            else if (mode == "2")
            {
                this.radioButton2.Checked = true;
            }
            else if (mode == "3")
            {
                this.radioButton3.Checked = true;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox3.Image = this.imageListClose.Images[0];
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox3.Image = this.imageListClose.Images[1];
        }

        private void History_Load(object sender, EventArgs e)
        {
            this.pictureBox3.Image = this.imageListClose.Images[0];
        }

        //保存
        private void label1_Click(object sender, EventArgs e)
        {
            Global.GetConfig().SetConfigString("system", "MainUrlIp", this.TxtIp.Text.Trim());
            Global.GetConfig().SetConfigString("system", "MainUrlPort", this.TxtPort.Text.Trim());
            string mode = "1";
            if (this.radioButton1.Checked)
            {
                mode = "1";
            }
            if (this.radioButton2.Checked)
            {
                mode = "2";
            }
            if (this.radioButton3.Checked)
            {
                mode = "3";
            }
            Global.GetConfig().SetConfigString("system", "CallMode", mode);

            if (MessageBox.Show("立即重启系统将配置立即生效?", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                string path = Application.StartupPath + @"/点菜100.exe";
                System.Diagnostics.Process.Start(path);
                Environment.Exit(0);
            }
            this.Close();
        }

        //保存
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Global.GetConfig().SetConfigString("system", "MainUrlIp", this.TxtIp.Text.Trim());
            Global.GetConfig().SetConfigString("system", "MainUrlPort", this.TxtPort.Text.Trim());

            string mode = "1";
            if (this.radioButton1.Checked)
            {
                mode = "1";
            }
            if (this.radioButton2.Checked)
            {
                mode = "2";
            }
            if (this.radioButton3.Checked)
            {
                mode = "3";
            }
            Global.GetConfig().SetConfigString("system", "CallMode", mode);

            if (MessageBox.Show("立即重启系统将配置立即生效?", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                string path = Application.StartupPath + @"/点菜100.exe";
                System.Diagnostics.Process.Start(path);
                Environment.Exit(0);
            }
            this.Close();
        }

        #region IView 成员


        public void Active()
        {
           
        }

        public string GetName()
        {
            return "设置";
        }

        #endregion
    }
}
