using Sunny.TemplateT4.FluentApiConfig;
using System;

namespace TemplateT4Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string assemblyName = "RepositoryDemo";
            string configNameSpace = "RepositoryDemo.DbModel.ModelConfig";

            FluentApiConfigProcesser.GenerateFiles(assemblyName,configNameSpace);
            
            Console.Read();
        }
    }
}
