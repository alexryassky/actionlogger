using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;



namespace ActionLogger
{
    public partial class formStart : Form
    {
        /// <summary>
        /// Делегат для метода - обработчика, который вызывается в  момент добавления нового перехваченного события
        /// </summary>
        public delegate void OnIntercept(object sender, EventArgs ea);
        /// <summary>
        /// Экземпляр объекта-прослушивателя. В будущем это будет класс потока, слушающий DLL, а сейчас managed-класс.
        /// </summary>
        private cThreadHook hk = null;
        /// <summary>
        /// Поле для обработчика
        /// </summary>
        public OnIntercept intercept = null;
        public bool hookenabled = false;
        public ToolTip tpPickButton;
        public EventHandler setedittext = null;
        public bool Mode = false; //false = Window not captured , otherwise true

        public ToolStripCheckBox tscSelectWindow = null;
        public ToolStripCheckBox tsbStart = null;
        public formStart()
        {
            //ToDo : FindWindow (class = "WindowsForms10.Window.8.app.0.297b065_r13_ad1")
            InitializeComponent();
            intercept = AddInterceptedMessage;
            setedittext = SetEditHandleText;
            tscSelectWindow = new ToolStripCheckBox("Выбрать окно");
            tscSelectWindow.CheckBoxControl.Click += btnPickWindow_Click_1;
            mainmnu.Items.Add(tscSelectWindow);
            int nIndex = mainmnu.Items.IndexOf(tscSelectWindow);
            tsbStart = new ToolStripCheckBox("Start");
            tsbStart.CheckBoxControl.Click += btnStart_Click;
            mainmnu.Items.Insert(nIndex - 1,tsbStart);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          

            tpPickButton = new ToolTip();
            tpPickButton.SetToolTip(tscSelectWindow.CheckBoxControl, "Нажмите кнопку и кликните на желаемое окно (на заголовок!).");
            ucActionInfo.AnimateWindow(this.Handle, 500, ucActionInfo.AnimateWindowFlags.AW_CENTER| ucActionInfo.AnimateWindowFlags.AW_HOR_POSITIVE );

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!hookenabled)
            {
                
                try
                {
                    StartHook();
                    tsbStart.Text = "Stop";
                    hookenabled = !hookenabled;
                    try
                    {
                        cAppBarHelper.AddAppBar(this.Handle, true);
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка создания AppBar", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Left = 0;
                    Top = 0;
                    Height = Screen.PrimaryScreen.WorkingArea.Height;
                 //   TopMost = true;
                    tscSelectWindow.Enabled = false;
                }
                catch
                {
                    tsbStart.CheckBoxControl.Checked = false;
                }
            }
            else
            {
                tsbStart.Text = "Start";
                tsbStart.CheckBoxControl.Checked = false;
                
                hookenabled = !hookenabled;
                StopHook();
                tscSelectWindow.Enabled = true;
            }
        }

        public void StartHook()
        {
            try
            {   
                IntPtr TargetWindow = new IntPtr(Int32.Parse(edtHandle.Text, System.Globalization.NumberStyles.HexNumber));
                hk = new cThreadHook(this, TargetWindow);
                hk.EnableListeningHook();               
                hk.Start();
            }
            catch (Exception er)
            {
                if (er is FormatException)
                {
                    MessageBox.Show("Неправильный дескриптор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(er.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                throw new FormatException();
            }
        }

        public void StopHook()
        {
            if (!Mode)
                hk.DisableFindingHook();
            else
            {
                
                hk.DisableListeningHook();
            }
           
        }

        public void SetEditHandleText(object sender, EventArgs e)
        {
            tscSelectWindow.Checked = false;
            string hex = String.Format("000{0:X}", ActionHost.TargetWindow.ToInt32());
            edtHandle.Text = hex;       
            StopHook();
            Invalidate();
            Refresh();
            
        }

        /// <summary>
        /// Обработчик события Add в классе ThreadHook
        /// </summary>
        public void AddInterceptedMessage(object sender, EventArgs ea)
        {
            panActions.SuspendLayout();
            UserAction uaIterator = ActionHost.Actions.Last<UserAction>();

            ucActionInfo aiTest = new ucActionInfo((uaIterator.KeyCode>1?"Клавиатура":"Мышь") , new Point(0, 0), uaIterator.ControlHandle, uaIterator.EventCode);
            if (ActionHost.Actions.Count % 2 == 0)
            {              
                aiTest.BackColor = Color.LightSteelBlue;
            }
            else
                aiTest.BackColor = Color.FromArgb(255, Color.White);
      
            aiTest.BriefInfo = uaIterator.BriefInfo;
            panActions.Controls.Add(aiTest);
            aiTest.Dock = DockStyle.Top;
            aiTest.BringToFront();
      
            aiTest.Show();
            panActions.VerticalScroll.Value= panActions.VerticalScroll.Maximum;
            panActions.ResumeLayout();
            Invalidate();
            Refresh();

        }

        private void btnPickWindow_MouseUp(object sender, MouseEventArgs e)
        {
       
       
        }

        private void btnPickWindow_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnPickWindow_Click_1(object sender, EventArgs e)
        {
            hk = new cThreadHook(this, IntPtr.Zero);
            hk.EnableFindingHook();
            hk.Start();

            tscSelectWindow.Checked = true;
            Mode = true;
            TopMost = true;
        }

        private void formStart_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            ucActionInfo.AnimateWindow(this.Handle, 500, ucActionInfo.AnimateWindowFlags.AW_HIDE | ucActionInfo.AnimateWindowFlags.AW_CENTER );
        }

        private void btnMakeReport_Click(object sender, EventArgs e)
        {
           
        }

      
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        private void загрузитьОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mainmnu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mnuShowReport_Click(object sender, EventArgs e)
        {
            frmReport fReport = new frmReport();
            fReport.TopMost = true;
            ucActionInfo.AnimateWindow(fReport.Handle, 200, ucActionInfo.AnimateWindowFlags.AW_SLIDE | ucActionInfo.AnimateWindowFlags.AW_VER_POSITIVE | ucActionInfo.AnimateWindowFlags.AW_HOR_POSITIVE);
            fReport.Show(this);
        }

        private void mnuShowSettings_Click(object sender, EventArgs e)
        {
            frmSettings settingsForm = new frmSettings();
            settingsForm.ShowDialog();
           
            
         }

        public static ActionHost.ItemAdded handler = new ActionHost.ItemAdded((object sender, EventArgs e) =>
            {

                formStart frm = (sender as formStart);
                frm.AddInterceptedMessage(sender, e);
            }
        );

        private void mnuLoadReport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpenReport = new OpenFileDialog();
            dlgOpenReport.Filter  = "Текстовый файл |*.txt";
            ActionHost.OnItemAdded += AddInterceptedMessage;
            if (dlgOpenReport.ShowDialog() == DialogResult.OK)
            {
                Func.LoadFromXML(dlgOpenReport.FileName, ref ActionHost.Actions);
                
            }


        }

        private void ClearLog()
        {
            ActionHost.Actions.Clear();
            panActions.Controls.Clear();

        }

        private void mnuClearImmediately_Click(object sender, EventArgs e)
        {
            ClearLog();
        }
    }
}
