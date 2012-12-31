using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ActionLogger
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
            string strReport = Func.MakeReport();
            rtbReport.Text = strReport;
        }

    
        private void mnuSave_Click(object sender, EventArgs e)
        {
            string strReport = Func.MakeReport();
            Func.SaveToXML(strReport);
        }

        private void frmReport_Activated(object sender, EventArgs e)
        {
        
        }
    }
}
