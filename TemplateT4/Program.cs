using Sunny.TemplateT4.DbModelConfig;
using System;
using TemplateT4.DbModelConfig;

namespace TemplateT4
{
    class Program
    {
        static void Main(string[] args)
        {
            DbModelFluentApiConfigProcesser.GenerateFiles();




            Console.WriteLine("Hello World!");

            Console.Read();
        }


    }
}
