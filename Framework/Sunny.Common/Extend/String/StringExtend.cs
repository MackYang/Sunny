using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtend
    {
        private static readonly Regex emailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex webUrlExpression = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex phoneNumExpression = new Regex(@"^(13[0-9]|15[012356789]|17[0678]|18[0-9]|14[57])[0-9]{8}$", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

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

     
        public static bool isInt(this string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, "^(\\d*)$");
        }

        public static bool IsPhoneNum(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) && phoneNumExpression.IsMatch(input);
        }

        public static bool IsEmail(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) && emailExpression.IsMatch(input);
        }

        public static bool IsWebUrl(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) && webUrlExpression.IsMatch(input);
        }

        public static bool IsIPAddress(this string input)
        {
            IPAddress ip;
            return !string.IsNullOrWhiteSpace(input) && IPAddress.TryParse(input, out ip);
        }

        /// <summary>
        /// 检查是否是数字，包括整数和小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumber(this string input)
        {
            Regex r = new Regex(@"^[-]?\d+[.]?\d*$");
            return (!string.IsNullOrWhiteSpace(input) && r.IsMatch(input));
        }

        /// <summary>
        /// 检查是否是大于0的数字，包括整数和小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumberMoreThanZero(this string input)
        {
            Regex r = new Regex(@"^\d+[.]?\d*$");
            return (!string.IsNullOrWhiteSpace(input) && r.IsMatch(input));
        }

        /// <summary>
        /// 让字符串中的多个空格合并成一个,并移除首尾的空格字符,将中文空格换成英文的
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SingleSpace(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }
            input = input.Trim();
            input = input.Replace("　", " ");//将中文空格换成英文的
            return Regex.Replace(input, @" +", " ");
        }


        #region 防止注入
        /// <summary>
        /// 验证数据，防止注入
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NOSQL(this string input)
        {
            string str = "";
            if (!string.IsNullOrWhiteSpace(input))
            {
                str = input.ToLower();
                str = str.Replace("select", "");
                str = str.Replace("insert", "");
                str = str.Replace("update", "");
                str = str.Replace("delete from", "");
                str = str.Replace("truncate", "");
                str = str.Replace("drop", "");
                str = str.Replace("create", "");
                str = str.Replace("exec", "");
                str = str.Replace(" or ", "");
                str = str.Replace(" in", "");
                str = str.Replace(" and ", "");
                str = str.Replace("exec", "");
                str = str.Replace("execute", "");
                str = str.Replace("where", "");
                str = str.Replace("go", "");
                str = str.Replace("declare", "");
                str = str.Replace("commit", "");
                str = str.Replace("rollback", "");
                str = str.Replace("transaction", "");
                str = str.Replace("immediate", "");
                str = str.Replace("net localgroup administrators", "");
                str = str.Replace("net user", "");
                str = str.Replace("copy", "");
                str = str.Replace("count(", "");
                str = str.Replace("asc(", "");
                str = str.Replace("mid(", "");
                str = str.Replace("char(", "");
                str = str.Replace("xp_cmdshell", "");
                //str = str.Replace("'", "");拼in字符串的时候不能没有'
            }
            else
            {
                input = "";
            }
            if (input.Length == str.Length)
            {
                return input;
            }
            else
            {
                return str;
            }

        }


        #endregion

        #region 截取字符串
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="inputString">输入字符串</param>
        /// <param name="maxLength">最大长度(按半角计算, 全角按半角的2倍计算)</param>
        /// <param name="appendEllipsis">是否需要添加省略号</param>
        /// <returns>截取后的字符串</returns>
        public static string BxSubstring(this string inputString, int maxLength, bool appendEllipsis)
        {
            //bool flag = false; //执行状态
            string strResult = string.Empty;
            StringBuilder builder = new StringBuilder();
            int count = 0;
            int byteCount = 0;
            string tmpString = inputString;
            if (!string.IsNullOrWhiteSpace(inputString))
            {
                inputString = inputString.Trim();
                if (inputString.Length > maxLength)
                {
                    tmpString = inputString.Substring(0, maxLength);
                }

                char[] charArray = tmpString.ToCharArray();

                foreach (char chr in charArray)
                {
                    byteCount = Encoding.Default.GetByteCount(chr.ToString());
                    switch (byteCount)
                    {
                        case 1:
                            {
                                builder.Append(chr.ToString());
                                count += byteCount;
                            }
                            break;
                        case 2:
                            {
                                if (count + byteCount <= maxLength)
                                {
                                    builder.Append(chr.ToString());
                                }
                                count += byteCount;
                            }
                            break;
                    }

                    if (count > maxLength)
                        break;
                }
            }

            if (appendEllipsis && inputString.Length != builder.Length)
            {
                strResult = builder.ToString() + "...";
            }
            else
            {
                strResult = builder.ToString();
            }

            return strResult;
            #region 参考代码
            /*
             其中string.length表示字符串的字符数， 
            System.text.Encoding.Default.GetByteCount表示字符串的字节数。 
            判断半角如下： 
            if (checkString.Length == Encoding.Default.GetByteCount(checkString))
               {
                return true;
               }
               else
               {
                return false;
               } 
            全角如下： 
            if (2 * checkString.Length ==  Encoding.Default.GetByteCount(checkString))
               {
                return true;
               }
               else
               {
                return false;
               } 
 
             */
            #endregion 参考代码
        }
        #endregion

        /// <summary>
        /// 遮罩中间部分字符串
        /// </summary>
        /// <param name="x"></param>
        /// <param name="maskRate">遮罩比例,0.6表示60%</param>
        /// <returns></returns>
        static public string MaskString(this string x, decimal maskRate = 0.6M)
        {
            string res = "";
            if (!string.IsNullOrWhiteSpace(x))
            {
                int hideLen = (int)Math.Round(x.Length * maskRate, 0);
                if (hideLen == x.Length)
                {
                    for (int i = 0; i < x.Length; i++)
                    {
                        res += "*";
                    }
                }
                else
                {
                    res += x.Substring(0, 1);
                    for (int i = 0; i < hideLen; i++)
                    {
                        res += "*";
                    }
                    res += x.Substring(hideLen);
                }
            }
            return res;


        }


        /// <summary>
        /// 获取一串中文字的首字母组合
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetChineseSpell(this string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += GetSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        //用来获得一个字的拼音首字母
        private static string GetSpell(string cnChar)
        {
            //将汉字转化为ASNI码,二进制序列
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217,45253,45761,46318,46826,47010,
                                    47297,47614,48119,48119,49062,49324,
                                    49896,50371,50614,50622,50906,51387,
                                    51446,52218,52698,52698,52698,52980,
                                    53689,54481
                                    };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "其他"; // return "*";
            }
            else
                return cnChar;
        }

        //清除HTML函数 
        public static string NoHTML(this string Htmlstring)
        {

            //删除脚本 

            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            //删除HTML 

            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");

            Htmlstring.Replace(">", "");

            Htmlstring.Replace("\r\n", "");

            Htmlstring.Replace("&", "");


            return Htmlstring;

        }
        #region 包含与被包含

        /// <summary>
        /// 判断当前字符串中是否包含数组中的任意元素
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <param name="strArr">字符串数组</param>
        /// <returns>true包含</returns>
        public static bool LikeElement(this string str, string[] strArr)
        {
            return !string.IsNullOrWhiteSpace(GetLikeFirstElement(str, strArr));
        }

        /// <summary>
        /// 如果当前字符串中包含数组中的任意元素,则返回第一个符合条件的元素
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <param name="strArr"></param>
        /// <returns>返回第一个符合条件的元素,没有则返回null</returns>
        public static string GetLikeFirstElement(this string str, string[] strArr)
        {
            if (strArr.Length > 0)
            {
                foreach (string s in strArr)
                {
                    if (str.ContainsUL(s))
                    {
                        return s;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 是否包含某字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="data">某字符串</param>
        /// <param name="caseUL">是否大小写敏感，默认否</param>
        /// <returns>包含返回true</returns>
        public static bool ContainsUL(this string s, string data, bool caseUL = false)
        {
            int idx = 0;
            if (caseUL)
            {
                idx = s.IndexOf(data);
            }
            else
            {
                idx = s.IndexOf(data, StringComparison.OrdinalIgnoreCase);
            }
            return idx >= 0 ? true : false;
        }

        /// <summary>
        /// 当前字符串数组中的任何元素是否与指定数组中的任一元素相等
        /// </summary>
        /// <param name="s">当前字符串数组</param>
        /// <param name="arr">指定数组</param>
        /// <param name="caseUL">是否区分大小写,默认不区分</param>
        /// <returns></returns>
        public static bool ContainsElement(this string[] s, string[] arr, bool caseUL = false)
        {
            bool flag = false;
            if (s != null && arr != null)
            {
                if (caseUL)
                {
                    foreach (string item in arr)
                    {
                        if (s.Any(x => x.Equals(item)))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (string item in arr)
                    {
                        if (s.Any(x => x.ToLower().Equals(item.ToLower())))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }

            return flag;

        }

        /// <summary>
        /// 截取字符串的指定部分，索引超出时返回原字符串,不报异常
        /// </summary>
        /// <param name="input"></param>
        /// <param name="startIndex">开始位置</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string AutoSubstring(this string input, int startIndex, int length)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                if ((startIndex + length) <= input.Length)
                {
                    return input.Substring(startIndex, length);
                }
            }

            return input;
        }




        #endregion
        /// <summary>
        /// 转义like参数中的通配符
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string SqlLikeArgsProcess(this string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                return s.Replace("_", "\\_").Replace("%", "\\%").Replace("[", "\\[").Replace("^", "\\^").Replace("]", "\\]");
            }
            return null;
        }


        /// <summary>
        /// 获取in条件格式的字符串,用','连接的
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static public string GetInString(this string[] strArr)
        {
            return strArr.ToList().GetInString();
        }

        /// <summary>
        /// 获取in条件格式的字符串,用','连接的
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static public string GetInString(this List<string> list)
        {
            if (list != null && list.Count > 0)
            {
                string str = "'";
                list.ForEach(x => str = str + x + "','");
                str = str.Substring(0, str.Length - 2);
                return str.NOSQL();
            }
            return "";
        }


    }
}
