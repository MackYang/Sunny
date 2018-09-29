using Sunny.Common.Helper.File;
using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TemplateT4.DbModelConfig
{
    public class DbModelFluentApiConfigProcesser
    {
        //DbModel所在的程序集名称
        static string assemblyName = "Sunny.Repository";
        //生成文件的命名空间
        static string outputFileNamespace = "Sunny.Repository.DbModel.MoelConfig";
        //生成文件的输出目录
         static string outputDir = @"D:\SunnyFramework\Output\DbConfig\";



        static public  void GenerateFiles()
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
