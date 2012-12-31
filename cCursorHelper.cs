using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ActionLogger
{
    public static class cCursorHelper
    {

        #region Win32 definitions

        #endregion

        #region Win32 declarations

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        #endregion
    }
}
