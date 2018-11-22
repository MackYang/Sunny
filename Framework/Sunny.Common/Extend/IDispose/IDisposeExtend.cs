using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class IDisposeExtend
    {
        public static void DisposeIfNotNull(this IDisposable disObj)
        {
            if (disObj != null)
            {
                disObj.Dispose();
            }
        }
    }
}
