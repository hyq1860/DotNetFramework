// -----------------------------------------------------------------------
// <copyright file="QuartzJobBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using Formatting = System.Xml.Formatting;

namespace DotNet.TaskScheduler
{
    //public  class QuartzJobBase : IJob
    //{
    //    public void Execute(IJobExecutionContext context)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    // DisallowConcurrentExecution
    [DisallowConcurrentExecution]
    public class QuartzJobTest : IJob
    {
        public void Execute(IJobExecutionContext context)
        {

        }
    }

    public class TaskManager
    {
        //http://www.cnblogs.com/Magicsky/archive/2012/02/07/2341637.html
        //http://blog.csdn.net/wanggang421338916/article/details/7412642
        public void Test()
        {
            //创建一个工作调度器工场
            ISchedulerFactory factory = new StdSchedulerFactory();
            //获取一个任务调度器
            IScheduler scheduler = factory.GetScheduler();
            scheduler.Start();
            //创建一个工作
            IJobDetail job = JobBuilder.Create<QuartzJobTest>().WithIdentity("SampleJob", "JobGroup1").Build();

            //创建触发器

            //1.服务开始时执行
            ITrigger trigger = TriggerBuilder.Create().StartNow().Build();

            //2.
            //该任务执行时间为每隔10秒中，如果要每隔5分钟可以这样0 0/5 * * * * ?                                        
            //这样的格式表示每隔5分钟整执行                                                                             
            ITrigger trigger5 = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSchedule(CronScheduleBuilder.CronSchedule(new CronExpression("0/1 * * * * ?")))
                .Build();

            //2.在指定时间间隔内轮询执行
            //简单的调度作业，比如每隔一小时执行一次哪么简单调度器
            ITrigger trigger2= new SimpleTriggerImpl();
            //用的简单触发器，每隔5秒执行一次 
            ITrigger trigger7 = TriggerBuilder.Create()
                .WithSimpleSchedule(t => t.RepeatForever().WithIntervalInSeconds(5))
                .WithIdentity("t1")
                .Build();

            //3.日轮询执行
            ITrigger trigger3 = new DailyTimeIntervalTriggerImpl("DailyTimeIntervalTrigger", DateTimeOffset.UtcNow, null, new TimeOfDay(1, 0, 0), new TimeOfDay(22, 01, 00), IntervalUnit.Minute, 1);

            //4.复杂的时间设定
            //
            ITrigger trigger4 = new CronTriggerImpl("CronTrigger", "TriggerGroup1", "0 0 12 * * ?");
            ITrigger trigger6 = TriggerBuilder.Create()
                .WithIdentity("trigger6", "group1")
                .WithCronSchedule("0/5 * 18 * * ? ")//每天每天下午的 6点到6点59分每隔5s就触发
                .Build();

            //任务与触发器添加到调度器
            scheduler.ScheduleJob(job, trigger);
        }

        public void TestCronExpression()
        {
            //[秒] [分] [小时] [日] [月] [周] [年]  
            //实例化表达式类，把字符串转成一个对象  
            CronExpression expression = new CronExpression("0 15 10 * * ? 2012");

            while (true)
            {
                DateTimeOffset utcNow = SystemTime.UtcNow();
                Console.WriteLine("UtcNow - " + utcNow);

                //Console.WriteLine("GetFinalFireTime - " + expression.GetFinalFireTime());这个方法没有实现  
                //得到给定时间下一个无效的时间  
                Console.WriteLine("GetNextInvalidTimeAfter - " + expression.GetNextInvalidTimeAfter(utcNow));
                //得到给定时间的下一个有效的时间  
                Console.WriteLine("GetNextValidTimeAfter - " + expression.GetNextValidTimeAfter(utcNow));
                //得到给定时间下一个符合表达式的时间  
                Console.WriteLine("GetTimeAfter - " + expression.GetTimeAfter(utcNow));
                //Console.WriteLine("GetTimeBefore - " + expression.GetTimeBefore(utcNow));这个方法没有实现  
                //给定时间是否符合表达式  
                Console.WriteLine("IsSatisfiedBy - " + expression.IsSatisfiedBy(new DateTimeOffset(2012, 4, 6, 2, 15, 0, TimeSpan.Zero)));
                Console.WriteLine(expression.TimeZone);
                Console.WriteLine("------------------------------------");
                Console.WriteLine(expression.GetExpressionSummary());
                Console.Read();
            }  
        }
    }
}
