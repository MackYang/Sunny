using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sunny.Common.DependencyInjection
{
    public class DIHelper
    {

        static public void AutoRegister(IServiceCollection services)
        {

            var deps = DependencyContext.Default;
            var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");//排除所有的系统程序集、Nuget下载包

            foreach (var item in libs)
            {
                DIHelper.RegisterByAssemblyName(services, item.Name);
            }
        }

        /// <summary>
        /// 注册指定程序集的DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        static public void RegisterByAssemblyName(IServiceCollection services, string assemblyName)
        {

            //集中注册服务

            var mapping = GetDIMapping(assemblyName);

            var scoped = mapping.Where(x => typeof(IScoped).IsAssignableFrom(x.Key));
            var transient = mapping.Where(x => typeof(ITransient).IsAssignableFrom(x.Key));
            var singleton = mapping.Where(x => typeof(ISingleton).IsAssignableFrom(x.Key));
 
            scoped.ToList().ForEach(x => { x.Value.ToList().ForEach(n => services.AddScoped(n, x.Key)); });
            transient.ToList().ForEach(x => { x.Value.ToList().ForEach(n => services.AddTransient(n, x.Key)); });
            singleton.ToList().ForEach(x => { x.Value.ToList().ForEach(n => services.AddSingleton(n, x.Key)); });

        }


        /// <summary>  
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
        static public Dictionary<Type, Type[]> GetDIMapping(string assemblyName)
        {
            var result = new Dictionary<Type, Type[]>();
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                foreach (var item in ts.Where(s => !s.IsInterface && typeof(IDependency).IsAssignableFrom(s) && !s.GetTypeInfo().IsAbstract))
                {
                    var interfaceType = item.GetInterfaces();
                    result.Add(item, interfaceType);
                }

            }
            return result;
        }
    }
}
