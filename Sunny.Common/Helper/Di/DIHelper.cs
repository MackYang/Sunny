using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Sunny.Common.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sunny.Common.Helper
{
    public class DiHelper
    {

        public static IServiceProvider ServiceProvider { get; set; }


        /// <summary>
        /// 创建一个 DI 容器的 Scope
        /// </summary>
        /// <returns></returns>
        static public IServiceScope CreateScope()
        {
            var serviceScopeFactory = ServiceProvider.GetRequiredService<IServiceScopeFactory>();
            return serviceScopeFactory.CreateScope();
        }

        /// <summary>
        /// 获取一个实现了T接口的类对象,如果该类构造函数有其他接口参数,自动注入需要的接口实例
        /// </summary>
        /// <typeparam name="T">某接口的类型</typeparam>
        /// <returns>某接口的实例</returns>
        static public T GetService<T>()
        {
            T t = default;
            try
            {
                t = ServiceProvider.GetService<T>();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("from root provider"))
                {
                    t = CreateScope().ServiceProvider.GetService<T>();
                }
            }


            return t;
        }

        /// <summary>
        /// 从实现ISupportRequiredService接口的IServiceProvider获取实现了T接口的类对象,如果该类构造函数有其他接口参数,自动注入需要的接口实例
        /// </summary>
        /// <typeparam name="T">某接口的类型</typeparam>
        /// <returns>某接口的实例</returns>
        static public T GetRequiredService<T>()
        {
            T t = default;

            try
            {
                t = ServiceProvider.GetRequiredService<T>();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("from root provider"))
                {
                    t = CreateScope().ServiceProvider.GetRequiredService<T>();
                }
            }

            return t;
        }



        /// <summary>
        /// 创建一个某类的实例,会自动为该类的构造函数注入需要的接口实例
        /// </summary>
        /// <param name="arguments">构造函数参数</param>
        /// <returns>通过 DI 构造的实例</returns>
        static public T CreateInstance<T>(params object[] arguments)
        {
            T t = default;

            try
            {
                t = ActivatorUtilities.CreateInstance<T>(ServiceProvider, arguments);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("from root provider"))
                {
                    t = ActivatorUtilities.CreateInstance<T>(CreateScope().ServiceProvider, arguments);
                }

            }
            return t;
        }

        /// <summary>
        /// 创建一个类型实例,自动进行构造函数依赖注入
        /// </summary>
        /// <param name="instanceType">要创建的实力类型</param>
        /// <param name="arguments">DI 容器中未提供构造函数参数</param>
        /// <returns>通过 DI 构造的实例</returns>
        static public object CreateInstance(Type instanceType, params object[] arguments)
        {
            return ActivatorUtilities.CreateInstance(ServiceProvider, instanceType, arguments);
        }



        /// <summary>
        /// 自动为实现了ITransient,IScoped,ISingleton的类型注入实例
        /// </summary>
        /// <param name="services"></param>
        static public void AutoRegister(IServiceCollection services)
        {
            //指定控制台输出编辑为UTF8,不然中文会有乱码
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var deps = DependencyContext.Default;
            var libs = GetCustomizeCompilationLibraries();

            foreach (var item in libs)
            {
                DiHelper.RegisterByAssemblyName(services, item.Name);
            }
        }

        /// <summary>
        /// 获取自定义的程序集,排除所有的系统程序集、Nuget下载包的程序集
        /// </summary>
        /// <returns></returns>
        static public IEnumerable<CompilationLibrary> GetCustomizeCompilationLibraries()
        {
            var deps = DependencyContext.Default;
            return deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");
        }


        /// <summary>
        /// 获取自定义类型集合
        /// </summary>
        /// <param name="filter">过虑器</param>
        /// <returns>符合过滤条件的类型集合</returns>
        static public IEnumerable<Type> GetCustomizeTypes(Func<Type, bool> filter = null)
        {
            var libs = GetCustomizeCompilationLibraries();

            List<Type> types = new List<Type>();

            foreach (var item in libs)
            {
                Assembly assembly = Assembly.Load(item.Name);
                types.AddRange(assembly.GetTypes().ToList());
            }
            return filter == null ? types : types.Where(filter);

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
