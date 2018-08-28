using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class PrintA : IPrintService
    {
        DateTime now = DateTime.Now;
        public string Print()
        {
            
            return "Print A"+now;
        }
    }
}
