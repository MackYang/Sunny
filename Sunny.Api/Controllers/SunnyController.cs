using Microsoft.AspNetCore.Mvc;
using Sunny.Api.DTO.Response;
using Sunny.Api.Midware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Api.Controllers
{

    [ApiController]
    public class SunnyController : Controller
    {
        /// <summary>
        /// 返回一个动态类型的值,通常用于想返回扩展类型的场景,比如在源对象上增加一个枚举值描述的动态属性
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        protected  Result<dynamic> SuccessDynamic(dynamic responseData)
        {
            Result<dynamic> r = new Result<dynamic>();
            r.Data = responseData;
            
            return r;
        }


    }
}
