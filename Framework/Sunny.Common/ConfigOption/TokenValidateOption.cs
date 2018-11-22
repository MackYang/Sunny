using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Common.ConfigOption
{
    public class TokenValidateOption
    {
        /// <summary>
        /// 根据TokenKey的值从HttpHeader中获取对应的字符串,默认是"token"
        /// </summary>
        public string TokenKey { get; set; } = "token";

        /// <summary>
        /// 以此开头的API都需要验证Token
        /// </summary>
        public string AuthApiStartWith { get; set; }
    }
}
