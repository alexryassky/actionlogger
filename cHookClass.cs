using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
/*using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;*/
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;



namespace ActionLogger
{

    /// <summary>
    /// Класс для перехвата событий клавиатуры и мыши
    /// </summary>
    public class HookClass
    {

        private bool _installed;


        #region API data definitions

        [StructLayout(LayoutKind.Sequential)]
        public class KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public KBDLLHOOKSTRUCTFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [Flags]
        public enum KBDLLHOOKSTRUCTFlags : uint
        {
            LLKHF_EXTENDED = 0x01,
            LLKHF_INJECTED = 0x10,
            LLKHF_ALTDOWN = 0x20,
            LLKHF_UP = 0x80,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData; // be careful, this must be ints, not uints (was wrong before I changed it...). regards, cmew.
            public int flags;
            public int time;
            public UIntPtr dwExtraInfo;
        }

        public enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEHOOKSTRUCT
        {
            public POINT pt;
            public IntPtr hwnd;
            public uint wHitTestCode;
            public IntPtr dwExtraInfo;
        }

        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
        public int X;
        public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public UInt32 message;
            public IntPtr wParam;
            public IntPtr lParam;
            public UInt32 time;
            public POINT pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int _Left;
            public int _Top;
            public int _Right;
            public int _Bottom;
        }

        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;
      
        #endregion

        #region  API declarations
        
        delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        // Windows hook routines
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

      
        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll",CallingConvention = CallingConvention.StdCall)]
        static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam,
           IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true,CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        //Finding window in mouse position
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
 
