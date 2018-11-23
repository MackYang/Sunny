using System.Collections.Generic;

namespace Sunny.Common.ConfigOption
{
    /// <summary>
    /// 任务的配置项
    /// </summary>
    public class JobOption
    {
        /// <summary>
        /// 任务类的名称
        /// </summary>
        public string JobClassName { get; set; }

        /// <summary>
        ///任务所属的组名称,同一组类不能有2个相同的任务 
        /// </summary>
        public string JobGroup { get; set; }

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
