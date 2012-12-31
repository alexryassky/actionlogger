using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ActionLogger
{

    
    public delegate bool CallBack(IntPtr handle, IntPtr param);

    /// <summary>
    /// Класс для работы с перечислением окон через стандартный API.
    /// </summary>
    class WindowsEnumerator
    {

        private bool NotInList(string text, List<string> stringList)
        {
            return !stringList.Contains(text);
        }

        // Дескриптор родительского окна, для которого мы перечисляем найденные окна
        public IntPtr ParentHandle =  IntPtr.Zero;

        // Найденный дескриптор
        public static IntPtr FindedHandle = IntPtr.Zero;
        public delegate bool EnumWindowsProc(IntPtr hWnd,   IntPtr lParam);

        // Список окон, которые мы не хотим находить
        private static List<string> BlackList = new List<string>();



        #region WinAPI Definitions
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;       
            public int Top;         
            public int Right;      
            public int Bottom;      
        }

        [StructLayout (LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }


        #endregion

        #region WinAPI Declarations
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowsProc callback, IntPtr lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PtInRect([In] ref RECT lprc, POINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd,
           StringBuilder lpClassName,
           int nMaxCount
        );


        //Compute the client coordinates
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        #endregion
      
 
        public WindowsEnumerator()
        {
            BlackList.Add("Shell_TrayWnd");
            BlackList.Add("tooltips_class32");
            BlackList.Add("#32768");
            BlackList.Add("#32769");
            BlackList.Add("#32770");
            BlackList.Add("#32771");
            BlackList.Add("#32772");
        }

        /// <summary>
        /// Функция находит дочернее окно в данной точке.
        /// </summary>
        /// <param name="pt">Точка в абсолютных координатах</param>
        /// <returns>Возвращает дескриптор окна</returns>
        public IntPtr FindWindowAtPos(System.Drawing.Point pt)
        {
          FindedHandle = IntPtr.Zero;
          POINT APIpt = new POINT();
          APIpt.X = pt.X;
          APIpt.Y = pt.Y;
          GCHandle GCPoint = GCHandle.Alloc(APIpt); 
          EnumWindowsProc cbFinder = new EnumWindowsProc(FindNextLevelWindowAtPos);
          EnumChildWindows(ParentHandle,FindNextLevelWindowAtPos, GCHandle.ToIntPtr(GCPoint));     
          return FindedHandle;
        }

        /// <summary>
        /// Каллбэк функция для EnumChildWindows
        /// </summary>
        /// <param name="hwnd">Дескриптор дочернего окна</param>
        /// <param name="Parameter">Указатель в куче процесса на точку</param>
        /// <returns></returns>
        private static bool FindNextLevelWindowAtPos(IntPtr hwnd, IntPtr Parameter)
        {
            GCHandle GCPoint = GCHandle.FromIntPtr(Parameter);
            POINT pt = (POINT)GCPoint.Target;// Разыменовать указатель
            RECT childRct;
            
            
          //  IntPtr hResult = cWindowsHelper.WindowFromPoint(pt2);
          //  bool result = (hResult !=IntPtr.Zero);
            GetWindowRect(hwnd,out childRct);         
            bool result = PtInRect(ref childRct,pt);
            cWindowsHelper.WINDOWPLACEMENT placement = new cWindowsHelper.WINDOWPLACEMENT();
            cWindowsHelper.GetWindowPlacement(hwnd, ref placement);
            result &= (placement.ShowCmd != cWindowsHelper.ShowWindowCommands.Hide);
            if (result){

                FindedHandle = hwnd;
               if (!BlackList.Contains(GetWindowClassName(FindedHandle)))
                    return false;
               else
                 return true;
            }
            else
            {
                return true;
            }
     
        }


        private static string GetWindowClassName(IntPtr hWnd)
        {
            StringBuilder buffer = new StringBuilder(128);

            GetClassName(hWnd, buffer, buffer.Capacity);

            return buffer.ToString();
        }

        private static bool Enumerator()
        {
            return true;
        }
        
    }
}
