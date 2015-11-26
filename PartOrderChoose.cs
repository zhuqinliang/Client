using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business;
using System.Collections;

namespace Client
{
    public partial class PartOrderChoose : Form
    {
        private string consumption;
        public PartOrderChoose(string _consumption)
        {
            InitializeComponent();
            consumption = _consumption;
        }

        //取消
        private void picCanel_Click(object sender, EventArgs e)
        {
            hide();
        }

        public void hide()
        {
            PartOrder po = new PartOrder(consumption);
            po = (PartOrder)this.Owner;
            po.Visible = true;
            this.Close();
        }

        //取消(标签)
        private void lbCanel_Click(object sender, EventArgs e)
        {
            hide();
        }

        //确定
        private void picOk_Click(object sender, EventArgs e)
        {
            add();
        }

        public List<string> listpercent = new List<string>();//部分打折的菜品ID

        public void add()
        {
            foreach (ListViewItem ctl in this.lv.Items)
            {
                if (ctl.Checked)
                {
                    listpercent.Add(ctl.Tag.ToString());  
                }
            }
            PassValue.listItemID = new ArrayList(listpercent.ToArray());
            PartOrder po = new PartOrder(consumption);
            po = (PartOrder)this.Owner;
            po.lbCount.Text = PassValue.listItemID.Count.ToString();
            hide();
        }

        //确定(标签)
        private void lbOk_Click(object sender, EventArgs e)
        {
            add();
        }

        private void panFa_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
