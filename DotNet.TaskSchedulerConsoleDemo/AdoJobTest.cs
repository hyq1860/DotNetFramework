// -----------------------------------------------------------------------
// <copyright file="AdoJobTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DotNet.TaskSchedulerConsoleDemo
{
    using System.Collections.Specialized;
    using System.Threading;

    using Common.Logging;

    using Quartz;
    using Quartz.Impl;

    /// <summary>
    ///http://www.cnblogs.com/wuyansheng/archive/2012/03/10/2389584.html
    /// </summary>
    public class AdoJobTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AdoJobTest));

        public virtual void Run(bool inClearJobs, bool inScheduleJobs)
        {
            var properties = new NameValueCollection();

            properties["quartz.scheduler.instanceName"] = "测试任务";
            properties["quartz.scheduler.instanceId"] = "instance_one";
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "2";
            properties["quartz.threadPool.threadPriority"] = "Normal";
            properties["quartz.jobStore.misfireThreshold"] = "60000";
            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";//AdoJobStore
            properties["quartz.jobStore.useProperties"] = "false";
            properties["quartz.jobStore.dataSource"] = "default";
            properties["quartz.jobStore.tablePrefix"] = "QRTZ_";//指定所使用的数据库表前缀
            properties["quartz.jobStore.clustered"] = "true";
            // if running SQLite we need this
            properties["quartz.jobStore.lockHandler.type"] = "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz";

            //properties["quartz.dataSource.default.connectionString"] = "Server=(local);Database=quartz;Trusted_Connection=True;";
            properties["quartz.dataSource.default.connectionString"] = @"Data Source=" + Environment.CurrentDirectory + "\\task.db";
            properties["quartz.dataSource.default.provider"] = "SQLite-10";

            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory(properties);
            IScheduler sched = sf.GetScheduler();

            if (inClearJobs)
            {
                log.Warn("***** Deleting existing jobs/triggers *****");
                sched.Clear();//清除数据
            }

            log.Info("------- Initialization Complete -----------");

            if (inScheduleJobs)
            {
                log.Info("------- Scheduling Jobs ------------------");

                string schedId = sched.SchedulerInstanceId;

                int count = 1;


                IJobDetail job = JobBuilder.Create<JobDemo>()
                    .WithIdentity("job_" + count, schedId) // put triggers in group named after the cluster node instance just to distinguish (in logging) what was scheduled from where
                    .RequestRecovery() // ask scheduler to re-execute this job if it was in progress when the scheduler went down...
                    .Build();


                ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                                                              .WithIdentity("triger_" + count, schedId)
                                                              .StartAt(DateBuilder.FutureDate(1, IntervalUnit.Second))
                                                              .WithSimpleSchedule(x => x.WithRepeatCount(2000).WithInterval(TimeSpan.FromSeconds(2)))
                                                              .Build();

                log.InfoFormat("{0} will run at: {1} and repeat: {2} times, every {3} seconds", job.Key, trigger.GetNextFireTimeUtc(), trigger.RepeatCount, trigger.RepeatInterval.TotalSeconds);

                count++;

                sched.ScheduleJob(job, trigger);
            }

            // jobs don't start firing until start() has been called...
            log.Info("------- Starting Scheduler ---------------");
            sched.Start();
            log.Info("------- Started Scheduler ----------------");

            log.Info("------- Waiting for one hour... ----------");

            Thread.Sleep(TimeSpan.FromHours(1));


            log.Info("------- Shutting Down --------------------");
            sched.Shutdown();
            log.Info("------- Shutdown Complete ----------------");
        }
    }
}
