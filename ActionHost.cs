using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace ActionLogger
{
    /// <summary>
    ///  Накопитель сообщений о действиях пользователя. Модель.
    /// </summary>
    public static class ActionHost
    {
     
 
        /// <summary>
        /// Дескриптор целевого окна
        /// </summary>
        private static IntPtr targetWindow;

        /// <summary>
        /// Имя exe целевого окна
        /// </summary>
        public static string strTargetExeName;

        /// <summary>
        /// Границы целевого окна
        /// </summary>
        public static System.Drawing.Size szTargetBounds;

        /// <summary>
        ///Начальное положение целевого окна.    
        /// </summary>
        public static System.Drawing.Point ptWindowPoint = new System.Drawing.Point();


        /// <summary>
        /// Тип метода- обработчика Добавления элемента
        /// </summary>
        public delegate void ItemAdded (object sender, EventArgs ea);

        /// <summary>
        /// Тип метода- обработчика Определения окна
        /// </summary>
        public delegate void TargetWindowPicked ();

       
        /// <summary>
        /// Индекс в списке, до которого повторять
        /// </summary>
        public static int RepeatTo = -1;           
      
        /// <summary>
        /// Хранилище действий пользователя. Наполняется из процедуры перехвата события пользователя 
        /// </summary>
        public static  List<UserAction> Actions = new List<UserAction>(10);   
     
        /// <summary>
        /// Метод, вызывающийся, когда новое действие было добавлено
        /// </summary>
        public static event ItemAdded OnItemAdded = null;

        /// <summary>
        /// Метод, вызывающийся, когда окно было добавлено
        /// </summary>
        public static event TargetWindowPicked OnTargetWindowPicked = null;

        /// <summary>
        /// Флаг, идентифицирующий, зохвачен ли поток целевого окна
        /// </summary>
        public static bool ThreadCaptured = false;   
          
        public static void AddItem(UserAction item)
        {
            
                
            Actions.Add(item);
            //Raise event
            if (OnItemAdded != null)
            {
                OnItemAdded(null,new EventArgs());
            }
        }
        public static IntPtr TargetWindow
        {
            get
            {
                return targetWindow;
            }
            set
            {
                targetWindow = value;
                if (OnTargetWindowPicked != null)
                    OnTargetWindowPicked();
            }
        }


    }

    public enum UserActionTypes : int
    {
        USER_MOUSE_CLICK,
        USER_KEYBD_PRESS
    }
    



    /// <summary>
    /// Класс, представляющий одно действие пользователя
    /// </summary>
    public class UserAction
    {
        public delegate void RepeatHandler();
        public delegate void AfterRepeatHandler();
        #region fields

        private string _strBriefInfo = "";
        private System.Windows.Forms.Message _eventcode = new System.Windows.Forms.Message();
        private IntPtr pControlHandle = IntPtr.Zero;
      
        private uint threadid = 0;
        public int KeyCode = 0;
        public System.Drawing.Point MouseCoords;
        public UserActionTypes EventType;

        /// <summary>
        /// Является ли клик двойным
        /// </summary>
        public bool isDblClick;
        /// <summary>
        ///  Временная метка события
        /// </summary>
        public long microtime; 

         /// <summary>
        /// Метод, вызывающийся когда требуется повторить действие
        /// </summary>
      
        #endregion

        /// <summary>
        /// Тип метода- обработчика Повторения дейиствия
        /// </summary>
      
        #region events

        public static event RepeatHandler OnRepeat;
        public  event AfterRepeatHandler OnAfterRepeat;

        #endregion
        #region Properties

        public System.Windows.Forms.Message EventCode
        {
            get { return _eventcode; }

            set { _eventcode = value; }
        }
        public IntPtr ControlHandle
        {
            get { return pControlHandle; }
            set { pControlHandle = value; }
        }

        public string BriefInfo
        {
            get { return _strBriefInfo; }
            set { _strBriefInfo = value; }
        }

        /// <summary>
        /// Дескриптор потока, который прослушивается
        /// </summary>
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
        #endregion

        public UserAction(System.Windows.Forms.Message message, uint threadId)
        {
            this._eventcode = message;
            this.threadid = threadId;
        }

        public UserAction()
        {
            this._eventcode = new System.Windows.Forms.Message();
            this.threadid = 1;
        }

        public void Repeat(bool bEmulateInput)
        {
            if (OnRepeat != null)
                OnRepeat();
          
                cCursorHelper.SetCursorPos(this.MouseCoords.X, this.MouseCoords.Y);
             if (bEmulateInput)
             {
                if (!ActionHost.ThreadCaptured)
                    { 
                    
                        int nCurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId;
                        ActionHost.ThreadCaptured = cWindowsHelper.AttachThreadInput((uint)nCurrentThread, ThreadId, true); 

                    }

                cWindowsHelper.INPUT[] input = new cWindowsHelper.INPUT[1];
                input[0].type = (int) cWindowsHelper.INPUT_TYPE.MOUSE_INPUT;
                input[0].mi.dx = this.MouseCoords.X;
                input[0].mi.dy = this.MouseCoords.Y;
                int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(cWindowsHelper.INPUT));

                switch (this.EventCode.Msg)
                {
                    case cWindowsHelper.WM_LBUTTONDOWN:
                        {
                            input[0].mi.dwFlags = (int)cWindowsHelper.MOUSEEVENTF_LEFTDOWN | (int)cWindowsHelper.MOUSEEVENTF_ABSOLUTE;                                                                                
                        }
                        break;
                    case cWindowsHelper.WM_LBUTTONUP:
                        {
                            input[0].mi.dwFlags = (int)cWindowsHelper.MOUSEEVENTF_LEFTUP | (int)cWindowsHelper.MOUSEEVENTF_ABSOLUTE;
                        }
                        break;
                }
                cWindowsHelper.SendMouseInput(1, input, size);
                if (ActionHost.ThreadCaptured)
                {

                    int nCurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId;
                    //TODO ThreadId Заменить на ActionHost.ThreadId
                    ActionHost.ThreadCaptured = cWindowsHelper.AttachThreadInput((uint)nCurrentThread, ThreadId, false);

                }
             }
            
            if (OnAfterRepeat != null)
            {
                OnAfterRepeat();
            }
        }

        public  void DetachThread()
        {

            int nCurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId;
            ActionHost.ThreadCaptured = cWindowsHelper.AttachThreadInput((uint)nCurrentThread, ThreadId, false); 
        }

    }
}
