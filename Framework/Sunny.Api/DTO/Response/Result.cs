using Sunny.Common.Enum;
using System;

namespace Sunny.Api.DTO.Response
{
    public interface IResult<in T> {

    }

    public class Result<T>:Result,IResult<T>
    {
        public new T Data { get; set; } = default(T);
    }

    /// <summary>
    /// 不带返回数据的,
    /// 但为了提供给前端的数据更规范,还是要返回Data字段,
    /// 如果要给前端返回数据时,请用泛型版本,这样看API时就知道返回的数据长什么样子
    /// </summary>
    public class Result
    {
        public int Code { get; set; } = Enums.OperationStatus.Success.GetHashCode();

        public string Msg { get; set; } = Enums.OperationStatus.Success.GetDescribe();

        public object Data { get;} 
    }
}
