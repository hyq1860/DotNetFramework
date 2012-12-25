// -----------------------------------------------------------------------
// <copyright file="JobDemo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.TaskSchedulerConsoleDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Quartz;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [PersistJobDataAfterExecution]//保存执行状态  
    [DisallowConcurrentExecution]//不允许并发执行  
    public class JobDemo:IJob
    {
        private const string Count = "count";
        public void Execute(IJobExecutionContext context)
        {
            JobKey jobKey = context.JobDetail.Key;
            JobDataMap data = context.JobDetail.JobDataMap;
            int count;
            if (data.ContainsKey(Count))
            {
                count = data.GetInt(Count);
            }
            else
            {
                count = 0;
            }
            count++;
            //data.Put(Count, count);

            Console.WriteLine("JobDemo: {0} done at {1}\n Execution #{2}", jobKey, DateTime.Now.ToString("r"), count);
        }
    }
}
