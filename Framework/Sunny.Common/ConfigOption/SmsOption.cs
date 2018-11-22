using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Common.ConfigOption
{
    public class SmsOption
    {
        /// <summary>
        /// 短信网关的API地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// IP白名单列表,在列表中的IP发短信前不执行检查事件
        /// </summary>
        public string[] IPWhiteList { get; set; }


    }
}
