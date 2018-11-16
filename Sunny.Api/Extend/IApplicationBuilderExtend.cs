using Microsoft.AspNetCore.Builder;
using Sunny.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    static public class IApplicationBuilderExtend
    {
        /// <summary>
        /// 初始化DiHelper中的ServiceProvider
        /// </summary>
        /// <param name="builder"></param>
        static public void InitServiceProvider(this IApplicationBuilder builder)
        {
            DiHelper.ServiceProvider = builder.ApplicationServices;
        }
    }
}
