using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControls;
using Kernel;

namespace Client
{
    public partial class frmQuery : frmCashier, IView, ILoadData
    {
        public frmQuery(string p_ConsumptionId)
            : base(p_ConsumptionId)
       {
           base.GetcqControl().ControlType = ConsumptionQueryControlType.Query;
       }

        public new string GetName()
        {
            return "历史查询";
        }
    }
}
