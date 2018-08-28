using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    public class PrintFB : IPrintService
    {
        DateTime now = DateTime.Now;
        public string Print()
        {
            
            return "Print B"+now;
        }
    }
}
