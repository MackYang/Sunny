using Sunny.Common.Enum;
using System;

namespace Sunny.Api.DTO.Response
{
    /// <summary>
    /// 带返回数据的结果
    /// </summary>
    /// <typeparam name="T">返回的类型</typeparam>
    public class Result<T>
    {
        public int Code { get; set; } = Enums.OperationStatus.Success.GetHashCode();

        public string Msg { get; set; } = Enums.OperationStatus.Success.GetDescribe();
        public new T Data { get; set; } = default(T);

        public static implicit operator Result<T>(Result result)
        {
            return new Result<T>
            {
                Code = result.Code,
                Msg = result.Msg
            };
        }
    }

    /// <summary>
    /// 不带返回数据的Result
    /// 如果要给前端返回数据时,请用泛型版本,这样看API时就知道返回的数据长什么样子
    /// </summary>
    public class Result
    {
        public int Code { get; set; } = Enums.OperationStatus.Success.GetHashCode();

        public string Msg { get; set; } = Enums.OperationStatus.Success.GetDescribe();

    }
}
