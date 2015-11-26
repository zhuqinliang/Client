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
    public partial class LockWindows : Form
    {
        //// 安装钩子 
        //[DllImport("user32.dll")]
        //public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //// 卸载钩子 
        //[DllImport("user32.dll")]
        //public static extern bool UnhookWindowsHookEx(int idHook);
        //// 继续下一个钩子 
        //[DllImport("user32.dll")]
        //public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        ////声明定义 
        //public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        //static int hKeyboardHook = 0;
        //HookProc KeyboardHookProcedure;

        public LockWindows()
        {
            InitializeComponent();
        }

        private void LockWindows_Resize(object sender, EventArgs e)
        {
            this.panel1.Left = (this.Width - this.panel1.Width) / 2;
            this.panel1.Top = (this.Height - this.panel1.Height) / 2;
        }

        private void PicLock_Click(object sender, EventArgs e)
        {
            this.Txt_PassWords.Visible = true;
            this.Txt_PassWords.Focus();
        }

        private void LockWindows_Load(object sender, EventArgs e)
        {
            //HookStart();
        }

        private void LockWindows_FormClosing(object sender, FormClosingEventArgs e)
        {
            //HookStop();
        }

        private void Txt_PassWords_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(this.Txt_PassWords.Text))
                {
                    Messagebox mb = new Messagebox();
                    PassValue.MessageInfor = "密码不能为空！";
                    mb.ShowDialog();
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.Txt_PassWords.Text) && this.Txt_PassWords.Text == PassValue.Code)
                    {
                        this.Close();
                    }
                    else
                    {
                        Messagebox mb = new Messagebox();
                        PassValue.MessageInfor = "密码错误！";
                        mb.ShowDialog();
                    }
                }
            }
        }
    }
}
