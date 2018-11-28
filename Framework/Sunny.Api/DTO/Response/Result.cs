using Sunny.Common.Enum;
using System;

namespace Sunny.Api.DTO.Response
{

    /// <summary>
    /// 无返回数据的结果
    /// </summary>
    public interface IResult
    {

        /// <summary>
        /// 状态码
        /// </summary>
        int Code { get; set; }
        /// <summary>
        /// 提示消息
        /// </summary>
        string Msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        object Data { get; }
    }

    /// <summary>
    /// 有返回数据的结果
    /// </summary>
    /// <typeparam name="T">返回的数据类型</typeparam>
    public interface IResult<in T>:IResult
    { 

    }

    

    /// <summary>
    /// 带返回数据的结果
    /// </summary>
    /// <typeparam name="T">返回的类型</typeparam>
    public class Result<T> : Result, IResult<T>
    {
        public new T Data { get; set; } = default(T);
    }

    /// <summary>
    /// 不带返回数据的,
    /// 但为了提供给前端的数据更规范,还是要返回Data字段,
    /// 如果要给前端返回数据时,请用泛型版本,这样看API时就知道返回的数据长什么样子
    /// </summary>
    public class Result : IResult
    {
        public int Code { get; set; } = Enums.OperationStatus.Success.GetHashCode();

        public string Msg { get; set; } = Enums.OperationStatus.Success.GetDescribe();

        public object Data { get; }
    }
}
