// -----------------------------------------------------------------------
// <copyright file="TaskTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Quartz;
using Quartz.Impl;

namespace DotNet.TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TaskTest
    {
        public void Test1()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            //// construct job info
            //JobDetail jobDetail = new JobDetail("myJob", null, typeof(HelloJob));
            //// fire every hour
            //Trigger trigger = TriggerUtils.MakeHourlyTrigger();
            //// start on the next even hour
            //trigger.StartTimeUtc = TriggerUtils.GetEvenHourDate(DateTime.UtcNow);
            //trigger.Name = "myTrigger";
            //sched.ScheduleJob(jobDetail, trigger); 
        }
    }
}
