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
    public partial class Messagebox : Form
    {
        public Messagebox()
        {
            InitializeComponent();
        }

        private void Messagebox_Load(object sender, EventArgs e)
        {
            this.lbMessage.Text = PassValue.MessageInfor;
            this.lbMessage.Left = (this.Width - this.lbMessage.Width) / 2;
            this.pictureBox3.Image = Properties.Resources.down;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox3.Image = Properties.Resources.down;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox3.Image = Properties.Resources.downpre;
        }

        private void picEnter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Messagebox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.Close();
            }
        }
    }
}
