using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public enum frmWaitDialogType
    {
        WaitInitDevice,             //正在初始化设备
        WaitReadCard,               //正在读卡
        WaitInputPSW,               //等待输入密码
        WriteCardFailed              //写卡失败   
    }

    public partial class frmWaitDialog : Form
    {
        Action m_Action;
        frmWaitDialogType m_frmWaitDialogType;
        public frmWaitDialog(Action p_Action, frmWaitDialogType p_frmWaitDialogType)
        {
            InitializeComponent();

            m_Action = p_Action;
            m_frmWaitDialogType = p_frmWaitDialogType;
            m_Image = GetImage(m_frmWaitDialogType);
            this.Opacity = 0.85;
           
            this.SizeChanged += new EventHandler(frmWaitDialog_SizeChanged);

            this.MouseClick += new MouseEventHandler(frmWaitDialog_MouseClick);
        }

        public void SetfrmWaitDialogType(frmWaitDialogType p_frmWaitDialogType)
        {
            if (m_frmWaitDialogType == p_frmWaitDialogType)
            {
                return;
            }

            m_frmWaitDialogType = p_frmWaitDialogType;
            m_Image = GetImage(m_frmWaitDialogType);

            frmWaitDialog_SizeChanged(null, null);
            this.Refresh();
        }

        private Image GetImage(frmWaitDialogType p_frmWaitDialogType)
        {
            switch (p_frmWaitDialogType)
            {
                case frmWaitDialogType.WaitInitDevice:
                    return Client.Properties.Resources.WaitInitDevice;
                    
                case frmWaitDialogType.WaitReadCard:
                    return Client.Properties.Resources.WaitReadCard;

                case frmWaitDialogType.WriteCardFailed:
                    return Client.Properties.Resources.WriteCardFailed;
           

                case frmWaitDialogType.WaitInputPSW:
                    return Client.Properties.Resources.WaitInputPSW;
    
            }
            return null;
        }

        void frmWaitDialog_SizeChanged(object sender, EventArgs e)
        {
            pox = (this.Width - m_Image.Width) / 2;
            poy = (this.Height - m_Image.Height) / 3 * 2;
        }

        int pox = 0;
        int poy = 0;
        Image m_Image;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

           e.Graphics.DrawImage(m_Image, pox, poy, m_Image.Width, m_Image.Height);
        }

        void frmWaitDialog_MouseClick(object sender, MouseEventArgs e)
        {
            int left = 0;
            switch (m_frmWaitDialogType)
            {
                case frmWaitDialogType.WaitInitDevice:
                    left = 348;
                    break;
                case frmWaitDialogType.WaitReadCard:
                case frmWaitDialogType.WriteCardFailed:
                    left = 300;
                    break;

                case frmWaitDialogType.WaitInputPSW:
                    left = 428;
                    break;
            }

            if (e.X > pox + left && e.X < pox + m_Image.Width)
            {
                if (e.Y > poy && e.Y < poy + 41)
                {
                    if (m_Action != null)
                    {
                        m_Action();
                    }
                    this.Close();
                }
            }
        }
    }
}
