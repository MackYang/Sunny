using Microsoft.Extensions.Options;
using Sunny.Common.ConfigOption;
using Sunny.Common.Id;

namespace Sunny.Common.Helper
{
    public class IdHelper
    {
        private static Snowflake snowflake = null;
        private IdHelper()
        {

        }

        static IdHelper()
        {

        }

        /// <summary>
        /// 程序启动时调用一次就OK
        /// </summary>
        public static void InitSnowflake(SnowflakeOption options)
        {
            snowflake = new Snowflake(options.MachineId, options.DatacenterId);
        }

        /// <summary>
        /// 获取ID
        /// </summary>
        /// <returns></returns>
        public static long GenId()
        {
            return snowflake.NextId();

        }
    }


}
