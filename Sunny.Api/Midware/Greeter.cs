using Sunny.Common.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Api.Midware
{
    public class Greeter : IGreeter 
    {
        public string Greet()
        {
            return "Hello from Greeter!";
        }
    }

    public interface IGreeter
    {
        string Greet();
    }
}
