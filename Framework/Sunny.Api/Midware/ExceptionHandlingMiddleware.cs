using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sunny.Common.Enum;
using Sunny.Common.Helper;
using System;
using System.Threading.Tasks;


namespace Sunny.Api.Midware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILoggerFactory loggerFactory;
        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {

            this.next = next;
            this.loggerFactory = loggerFactory;

        }

        public async Task Invoke(HttpContext context)
        {
            var statusCode = context.Response.StatusCode;

            try
            {
                await next(context);
            }
            catch (BizException ex)
            {
                await HandleBizExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            finally
            {
                if (statusCode != StatusCodes.Status200OK)
                {
                    await HandleErrorAsync(context, statusCode);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var logger = loggerFactory.CreateLogger(ex.TargetSite.DeclaringType);
            logger.LogError(ex, ex.Message);

            var data = new SpeciResult { code = Enums.OperationStatus.Exception.GetHashCode(), msg = "我们已经收到此次异常信息,将尽快解决!" };

            await ResponseInfo(context, data);
        }

        private async Task HandleErrorAsync(HttpContext context, int statusCode)
        {

            var msg = "未知错误";

            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    msg = "未授权";
                    break;
                case StatusCodes.Status404NotFound:
                    msg = "未找到服务";
                    break;
                case StatusCodes.Status502BadGateway:
                    msg = "请求错误";
                    break;
            }
            var data = new SpeciResult { code = Enums.OperationStatus.Fail.GetHashCode(), msg = msg };
            await ResponseInfo(context, data);
        }

        /// <summary>
        ///处理业务异常 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task HandleBizExceptionAsync(HttpContext context, BizException ex)
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
        private async Task ResponseInfo(HttpContext context, SpeciResult data)
        {
            var result = JsonHelper.ToJsonString(data);
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(result);

        }



    }

    /// <summary>
    /// 特殊的返回结果类,和IResult所定义的不同之处在于字段全是小写的,因为发生异常时,后边的中间件不会被调用,所以没法将大驼峰转小驼峰再返回给前端
    /// </summary>
    class SpeciResult
    {
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }

}
