using Quartz;
using ServiceDemo;
using Sunny.Api.Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemo.Job
{
    public class JobB : IJobEntity
    {

        public string JobName => "this job B Name";

        public string Describe => "this job B Describe";

        IStudentServic studentServic;

        public JobB(IStudentServic studentServic)
        {
            this.studentServic = studentServic;

        }


        public async Task ExecuteAsync(IJobExecutionContext jobContext)
        {
            Console.WriteLine(jobContext.JobDetail.JobDataMap["pxxx"]);//使用了配置中传来的参数,参数的名称要和配置里的一样
            Console.WriteLine( studentServic.GetStudent().StudentName);

        }

    }
}
