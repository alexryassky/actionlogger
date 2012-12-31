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
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            uint delay = 1;
            if (UInt32.TryParse(edtDelay.Text, out delay))
            {
                Properties.Settings.Default.delay = delay;
                Close();
            }
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            edtDelay.Text = Properties.Settings.Default.delay.ToString();
            chbForceMaximize.Checked = Properties.Settings.Default.forceMaximize;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void chbForceMaximize_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.forceMaximize = chbForceMaximize.Checked;
        }
    }
}