        //Get text of the current Window
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int  wParam, StringBuilder lParam);

       

        

        #endregion

        private IntPtr HHook;
        private IntPtr HHookM;
        private static HookProc KBCallbackDelegate = null;
        private static HookProc MouseCallbackDelegate = null;
        private static WindowsEnumerator weFinder = null;
        private uint ThreadId;

        public uint ProcessId {
            get { 
                return ThreadId; 
            } 
            set {
                ThreadId = value;
            }
        }

        public bool Installed
        {
            get { return _installed; }
            set { _installed = value; }
        }

        /// <summary>
        /// Получение дескриптора текущего процесса
        /// </summary>
        /// <param name="Handle"></param>
        /// <returns></returns>
        public uint GetProcess(IntPtr Handle)
        {
            uint wndProcessId = 0;
            uint threadId = 0; ;
            threadId = GetWindowThreadProcessId(Handle, out wndProcessId);
            ThreadId = threadId;
            
            Process pProcess = Process.GetProcessById((int)wndProcessId);
            ProcessModule pmModuleMain = pProcess.MainModule;
            IntPtr hModule = GetModuleHandle(pmModuleMain.ModuleName);
            StringBuilder exeName = new StringBuilder(1024);
            cWindowsHelper.GetModuleFileNameEx(pProcess.Handle, hModule, exeName, 1024);
            ActionHost.strTargetExeName = exeName.ToString();
            return threadId;         
        }


        


        /// <summary>
        ///  Каллбэк функция для клавиатурных событий. Пока отключена
        /// </summary>
        /// <param name="code">Флаг передачи управления следующему хуку</param>
        /// <param name="wParam"> Содержит доп. информацию о сообщении </param>
        /// <param name="lparam"> Содержит информацию о нажатых клавишах</param>
        /// <returns></returns>
        private int KBCallbackFunction(int code, IntPtr wParam, IntPtr lparam)
        {
           
          
            /*MSG msgstruct = (MSG) Marshal.PtrToStructure(wParam, typeof(MSG));
            System.Windows.Forms.Message Message_ = new System.Windows.Forms.Message();
            Message_.HWnd = msgstruct.hwnd;
            Message_.LParam = msgstruct.lParam;*/
            /*Message_.WParam = msgstruct.wParam;*/

            
            ActionLogger.UserAction uaKbd = new ActionLogger.UserAction();
            
            KBDLLHOOKSTRUCT kbinfo = (KBDLLHOOKSTRUCT) Marshal.PtrToStructure(lparam,typeof(KBDLLHOOKSTRUCT));
            uaKbd.ThreadId = this.ThreadId;
            System.Windows.Forms.Message msg = new System.Windows.Forms.Message();
            msg.Msg =  wParam.ToInt32();
            msg.HWnd = IntPtr.Zero;
            msg.WParam = wParam;
            msg.LParam = lparam;
            
            uaKbd.EventCode = msg;
            uaKbd.ControlHandle = IntPtr.Zero;
            uaKbd.KeyCode= (int)kbinfo.vkCode;
            ActionHost.AddItem(uaKbd);

           /* BinaryFormatter bfSerializer = new BinaryFormatter();
            NamedPipeServerStream pipeStream = new NamedPipeServerStream("hookpipe");
            bfSerializer.Serialize(pipeStream,uaKbd);
            pipeStream.Close();*/
        
        
           return CallNextHookEx(IntPtr.Zero, code, wParam, lparam);
            
        }


        /// <summary>
        /// Callback функция для хука -- определение окна под мышью. 
        /// </summary>
        private int MouseCallBackFunctionFinder(int code, IntPtr wparam, IntPtr LParam)
        {
            MSLLHOOKSTRUCT mouseInfo = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(LParam, typeof(MSLLHOOKSTRUCT));
            System.Windows.Forms.Message msg = new System.Windows.Forms.Message();
            msg.Msg = wparam.ToInt32();

            if (msg.Msg == WM_LBUTTONDOWN)
            {
                //Записываем координаты мыши в DWORD
                uint MousePt = unchecked((uint)((uint)mouseInfo.pt.Y << 16) | (uint)mouseInfo.pt.X);
                System.Drawing.Point pt = new System.Drawing.Point(mouseInfo.pt.X, mouseInfo.pt.Y);
                // Находим ближайшее окно
                IntPtr hwnd = weFinder.FindWindowAtPos(pt);
                ActionHost.TargetWindow = hwnd;
                RECT rctWindowRect = new RECT();
                GetWindowRect(hwnd, out rctWindowRect);
                ActionHost.ptWindowPoint = new Point(rctWindowRect._Left, rctWindowRect._Top);
                ActionHost.szTargetBounds.Width = rctWindowRect._Right - rctWindowRect._Left;
                ActionHost.szTargetBounds.Height = rctWindowRect._Bottom - rctWindowRect._Top;
               
              
            }
            return CallNextHookEx(IntPtr.Zero, code, wparam, LParam);
        }
        /// <summary>
        /// Callback функция для хука -- прослушивание событий мыши. 
        /// </summary> 
        private int MouseCallbackFunctionListener(int code, IntPtr wParam, IntPtr lparam)
        {
            
            MSLLHOOKSTRUCT mouseInfo = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lparam, typeof(MSLLHOOKSTRUCT));
            // Инициализируем новый итем
            ActionLogger.UserAction uaMouse = new ActionLogger.UserAction();
            System.Windows.Forms.Message msg = new System.Windows.Forms.Message();
            msg.Msg = wParam.ToInt32();
          
            if (msg.Msg == WM_LBUTTONDOWN || msg.Msg == WM_LBUTTONUP)
            {
                //Записываем координаты мыши в DWORD
                uint MousePt = unchecked( (uint)((uint)mouseInfo.pt.Y << 16) | (uint)mouseInfo.pt.X);
                System.Drawing.Point pt = new System.Drawing.Point(mouseInfo.pt.X, mouseInfo.pt.Y);
           
                //Рекурсивно находим самое нижнее из всех дочерних окон, в котором находится наша точка
                IntPtr OldWnd = weFinder.ParentHandle;
                IntPtr OldCWnd = IntPtr.Zero;
                IntPtr hwnd = weFinder.FindWindowAtPos(pt);               
                while (hwnd != IntPtr.Zero)
                {
                    OldCWnd = hwnd;
                    weFinder.ParentHandle = hwnd;
                    hwnd = weFinder.FindWindowAtPos(pt);
                      
                }
                weFinder.ParentHandle = OldWnd;
                hwnd = OldCWnd;
                if (hwnd != IntPtr.Zero)
                {
                    //Получаем текст окна
                    int len = SendMessage(hwnd, WM_GETTEXTLENGTH, 0, new StringBuilder()).ToInt32()+1;
                    StringBuilder strWindowText = new StringBuilder(len);
                    SendMessage(hwnd, WM_GETTEXT, len , strWindowText);
      
                    uaMouse.BriefInfo = strWindowText.ToString() + ":";

                    // Получаем класс окна              
                    StringBuilder strWindowClass = new StringBuilder(128);
                    WindowsEnumerator.GetClassName(hwnd,strWindowClass,128);
                    
                    uaMouse.BriefInfo  += strWindowClass.ToString();

                    //Формирует ординарное сообщение Windows для помещения в хранилище
                    IntPtr PtrToPt = new IntPtr(MousePt);
                    msg.LParam = PtrToPt;
                    msg.WParam = new IntPtr(1);
               
                    uaMouse.KeyCode = (mouseInfo.mouseData >> 16);                    
                    uaMouse.EventCode = msg;
                    uaMouse.ThreadId = this.ThreadId;
                    //Записываем новый итем в модель.
                    ActionHost.AddItem(uaMouse);
                }

            }
            // Передаем управление следующему хуку, если такой есть в системе.
            return CallNextHookEx(IntPtr.Zero, code, wParam, lparam);
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="HandleTo">Дескриптор окна, к которому хотим подключиться</param>
        public HookClass(IntPtr HandleTo)
        {
            weFinder = new WindowsEnumerator();
            weFinder.ParentHandle = HandleTo;
            if (HandleTo != IntPtr.Zero)
                this.ThreadId = GetProcess(HandleTo);
            KBCallbackDelegate = KBCallbackFunction;
           
        }

        /// <summary>
        /// Делегат, который будет обрабатывать перехваченное сообщение из системной очереди, 
        /// выполняет определение окна
        /// </summary>
        public void SetPickHook()
        {
            MouseCallbackDelegate = MouseCallBackFunctionFinder;
        }

        /// <summary>
        /// Делегат, который будет обрабатывать перехваченное сообщение из системной очереди, 
        /// выполняет прослушивание событий мыши
        /// </summary>
        public void SetListenHook()
        {
            MouseCallbackDelegate = MouseCallbackFunctionListener;
        }


        /// <summary>
        /// Метод, устанавливающий системный хук на очередь сообщений Windows. Так как .NET не позволяет создать DLL
        /// , которая могла бы расшарить адресное пространство с другим процессом, то ловим общесистемные события.
        /// </summary>
        /// <param name="install">True - устанавливаем хук, False - снимаем</param>
        public void SetHook(bool install)
        {
            if (install)
            {
           
                Process pProcess = Process.GetCurrentProcess();
                ProcessModule pmModuleMain = pProcess.MainModule;
                IntPtr hModule = GetModuleHandle(pmModuleMain.ModuleName);
           
                //  Клавиатурный хук пока отключаем. HHook = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, KBCallbackDelegate,  hModule, 0);
                HHookM = SetWindowsHookEx(HookType.WH_MOUSE_LL, MouseCallbackDelegate, hModule, 0);
                if (/*HHook != IntPtr.Zero &&*/ HHookM != IntPtr.Zero) // Клавиатурный хук пока отключаем.
                    this._installed = true;
                else
                {
                    int errorcode = Marshal.GetLastWin32Error();
                    System.Windows.Forms.MessageBox.Show("Код ошибки " + errorcode.ToString());
                }

            }
            else
            {
               // Клавиатурный хук пока отключаем. UnhookWindowsHookEx(HHook);
                if (HHookM!=IntPtr.Zero)
                    UnhookWindowsHookEx(HHookM);
                this._installed = false;
            }
        }



    }
}
