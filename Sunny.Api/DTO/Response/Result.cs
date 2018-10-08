using Sunny.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Api.DTO.Response
{
    public class Result<T>
    {
        public int Code { get; set; } = Enums.OperationStatus.Success.GetHashCode();

        public string Msg { get; set; } = Enums.OperationStatus.Success.GetDescribe();

        public T Data { get; set; } = default(T);
    }
}
