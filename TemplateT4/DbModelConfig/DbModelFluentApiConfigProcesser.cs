using Sunny.Common.Helper.File;
using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sunny.TemplateT4.DbModelConfig
{
    public class DbModelFluentApiConfigProcesser
    {
        /// <summary>
        /// 生成DbModel的FluentApi配置文件
        /// </summary>
        /// <param name="assemblyName">DbModel所在的程序集名称,如Sunny.Repository</param>
        /// <param name="outputFileNamespace">所生成的配置文件的命名空间,如Sunny.Repository.DbModel.MoelConfig</param>
        /// <param name="outputDir">生成文件的输出目录,默认是D:\SunnyFramework\Output\DbConfig\</param>
        static public  void GenerateFiles(string assemblyName,string outputFileNamespace,string outputDir = @"D:\SunnyFramework\Output\DbConfig\")
        {
            Assembly assembly = Assembly.Load(assemblyName);
            List<Type> ts = assembly.GetTypes().ToList();

            foreach (var item in ts.Where(s => !s.IsInterface && typeof(BaseModel).IsAssignableFrom(s) && !s.GetTypeInfo().IsAbstract && s.FullName != "Sunny.Repository.DbModel.BaseModel"))
            {
                var fullNameArr = item.FullName.Split(".");
                var filePath = $@"{fullNameArr[fullNameArr.Length - 2]}\{fullNameArr[fullNameArr.Length - 1]}Config.cs";

                string outputFile = outputDir + filePath;

                DbModelFluentApiConfig config = new DbModelFluentApiConfig(item, outputFileNamespace);

                FileHelper.WriteFile(outputFile, config.TransformText(), false);

            }
 
            Console.WriteLine("DbModalFluentApiConfig Generated!");
          
        }
    }
}
