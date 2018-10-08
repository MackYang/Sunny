using Sunny.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Api.DTO.Response
{
    public class Result
    {
        public int Code { get; set; } = Enums.OperationStatus.Success.GetHashCode();

        public string Msg { get; set; } = Enums.OperationStatus.Success.GetDescribe();

        public object Data { get; set; } 
    }
}
