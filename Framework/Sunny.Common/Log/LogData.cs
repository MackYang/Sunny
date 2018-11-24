using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Common.Log
{
    public class LogData
    {
        public string Message { get; set; }

        public string LevelString { get; set; }

        public Exception Exception { get; set; }
    }
}
