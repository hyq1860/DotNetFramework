using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.TaskScheduler;

namespace DotNet.TaskSchedulerConsoleDemo
{
    

    class Program
    {
        static void Main(string[] args)
        {
            //TaskManager taskManager = new TaskManager();
            //taskManager.Test02();
            //Console.ReadKey();
            AdoJobTest adoJobTest=new AdoJobTest();
            adoJobTest.Run(true,true);
        }
    }
}
