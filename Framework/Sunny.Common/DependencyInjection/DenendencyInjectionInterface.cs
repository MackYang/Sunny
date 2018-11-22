using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Common.DependencyInjection
{
    /// <summary>
    /// 用于标记需要批量注入的接口
    /// </summary>
    public interface IDependency
    {
    }
    /// <summary>
    /// 每次使用时都实例化一次
    /// </summary>
    public interface ITransient : IDependency { }

    /// <summary>
    /// 每个请求中只实例化一次
    /// </summary>
    public interface IScoped : IDependency { }

    /// <summary>
    /// 整个系统中只实例化一次
    /// </summary>
    public interface ISingleton : IDependency { }
}
