using Quartz;
using Sunny.Common.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Api.Quartz
{
    /// <summary>
    /// 任务信息
    /// </summary>
    public interface IJobEntity:ITransient
    {
        /// <summary>
        /// 任务的ID
        /// </summary>
        string JobId { get;  }
        /// <summary>
        /// 任务的名称
        /// </summary>
        string JobName { get; }
        /// <summary>
        /// 任务的描述
        /// </summary>
        string Describe { get; }

        /// <summary>
        /// 任务内容
        /// </summary>
        Task ExecuteAsync();
    }
}
