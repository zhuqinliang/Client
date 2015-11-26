using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public delegate void RetrievalDishesHandler(string p_RetrievaCode);

    /// <summary>
    /// 王海
    /// </summary>
    public partial class InputRetrievalDishesPanel : UserControl
    {
        public InputRetrievalDishesPanel()
        {
            InitializeComponent();
        }

        public event RetrievalDishesHandler RetrievalDishes;

        private void btnRetrieval_Click(object sender, EventArgs e)
        {
            if (RetrievalDishes != null)
            {
                RetrievalDishes(txtRetrievalCode.Text.Trim());
            }
        }

        public TextBox GetRetrievalCodeTextBox()
        {
            return txtRetrievalCode;
        }
    }
}
