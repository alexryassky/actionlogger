using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActionLogger
{
   
    class cTreeViewHelper
    {
        #region ComCtrl32 definitions
            /// <summary>
            /// TreeView first message code offset
            /// </summary>
            public const int TVN_FIRST = -400;
            /// <summary>
            /// TreeView last message code offset
            /// </summary>
            public const int TVN_LAST = -499;

            public const int TVN_ENDLABELEDIT = TVN_FIRST - 60;

            public const int TVN_BEGINLABELEDIT = TVN_FIRST - 59;
        #endregion
    }


}
