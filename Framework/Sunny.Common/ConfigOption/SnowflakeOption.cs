namespace Sunny.Common.ConfigOption
{
    public class SnowflakeOption
    {
       
        /// <summary>
        /// 数据中心ID,1到31之间
        /// </summary>
        public long DatacenterId { get; set; }

        /// <summary>
        /// 机器ID,1到31之间
        /// </summary>
        public long MachineId { get; set; }
    }
}
