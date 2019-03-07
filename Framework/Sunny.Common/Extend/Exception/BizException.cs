using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    /// <summary>
    /// 用于表示业务异常的类
    /// </summary>
    public class BizException : Exception
    {
        public BizException(string message) : base(message)
        {
        }

        public BizException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BizException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
