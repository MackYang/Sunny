using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sunny.Api.DTO.Response;
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var logger = loggerFactory.CreateLogger(ex.TargetSite.DeclaringType);
            logger.LogError(ex, ex.Message);
            
            var data = new Result{ Code = Enums.OperationStatus.Exception.GetHashCode(),Msg = "我们已经收到此次异常信息,将尽快解决!" };
            var result = JsonHelper.ToJsonString(data);
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }

        private Task HandleErrorAsync(HttpContext context, int statusCode)
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

            var data = new Result  { Code = Enums.OperationStatus.Fail.GetHashCode(),Msg = msg };
            var result = JsonHelper.ToJsonString(data);
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }

}
