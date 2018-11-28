using Sunny.Api.DTO.Response;
using Sunny.Common.Enum;

namespace Sunny.Api.Controllers
{
    static public class SunnyControllerExtend
    {

        #region 封装操作结果
        public static Result<T> GetResult<T>(this SunnyController controller, T responseData, int code, string msg)
        {
            return new Result<T> { Code = code, Msg = msg , Data = responseData};
        }

        public static Result<T> Success<T>(this SunnyController controller, T responseData) 
        {
            Result<T> r = new Result<T>();
            r.Data = responseData;
            return r;
        }

        
        public static Result Success(this SunnyController controller)
        {
            return new Result();
        }

        public static Result<object> Fail(this SunnyController controller, string msg)
        {
            return new Result<object> { Code = Enums.OperationStatus.Fail.GetHashCode(), Msg = msg };
        }

        #endregion
    }
}
