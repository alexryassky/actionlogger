using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ActionLogger
{
    class HookClass
    {
        private uint TargetThreadId;

        private uint TargetWindowHandle;

        #region WinAPI constants definitions
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

        delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        #endregion

        #region WinAPI function declarations
         [DllImport("user32.dll", SetLastError = true)]
            static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

         [DllImport("user32.dll", SetLastError = true)]
         static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

          [DllImport("user32.dll", SetLastError = true)]
         static extern bool UnhookWindowsHookEx(IntPtr hhk);

         [DllImport("kernel32.dll")]
         static extern IntPtr GetCurrentThread();


        #endregion

         public HookClass()
         {

         }
        
         public bool SetHook(bool Enabled)
         {

            IntPtr hhook = IntPtr.Zero;
            IntPtr hInstance= Process.GetCurrentProcess().Handle;
            if (Enabled)
            {
                hhook = SetWindowsHookEx(HookType.WH_MOUSE, WriteHook, hInstance, this.TargetThreadId);
                return true;
            }
            else
                if (hhook != IntPtr.Zero)
                    return UnhookWindowsHookEx(hhook);
                else
                    return false;
           
         }

        private int WriteHook(int code, IntPtr wparam, IntPtr lparam)
        {
            return 0;
          
        }

    }
}
