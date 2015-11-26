using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using Business;
using Kernel;
using Client.Properties;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        HttpAskfor httpReq = new HttpAskfor();

        private bool needRemPassword = false;
        public bool needHandOver = false;

        #region 窗体的加载
        /// <summary>
        /// 登录窗体的加载事件
        /// </summary>
        private void Login_Load(object sender, EventArgs e)
        {
            this.TxtUser.BackColor = Color.FromArgb(93, 76, 46);
            this.TxtKey.BackColor = Color.FromArgb(93, 76, 46);
            this.TxtUser.Text = Global.GetConfig().GetConfigString("system", "LoginUserName");

            needRemPassword = bool.Parse(Global.GetConfig().GetConfigString("system", "NeedRemPwd"));
            if (needRemPassword)
            {
                this.pictureBox3.Image = Properties.Resources.login_password_sel;
                this.TxtKey.Text = Global.GetConfig().GetConfigString("system", "LoginUserPSW");
            }
            else
            {
                this.pictureBox3.Image = Properties.Resources.login_password_nor;
                this.TxtKey.Text = "";
            }
        }
        #endregion

        #region 窗体的缓冲
        /// <summary>
        /// 开启双缓冲
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion

        #region 用户名的设置
        /// <summary>
        /// 用户名的离开事件
        /// </summary>
        private void TxtUser_Leave(object sender, EventArgs e)
        {
            if (this.TxtUser.Text == "")
            {
                this.TxtUser.Text = "请输入用户名";
                this.TxtUser.ForeColor = SystemColors.ScrollBar;
            }
        }

        /// <summary>
        /// 用户名的进入事件
        /// </summary>
        private void TxtUser_Enter(object sender, EventArgs e)
        {
            if (this.TxtUser.Text == "请输入用户名")
            {
                this.TxtUser.Text = "";
                this.TxtUser.ForeColor = Color.White;
            }
        }
        #endregion

        #region 密码的设置
        /// <summary>
        /// 密码的离开事件
        /// </summary>
        private void TxtKey_Leave(object sender, EventArgs e)
        {
            if (this.TxtKey.Text == "")
            {
                this.TxtKey.Text = "请输入密码";
                this.TxtKey.ForeColor = SystemColors.ScrollBar;
                this.TxtKey.PasswordChar = new char();
            }
        }

        /// <summary>
        /// 密码的进入事件
        /// </summary>
        private void TxtKey_Enter(object sender, EventArgs e)
        {
            if (this.TxtKey.Text == "请输入密码")
            {
                this.TxtKey.Text = "";
                this.TxtKey.ForeColor = Color.White;
                this.TxtKey.PasswordChar = '●';
            }
        }
        #endregion

        #region 登录按钮
        /// <summary>
        /// 登录事件
        /// </summary>
        private void picLogin_Click(object sender, EventArgs e)
        {
            if (this.TxtUser.Text == "请输入用户名" || this.TxtKey.Text == "请输入密码" || this.TxtUser.Text == "" || this.TxtKey.Text == "")//判断是否为空
            {
                PassValue.MessageInfor = "用户名和密码不能为空";
                Messagebox mb = new Messagebox();
                mb.ShowDialog();
                this.TxtKey.Text = "请输入密码"; this.TxtUser.Text = "";
                this.TxtUser.Focus();
            }
            else
            {
                PassValue.username = this.TxtUser.Text;//用户名
                PassValue.password = this.TxtKey.Text;//密码
                string passvalue = PassValue.username + ":" + PassValue.password;
                WebHeaderCollection headers = new WebHeaderCollection();
                headers.Add("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(passvalue)));
                headers.Add("Authed", "true");
                LoginID li = null;

                HttpResult httpResult = httpReq.HttpPost("auth/tokens", null, headers);
                if ((int)httpResult.StatusCode == 0)
                {
                    MessageBox.Show(string.Format("{0}{1}", httpResult.StatusDescription, httpResult.OtherDescription), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                //else if ((int)httpResult.StatusCode == 403)
                //{
                //    if (MessageBox.Show("用户已在其他地方登陆 是否强制登陆？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                //    {
                //        headers.Add("Authed", "true");
                //        httpResult = httpReq.HttpPost("auth/tokens", null, headers);
                //    }
                //    else
                //    {
                //        return;
                //    }
                //}

                if ((int)httpResult.StatusCode >= 200 && (int)httpResult.StatusCode < 300)
                {
                    li = httpReq.ScriptDeserialize<LoginID>(httpResult.Html);
                    PassValue.token = "Token " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(li.id));//ID加密         
                    PassValue.tokenid = li.id;
                    PassValue.Code = li.principal.code;
                    if (needRemPassword)
                    {
                        Global.GetConfig().SetConfigString("system", "LoginUserName", this.TxtUser.Text.Trim());
                        Global.GetConfig().SetConfigString("system", "LoginUserPSW", this.TxtKey.Text.Trim());
                    }

                    //获取楼层信息
                    Layout floorLayout = httpReq.HttpGet<Layout>("layout");
                    if (floorLayout != null)
                    {
                        foreach (Floor floor in floorLayout.floors)
                        {
                            PassValue.tablesstatuslist.Add(floor.number);
                        }
                        PassValue.tablesstatuslist.Sort();
                    }

                    //获取桌子信息
                    PassValue.Tables = httpReq.HttpGet<List<Table>>("tables");
                    
                    //获取交接班需要的信息
                    Shift shift = new Shift();
                    shift.cashier = new Employee();
                    if (li != null)
                    {
                        shift.cashier.id = li.principal.id;
                    }

                    httpResult = httpReq.HttpPost("shifts", shift);
                    if ((int)httpResult.StatusCode >= 200 && (int)httpResult.StatusCode < 300)
                    {
                        shift = httpReq.ScriptDeserialize<Shift>(httpResult.Html);
                        PassValue.shiftId = shift.id;
                        needHandOver = shift.isWarn;
                    }
                    else if ((int)httpResult.StatusCode == 0)
                    {
                        MessageBox.Show(string.Format("{0}{1}", httpResult.StatusDescription, httpResult.OtherDescription), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                    Main frmMain = new Main(this);
                    frmMain.Show();
                }
                else
                {
                    MessageBox.Show("请输入正确的工号和密码！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.TxtKey.Text = "";
                    this.TxtUser.Text = "";
                    this.TxtUser.Focus();
                }
            }
        }
        #endregion

        #region 登录设置
        /// <summary>
        /// 登录信息的设置
        /// </summary>
        private void pictureBoxSetup_Click(object sender, EventArgs e)
        {
            History hs = new History();
            hs.ShowDialog();
        }
        #endregion

        #region 窗体的适配
        /// <summary>
        /// 登录窗体的大小变化事件
        /// </summary>
        private void Login_Resize(object sender, EventArgs e)
        {
            this.pictureBox1.Parent = this.picUser;
            this.pictureBox3.Parent = this.picKey;
            this.pictureBox2.Parent = this.picKey;
            this.picLogin.Parent = this.picCardOut;
            this.pictureBoxLogo.Parent = this.picBackImage;
            this.pictureBoxSetup.Parent = this.picBackImage;
            this.picUser.Parent = this.picCardOut;
            this.picKey.Parent = this.picCardOut;
            
            this.panelCardOut.Left = (this.Width - this.panelCardOut.Width) / 2;
            this.panelCardOut.Top = (this.Height - this.panelCardOut.Height) / 22 * 12;
            this.pictureBoxLogo.Left = (this.Width - this.pictureBoxLogo.Width) / 2;
            this.pictureBoxLogo.Top = (this.Height - this.pictureBoxLogo.Height) / 11 * 2;
            this.panelCardOut.Parent = this.picBackImage;

            this.pictureBox1.Location = new System.Drawing.Point(30, this.picUser.Height / 2 - this.pictureBox1.Height / 2);
            this.pictureBox2.Location = new System.Drawing.Point(17, this.picKey.Height / 2 - this.pictureBox2.Height / 2);
            this.pictureBox3.Location = new System.Drawing.Point(245, this.picKey.Height / 2 - this.pictureBox2.Height / 2);
        }
        #endregion

        #region 登录的快捷键ENTER
        /// <summary>
        /// 登录的快捷方式
        /// </summary>
        private void TxtKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                picLogin_Click(sender, e);
            }
        }
        #endregion

        #region 保存用户名和密码
        /// <summary>
        /// 保存用户名和密码
        /// </summary>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (needRemPassword)
            {
                needRemPassword = false;
                Global.GetConfig().SetConfigString("system", "NeedRemPwd", "false");
                this.pictureBox3.Image = Properties.Resources.login_password_nor;
                this.TxtKey.Text = "";
            }
            else
            {
                needRemPassword = true;
                Global.GetConfig().SetConfigString("system", "NeedRemPwd", "true");
                this.pictureBox3.Image = Properties.Resources.login_password_sel;
            }
        }
        #endregion


        private void picLogin_MouseEnter(object sender, EventArgs e)
        {
            this.picLogin.Image = Properties.Resources.loginmouseenter;
        }

        private void picLogin_MouseLeave(object sender, EventArgs e)
        {
            this.picLogin.Image = Properties.Resources.loginmouseleave;
        }
    }
}
