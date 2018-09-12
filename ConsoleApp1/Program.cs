
 
using System;

namespace ConsoleApp1
{
    class Program
    {
        static IPrintService service;
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterType<PrintB>().As<IPrintService>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IPrintService>();
                Console.WriteLine(writer.Print());
            }
            Console.WriteLine("Hello World!");

           
            Console.Read();
        }
    }
}
