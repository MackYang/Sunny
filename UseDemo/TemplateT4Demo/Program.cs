using Sunny.TemplateT4.DbModelConfig;
using System;

namespace TemplateT4Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string assemblyName = "RepositoryDemo";

            string configNameSpace = "RepositoryDemo.DbModel.ModelConfig";


            DbModelFluentApiConfigProcesser.GenerateFiles(assemblyName,configNameSpace);




            Console.WriteLine("Hello World!");

            Console.Read();
        }
    }
}
