using System;

namespace Sunny.Common.Helper
{
    public class CalcHelper
    {

        /// <summary>
        /// 计算增减率返回%比形式字符串,保留2位小数
        /// </summary>
        /// <param name="a">本期</param>
        /// <param name="b">同期</param>
        /// <returns></returns>
        public static string CalcDifferenceRate(decimal a, decimal b)
        {
            if (b == 0)
            {
                if (a == b)
                {
                    return "0.00%";
                }
                if (a > b)
                {
                    return "100.00%";
                }
                else
                {
                    return "-100.00%";
                }

            }

            return ((a - b) / b).ToString("0.##%");


        }


        /// <summary>
        /// 计算增减率返回转成百分比后的decimal
        /// </summary>
        /// <param name="a">本期</param>
        /// <param name="b">同期</param>
        /// <returns></returns>
        public static decimal CalcDifferenceRateReturnDecimal(decimal a, decimal b)
        {
            if (b == 0)
            {
                if (a == b)
                {
                    return 0;
                }
                if (a > b)
                {
                    return 100;
                }
                else
                {
                    return -100;
                }

            }

            return Math.Round(((a - b) / b) * 100, 2);


        }


        /// <summary>
        /// 除法计算保留几位小数
        /// </summary>
        /// <param name="a">被除数</param>
        /// <param name="b">除数</param>
        /// <returns></returns>
        public static decimal CalcDivision(decimal a, decimal b, int decimals = 2)
        {
            if (b == 0)
            {
                return 0.00M;
            }
            return Math.Round(a / b, decimals);

        }
    }
}
