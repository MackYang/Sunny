using FluentValidation;
using Sunny.Common.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Api.FluentValidation
{
    /// <summary>
    /// 需要验证的实体请继承自这个类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Validator<T>: AbstractValidator<T>, ISingleton
    {
    }
}
