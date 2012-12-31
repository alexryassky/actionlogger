using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace ActionLogger
{
    
    public partial class ucActionInfo : UserControl
    {
        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr hwnd, int time, AnimateWindowFlags flags);

        public enum AnimateWindowFlags : uint
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }


        public const int MOUSE_WPARAM = 1;
        
        #region fields
        private string _strBriefInfo = "";
        private string _strFullInfo = "WM_NULL -- тест --- ";
        private ToolTip tpBtnCursor;
        private ToolTip tpBtnRepeat;
        private Point ptMouseCoords = new Point (0,0);
        private IntPtr pControlHandle = IntPtr.Zero;
        private DateTime dtActionTime = DateTime.Now;
        private bool _mouseevent = false;
        private bool _keyboardevent = true;
        private System.Windows.Forms.Message _eventcode = new System.Windows.Forms.Message();
        private uint threadid = 0;
        private int actionIndex = -1;
        #endregion

        #region Properties
        public System.Windows.Forms.Message EventCode 
        {
            get { return _eventcode; }
            set { _eventcode = value; }
        } 

        public bool Keyboard_event 
        { 
            get { return _keyboardevent;}
            set {
                    _keyboardevent = value;
                   
                }
        } 
        public bool Mouse_event 
        {
            get { return _mouseevent; } 
            set {
                _mouseevent = value;
               
                }
        }

        public uint ThreadId
        {
            get 
            { 
                return threadid;
            } 

            set 
            {
                threadid = value;
            }
                
        }

        public string BriefInfo
        {   get { return _strBriefInfo; }
            set { _strBriefInfo = value; lblBriefInfo.Text = value; }
        }

        public string FullInfo 
        {
            get { return _strFullInfo; } 
            set {_strFullInfo = value;  }
        }
        public Point MouseCoords
        {
            get { return ptMouseCoords; } 
            set { ptMouseCoords = value;} 
                
        }
        public IntPtr  ControlHandle 
        { 
            get { return pControlHandle; }
            set { pControlHandle = value;}
        }
        #endregion

        public ucActionInfo()
        {
          
            InitializeComponent();
            tpBtnCursor = new ToolTip();
            tpBtnCursor.SetToolTip(btnSetCursor, "Поместить курсор на позицию действия");
            tpBtnRepeat = new ToolTip();
            tpBtnRepeat.SetToolTip(btnRepeat,"Повторить все действия, начиная от первого, заканчивая данным");
        }

        public ucActionInfo(string brief,  Point mouse, IntPtr hWND, System.Windows.Forms.Message eventcode, int index = -1)
        {
            this.ptMouseCoords = mouse;
            this._strBriefInfo = brief;
            this.actionIndex = ActionHost.Actions.Count()-1 ; //TODO убрать нафиг
            this.ControlHandle = hWND;
            this.EventCode = eventcode;

            if (eventcode.WParam.ToInt32() == MOUSE_WPARAM) //TODO Сделаь switch по message_code
            {
            
                int mouseX = eventcode.LParam.ToInt32() & 0x0000ffff;
                int mouseY = (int)((eventcode.LParam.ToInt32() & 0xffff0000) >> 16);

                this.MouseCoords = new Point(mouseX, mouseY);
               
                this._strFullInfo = "X:"+(mouseX).ToString()+" Y:"+(mouseY).ToString();

            }
            else
            {
                this._strFullInfo = eventcode.ToString();
            }
            InitializeComponent();
            this.lblBriefInfo.Text = this.BriefInfo;
            this.lblFull.Text = this._strFullInfo;

            
        }

        
        private void btnRepeat_Click(object sender, EventArgs e)
        {
            UserAction uact = ActionHost.Actions.ElementAt<UserAction>(this.actionIndex);
            uact.Repeat(true);

            switch (Mouse_event)
            {
                case true: //
                    {
                        // Attach to thread
                        // Send mouse event

                    }
                    break;

                default: // Keyboard events
                    {
                        // Attach to thread
                        // Send keys

                    }
                    break;
            }
        }

        private void btnCursor_Click(object sender, EventArgs e)
        {
             long mouseX = EventCode.LParam.ToInt32() & 0x0000ffff;
             long mouseY = ((EventCode.LParam.ToInt32() & 0xffff0000) >> 16);
             cCursorHelper.SetCursorPos((int)mouseX , (int)mouseY);
        }

        private void btnSetCursor_Click(object sender, EventArgs e)
        {

        }

        private void btnRepeatTill_Click(object sender, EventArgs e)
        {
          
            
            UserAction uaLastAction = ActionHost.Actions.Last<UserAction>();
            uaLastAction.OnAfterRepeat += new UserAction.AfterRepeatHandler(uaLastAction.DetachThread) ;
            for (int i = 0; i <= this.actionIndex; i++)
            {
               
              
                UserAction uact = ActionHost.Actions.ElementAt<UserAction>(i);
                uact.Repeat(true);
                System.Threading.Thread.Sleep((int)(Properties.Settings.Default.delay * 1000));
               
            }
        }
    }
}
