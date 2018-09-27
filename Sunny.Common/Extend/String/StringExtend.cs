using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtend
    {
        public static string UpperCharToUnderLine(this string param)
        {
            if (string.IsNullOrWhiteSpace(param)) { return param; }
            Regex p = new Regex("[A-Z]");
            var tmp = p.Replace(param, new MatchEvaluator(x => x.ToString().Replace(x.ToString(), "_" + x.ToString().ToLower())));
            if (tmp.StartsWith("_"))
            {
                tmp = tmp.Substring(1);
            }
            return tmp;

        }



    }
}
