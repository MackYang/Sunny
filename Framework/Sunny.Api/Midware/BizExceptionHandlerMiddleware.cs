using Microsoft.AspNetCore.Http;
using Sunny.Common.Enum;
using Sunny.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sunny.Api.Midware
{
  
    /// <summary>
    /// 业务异常中间件
    /// </summary>
    public class BizExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
       
        public BizExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BizException ex)
            {
                await HandleBizExceptionAsync(context, ex);
            }
            
        }

        /// <summary>
        ///处理业务异常 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public async Task HandleBizExceptionAsync(HttpContext context, BizException ex)
        {
            var data = new SpeciResult { code = Enums.OperationStatus.Fail.GetHashCode(), msg = ex.Message };
            await ResponseInfo(context, data);
        }

        /// <summary>
        /// 向前端输出信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static async Task ResponseInfo(HttpContext context, SpeciResult data)
        {
            var result = JsonHelper.ToJsonString(data);
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(result);

        }
    }

    /// <summary>
    /// 特殊的返回结果类,和IResult所定义的不同之处在于字段全是小写的,因为发生异常时,后边的中间件不会被调用,所以没法将大驼峰转小驼峰再返回给前端
    /// </summary>
    public class SpeciResult
    {
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
}
