using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Client
{
    public enum ControlState
    {
        Normal,
        Hover,
        Pressed,
        Disable
    }

    public partial class ImageButtonEx : Control
    {
        /// <summary>
        /// Module ID    :ImageButton
        /// Depiction    :提供4种状态下，切换不同背景图片的按钮 ButtonStatus { Normal, Hover, Active, Grayed }
        /// Author       :wanghai
        /// Create Date  :2010-06-06
        /// </summary>
        /// 

        static Font defaultFont = new Font("宋体", 9);
        protected Image m_imgNormal;              //通常状态
        protected Image m_imgHover;               //Hover状态
        protected Image m_imgActive;              //Active状态
        protected Image m_imgGrayed;              //Grayed状态
        protected ControlState m_ControlState;   //标记所处的状态
        protected ToolTip m_ToolTip;
        protected string m_ToolTipText;
        protected bool m_ShowTooltip = false;
        bool m_IsCustomButton = false;          //是否是默认按钮
        bool m_IsTabButton = false;
        public bool IsTabButton
        {
            get { return m_IsTabButton; }
            set
            { 
                m_IsTabButton = value;
                if (m_IsCustomButton)
                    m_IsCustomButton = true;
            }
        }
        private string m_ButtonText = "";
        public string ButtonText
        {
            get { return m_ButtonText; }
            set
            {
                m_ButtonText = value;
                this.Refresh();
            }
        }
        private Font m_NormalFont = defaultFont;
        public Font NormalFont
        {
            get { return m_NormalFont; }
            set
            {
                m_NormalFont = value;
                m_ActiveFont = value;
                m_HoverFont = value;
                m_GrayedFont = value;
            }
        }
        private Font m_HoverFont = defaultFont;
        public Font HoverFont
        {
            get { return m_HoverFont; }
            set { m_HoverFont = value; }
        }
        private Font m_ActiveFont = defaultFont;
        public Font ActiveFont
        {
            get { return m_ActiveFont; }
            set { m_ActiveFont = value; }
        }
        private Font m_GrayedFont = defaultFont;
        public Font GrayedFont
        {
            get { return m_GrayedFont; }
            set { m_GrayedFont = value; }
        }
        private Color m_NormalForeColor = Color.Black;
        public Color NormalForeColor
        {
            get { return m_NormalForeColor; }
            set
            {
                m_NormalForeColor = value;
                m_HoverForeColor = value;
                m_ActiveForeColor = value;
                m_GrayedForeColor = value;
            }
        }
        private Color m_HoverForeColor = Color.Black;
        public Color HoverForeColor
        {
            get { return m_HoverForeColor; }
            set { m_HoverForeColor = value; }
        }
        private Color m_ActiveForeColor = Color.Black;
        public Color ActiveForeColor
        {
            get { return m_ActiveForeColor; }
            set { m_ActiveForeColor = value; }
        }
        private Color m_GrayedForeColor = Color.Gray;
        public Color GrayedForeColor
        {
            get { return m_GrayedForeColor; }
            set { m_GrayedForeColor = value; }
        }
        private Point m_TextOffset;
        bool m_bHaveTextOffset = false;
        public Point TextOffset
        {
            get { return m_TextOffset; }
            set
            {
                m_TextOffset = value;
                m_bHaveTextOffset = true;
            }
        }
        bool m_AutoChangeStatus = true;
        public bool AutoChangeStatus
        {
            get { return m_AutoChangeStatus; }
            set { m_AutoChangeStatus = value; }
        }
        object m_Data = null;
        public object Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }
        string m_Id;
        public string Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        /// <summary>
        /// 单击事件
        /// </summary>
        public string ToolTipText
        {
            get
            {
                return m_ToolTipText;
            }
            set
            {
                m_ToolTipText = value;
            }
        }
        protected Image m_wholeImage = null;
        public virtual Image WholeImage
        {
            get
            {
                return m_wholeImage;
            }
            set
            {
                m_wholeImage = value;
                this.Width = m_wholeImage.Width;
                m_IsCustomButton = true;
                Refresh();
            }
        }
        /// <summary>
        /// Depiction:set BrushNormal
        /// </summary>
        [Browsable(true)]
        public virtual Image NormalImage
        {
            set
            {
                if (value != null)
                {
                    m_imgNormal = value;
                    this.Size = new Size(m_imgNormal.Width, m_imgNormal.Height);
                    m_IsCustomButton = true;
                }
            }
            get
            {
                return m_imgNormal;
            }
        }
        /// <summary>
        /// Depiction:set BrushHover
        /// </summary>
        [Browsable(true)]
        public Image HoverImage
        {
            set
            {
                m_imgHover = value;
                m_IsCustomButton = true;
            }
            get
            {
                return m_imgHover;
            }
        }

        /// <summary>
        /// Depiction:set BrushActive
        /// </summary>
        [Browsable(true)]
        public Image ActiveImage
        {
            set
            {
                m_imgActive = value;
                m_IsCustomButton = true;
            }
            get
            {
                return m_imgActive;
            }
        }
        public Image GrayedImage
        {
            set
            {
                m_imgGrayed = value;
                m_IsCustomButton = true;
            }
        }
        public ControlState ControlState
        {
            get
            {
                return m_ControlState;
            }
            set
            {
                m_ControlState = value;
                this.Refresh();
            }
        }
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                ControlState = value ? ControlState.Normal : ControlState.Disable;
                this.Refresh();
            }
        }
        public ImageButtonEx()
        {
            ControlState = ControlState.Normal;                  //默认为Normal
            m_ToolTip = new ToolTip();
            m_ToolTipText = "";

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.FromArgb(0, Color.Black);
            this.SetStyle(ControlStyles.DoubleBuffer, true);        //闪烁缓存

            this.MouseUp += new MouseEventHandler(ImageButton_MouseUp);
            this.MouseMove += new MouseEventHandler(ImageButton_MouseMove);
            this.MouseDown += new MouseEventHandler(ImageButton_MouseDown);
            this.MouseClick += new MouseEventHandler(ImageButton_MouseClick);
            this.MouseLeave += new EventHandler(ImageButton_MouseLeave);

            Init();
        }

        private void Init()
        {
            m_NormalForeColor = Color.White;
            m_HoverForeColor = Color.White;
            m_ActiveForeColor = Color.White;
            m_GrayedForeColor = Color.White;

            this.NormalImage = Client.Properties.Resources.btn_bg_nor;
            this.HoverImage = ActiveImage = Client.Properties.Resources.btn_bg_pre;
            this.GrayedImage = Client.Properties.Resources.btn_bg_gray;

            this.Height = this.NormalImage.Height;

            NormalFont = new Font("微软雅黑", 12);
          
        }
        protected virtual void DrawText(Graphics g, Font font, Color color, Point pt)
        {
            if (string.IsNullOrEmpty(m_ButtonText))
                return;
            if (m_bHaveTextOffset)
                g.DrawString(m_ButtonText, font, new SolidBrush(color), m_TextOffset.X + pt.X, m_TextOffset.Y + pt.Y);
            else
            {
                SizeF s = g.MeasureString(m_ButtonText, font);
                g.DrawString(m_ButtonText, font, new SolidBrush(color),
                    (ClientRectangle.Width - s.Width) / 2 + pt.X,
                    (ClientRectangle.Height - (int)s.Height) / 2 + 1 + pt.Y);
            }
        }
        public void DrawButton(Graphics graphics, Point pt, ControlState status)
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bitmap);
            if (m_wholeImage != null)
            {
                switch (status)
                {
                    case ControlState.Hover:
                        g.DrawImage(m_wholeImage, new Rectangle(pt.X, pt.Y, this.Width, this.Height), new Rectangle(0, 1 * this.Height, m_wholeImage.Width, this.Height), GraphicsUnit.Pixel);
                        DrawText(g, HoverFont, HoverForeColor, pt);
                        break;
                    case ControlState.Pressed:
                        g.DrawImage(m_wholeImage, new Rectangle(pt.X, pt.Y, this.Width, this.Height), new Rectangle(0, 2 * this.Height, m_wholeImage.Width, this.Height), GraphicsUnit.Pixel);
                        DrawText(g, ActiveFont, ActiveForeColor, pt);
                        break;
                    case ControlState.Disable:
                        g.DrawImage(m_wholeImage, new Rectangle(pt.X, pt.Y, this.Width, this.Height), new Rectangle(0, 3 * this.Height, m_wholeImage.Width, this.Height), GraphicsUnit.Pixel);
                        DrawText(g, GrayedFont, GrayedForeColor, pt);
                        break;
                    default:        //画Normal状态的m_imgNormalBackGround背景图片
                        g.DrawImage(m_wholeImage, new Rectangle(pt.X, pt.Y, this.Width, this.Height), new Rectangle(0, 0 * this.Height, m_wholeImage.Width, this.Height), GraphicsUnit.Pixel);
                        DrawText(g, NormalFont, NormalForeColor, pt);
                        break;
                }
            }
            else
            {
                switch (status)
                {
                    case ControlState.Hover:
                        if (m_imgHover != null)
                        {
                            g.DrawImage(m_imgHover, new Rectangle(pt.X, pt.Y, this.Width, this.Height));
                        }
                        else
                        {
                           g.DrawImage(m_imgNormal, new Rectangle(pt.X, pt.Y, this.Width, this.Height));
                        }
                        DrawText(g, HoverFont, HoverForeColor, pt);
                        break;
                    case ControlState.Pressed:
                        if (m_imgActive != null)
                        {
                            g.DrawImage(m_imgActive, new Rectangle(pt.X, pt.Y, this.Width, this.Height));
                        }
                        else
                        {
                            g.DrawImage(m_imgNormal, new Rectangle(pt.X, pt.Y, this.Width, this.Height));
                        }
                        DrawText(g, ActiveFont, ActiveForeColor, pt);
                        break;
                    case ControlState.Disable:
                        if (m_imgGrayed != null)
                            g.DrawImage(m_imgGrayed, new Rectangle(pt.X, pt.Y, this.Width, this.Height));
                        else
                            g.DrawImage(m_imgNormal, new Rectangle(pt.X, pt.Y, this.Width, this.Height));
                        DrawText(g, GrayedFont, GrayedForeColor, pt);
                        break;
                    default:        //画Normal状态的m_imgNormalBackGround背景图片

                        g.DrawImage(m_imgNormal, new Rectangle(pt.X, pt.Y, this.Width, this.Height));
                        DrawText(g, NormalFont, NormalForeColor, pt);


                        break;
                }
            }
            graphics.DrawImage(bitmap, 0, 0, this.Width, this.Height);
            g.Dispose();
            bitmap.Dispose();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            //调用基类 OnPaint
            base.OnPaint(pe);
            DrawButton(pe.Graphics, new Point(0, 0), m_ControlState);
        }
        void ImageButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_ToolTipText != "" && !m_ShowTooltip)
            {
                m_ShowTooltip = true;
                m_ToolTip.ShowAlways = true;
                m_ToolTip.Active = true;
                m_ToolTip.UseFading = true;
                m_ToolTip.SetToolTip(this, this.m_ToolTipText);
            }
            if (e.Button == MouseButtons.None && this.ControlState != ControlState.Disable)
            {
                SetStatus(ControlState.Hover);
            }
        }
        void ImageButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_ControlState == ControlState.Disable)
                return;
            if (e.Button != MouseButtons.Left)
                return;
            if (m_AutoChangeStatus)
                ControlState = ControlState.Pressed;

            if (ContextMenuStrip != null && this.ContextMenuStrip.Items.Count > 0)
                ContextMenuStrip.Show(this, new Point(0, this.Height), ToolStripDropDownDirection.BelowRight);
            this.Refresh();
        }
        void ImageButton_MouseUp(object sender, MouseEventArgs e)
        {
            m_ToolTip.ShowAlways = false;
            m_ToolTip.Active = false;
            if (m_ControlState == ControlState.Disable)
                return;
            SetStatus(ControlState.Hover);
        }
        void ImageButton_MouseLeave(object sender, EventArgs e)
        {
            if (m_ControlState == ControlState.Disable)
                return;
            m_ShowTooltip = false;
            m_ToolTip.ShowAlways = false;
            m_ToolTip.Active = false;

            SetStatus(ControlState.Normal);
        }
        void ImageButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_ControlState == ControlState.Disable)
                return;
            SetStatus(ControlState.Normal);
        }
        private void SetStatus(ControlState p_ButtonStatus)
        {
            if (m_AutoChangeStatus)
            {
                if (!IsTabButton)
                {
                    ControlState = p_ButtonStatus;
                }
                else if (ControlState != ControlState.Pressed)
                {
                    ControlState = p_ButtonStatus;
                }
                this.Refresh();
            }
        }
    }
}
