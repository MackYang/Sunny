using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sunny.Common.Enum
{
    public class Enums
    {
        #region 操作状态,表示本次操作成功失败或异常

        public enum OperationStatus
        {
            [Description("操作异常")]
            Exception = -1,


            [Description("操作成功")]
            Success = 0,


            [Description("操作失败")]
            Fail = 1
           
        }
        #endregion

        public enum RequestType
        {
            [Description("Get请求")]
            Get,
            [Description("Post请求")]
            Post
        }
    }
}
