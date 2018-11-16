using System.Collections.Generic;

namespace Sunny.Common.ConfigOption
{
    /// <summary>
    /// 任务的配置项
    /// </summary>
    public class JobOption
    {
        /// <summary>
        /// 任务的名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 在什么时候运行,用Cron表达式
        /// </summary>
        public string RunAtCron { get; set; }

        /// <summary>
        ///任务的参数
        /// </summary>
        public IDictionary<string, object> Args { get; set; }
    }
}
