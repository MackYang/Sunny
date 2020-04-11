using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunny.Common.Helper
{
    public static class StringHelper
    {
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetExName(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf('.'));
        }

        /// <summary>
        /// 转为Base64字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBase64(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                byte[] bytes = Encoding.Default.GetBytes(input);
                return Convert.ToBase64String(bytes);

            }
            return input;
        }

        /// <summary>
        /// 从base64字符串转成普通字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FromBase64(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                byte[] outputb = Convert.FromBase64String(input);
                return Encoding.Default.GetString(outputb);
            }
            return input;
        }


        /// <summary>
        /// 获取AB两个字符串中最大的相同部分
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="diffCaps">是否区分大小写</param>
        /// <returns></returns>
        public static List<string> GetMaxSameString(string a, string b, bool diffCaps = true)
        {
            List<string> res = new List<string>();
            if (!string.IsNullOrWhiteSpace(a) && !string.IsNullOrWhiteSpace(b))
            {
                if (a.Length > b.Length)
                {
                    string temp = a; a = b; b = a;
                }
                List<string> list = new List<string>();
                for (int posA = 0; posA < a.Length; posA++)//将a字符串中的每一个字符作为首字符串进行对比
                {
                    int tempPosA = posA;
                    StringBuilder sb = new StringBuilder();

                    for (int posB = 0; posB < b.Length; posB++)
                    {
                        string tempA = a.Substring(tempPosA, 1);
                        if (tempPosA == posA)//如果是对比首字符串,那么就用indexof进行快速定位,定位到要开始对比字符串的位置,而不用从头开始逐个对比
                        {
                            if (diffCaps)
                            {
                                posB = b.IndexOf(tempA, posB);
                            }
                            else
                            {
                                posB = b.IndexOf(tempA, posB, StringComparison.CurrentCultureIgnoreCase);
                            }
                            if (posB == -1) { break; }//如果剩下未对比字符中没有找到以首字符开头的,就退出
                        }
                        string tempB = b.Substring(posB, 1);
                        bool eqRes = false;
                        if (diffCaps)
                        {
                            eqRes = tempA.Equals(tempB);
                        }
                        else
                        {
                            eqRes = tempA.ToLower().Equals(tempB.ToLower());
                        }
                        if (eqRes)//如果匹配,就接着对比下一字符
                        {
                            sb.Append(tempA);
                            if (tempPosA + 1 < a.Length)
                            {
                                ++tempPosA;
                            }

                        }
                        else//如果不匹配,则-1对比当前字符是否可作为下次对比的首字符
                        {
                            tempPosA = posA;

                            if (sb.Length > 0)
                            {
                                list.Add(sb.ToString());
                                sb.Clear();
                                --posB;
                            }

                        }
                    }
                }
                string maxStr = "";
                foreach (string item in list)//找最大长度的字符
                {
                    if (item.Length > maxStr.Length)
                    {
                        maxStr = item;

                    }
                }
                foreach (string item in list)//找与最大长度相等内容不等的字符
                {
                    if (item.Length == maxStr.Length)
                    {
                        if (diffCaps)
                        {
                            if (!item.Equals(maxStr))
                            {
                                res.Add(item);
                            }
                        }
                        else
                        {
                            if (!item.ToLower().Equals(maxStr.ToLower()))
                            {
                                res.Add(item);
                            }
                        }

                    }
                }
                res.Add(maxStr);
            }
            return res;
        }

        /// <summary>
        /// 根据长度参数，生成随机字符串
        /// </summary>
        /// <param name="codeLen">长度</param>
        /// <returns>返回随机字符串</returns>
        public static string CreateValidCode(int codeLen)
        {
            //下边这个是用于验证码的字符串。
            string codeSerial = "2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,S,T,U,V,W,X,Y,Z";

            string[] arr = codeSerial.Split(',');

            string code = "";

            int randValue = -1;

            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));

            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);

                code += arr[randValue];
            }

            return code;
        }

    }
}
