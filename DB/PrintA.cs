using Sunny.Common.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    public class PrintA : IPrintService ,IScoped
    {
        DateTime now = DateTime.Now;
        public string Print()
        {
            
            return "Print A"+now;
        }
    }

    public class PrintB : ITransient, IB
    {
        public string Print()
        {
            return "this BBB";
        }
    }

    public class PrintC : ISingleton, IC
    {
        string IC.Print()
        {
            return "this CCC";
        }
    }
}
