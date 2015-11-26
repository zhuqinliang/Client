using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using e7;
using System.Threading;
using Business;

namespace Client
{
    public partial class InputPassWord : Form
    {
        DeivceE7Impl E7 = new DeivceE7Impl();
        public delegate void SetTextLabel(Label lb, string str);
        public delegate void SetTextTextBox(TextBox tb, string str);
        public delegate void CloseWindow();
        private Thread t;

        public InputPassWord()
        {
            InitializeComponent();
        }

        private void InputPassWord_Click(object sender, EventArgs e)
        {
            this.Btn_Canel.Image = Properties.Resources.取消;
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Canel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void InputPassWord_Load(object sender, EventArgs e)
        {
            t = new Thread(new ThreadStart(ReadKey));
            t.Start();
        }

        private void ReadKey() 
        {
            while (true)
            {
                try
                {
                    //打开设备
                    string openResult = E7.openDevice();
                    if (int.Parse(openResult) <= 0)
                    {
                        SetLabel(this.lbShowMsg, "没有找到设备！");
                        return;
                    }

                    SetLabel(this.lbShowMsg, "请输入密码！");
                    E7.DisplayLcd("请输入密码！");
                    string password = E7.readKeyInPass();
                    SetTextBox(this.txtPwd, password);

                    if (PassValue.MemberCardPwd == password)
                    {
                        SetLabel(this.lbShowMsg, "密码正确！");
                        Thread.Sleep(1000);
                        SetWindowClose();
                        break;
                    }
                    else
                    {
                        E7.DisplayLcd("密码输错误，请确认！");
                        SetLabel(this.lbShowMsg, "输入错误，请重新输入！");
                        Thread.Sleep(1000);
                        SetTextBox(this.txtPwd, "");
                    }
                }
                catch (Exception)
                {

                }
                finally 
                {
                    E7.closeDevice();
                }
            }
        }

        public void SetLabel(Label lb, string labelText)
        {
            SetTextLabel mi = new SetTextLabel(SetLbText);
            Invoke(mi, new object[] { lb, labelText });
        }

        public void SetTextBox(TextBox tb, string textText)
        {
            SetTextTextBox mi = new SetTextTextBox(SetTxtText);
            Invoke(mi, new object[] { tb, textText });
        }

        public void SetWindowClose()
        {
            CloseWindow mi = new CloseWindow(CloseWindowM);
            Invoke(mi, new object[] { });
        }

        public void SetTxtText(TextBox tb, string textText)
        {
            tb.Text = textText;
        }

        public void SetLbText(Label lb, string labelText)
        {
            lb.Text = labelText;
        }

        public void CloseWindowM()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InputPassWord_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //t.Suspend();
                t.Abort();
            }
            catch
            {
            }
        }

        private void Btn_OK_Click_1(object sender, EventArgs e)
        {
            if (PassValue.MemberCardPwd == this.txtPwd.Text)
            {
                this.lbShowMsg.Text = "密码正确！";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.lbShowMsg.Text = "输入错误，请重新输入！";
                this.txtPwd.Text = "";
            }
        }
    }
}
