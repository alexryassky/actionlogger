using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace ActionLogger
{
    public class ToolStripCheckBox : ToolStripControlHost
    {
        
        public ToolStripCheckBox(string Caption) : base(new CheckBox())
        {
            CheckBoxControl.Appearance = Appearance.Button;
            CheckBoxControl.BackColor = Color.Transparent;
            Text = Caption;
         
        }

        public CheckBox CheckBoxControl
        {
            get { return (CheckBox)Control; }
        }

        public bool Checked
        {
            get { return CheckBoxControl.Checked; }
            set { CheckBoxControl.Checked = value; }
        }
    }
}
