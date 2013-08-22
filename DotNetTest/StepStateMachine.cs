using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTest
{
    public delegate StepStateProcess StepStateProcess(StepStateContext context);

    public class StepProcess
    {
        public StepProcess()
        {
        }

        public DrawState State { get; set; }

        public StepStateProcess PreviousStepStateProcess { get; set; }

        public StepStateProcess NextStepStateProcess { get; set; }
    }

    public enum DrawState
    {
        /// <summary>
        /// 创建提现单
        /// </summary>
        InitDraw = 0,

        /// <summary>
        /// 审核通过
        /// </summary>
        Approved = 1,

        /// <summary>
        /// 转账成功
        /// </summary>
        DrawSuccessed = 2,

        /// <summary>
        /// 转账完成
        /// </summary>
        DrawCompleted = 3,

        /// <summary>
        /// 转账失败
        /// </summary>
        DrawFail = 4,

        /// <summary>
        /// 审核不通过
        /// </summary>
        NoApproved = -1,

        /// <summary>
        /// 无状态
        /// </summary>
        None,
    }

    public class Step
    {
        public string Name { get; set; }

        public string Date { get; set; }

        public DrawState State { get; set; }
    }

    /// <summary>
    /// 进度状态机上下文
    /// </summary>
    public class StepStateContext
    {
        public DrawState CurrentStatus { get; set; }

        public DrawState CurrentDrawStatus { get; set; }

        /// <summary>
        /// 提交提现单时间
        /// </summary>
        public DateTime InDate { get; set; }

        /// <summary>
        /// 审核不通过、转账失败等最终状态时间
        /// </summary>
        public DateTime EditDate { get; set; }

        /// <summary>
        /// 审核通过时间
        /// </summary>
        public DateTime AuditDate { get; set; }

        /// <summary>
        /// 转账时间
        /// </summary>
        public DateTime TransferDate { get; set; }

        /// <summary>
        /// 转账成功时间
        /// </summary>
        public DateTime TransferSucessDate { get; set; }

        /// <summary>
        /// 进度条
        /// </summary>
        public List<Step> Steps { get; set; }
    }

    public class StepHelper
    {
        public Dictionary<int, List<StepProcess>> ProcessDict { get; set; } 

        public StepHelper()
        {
            ProcessDict=new Dictionary<int, List<StepProcess>>();

            //1(0=>-1)
            var process1 = new List<StepProcess>();
            ProcessDict.Add(1, process1);
            process1.Add(new StepProcess() { State = DrawState.InitDraw, PreviousStepStateProcess = null, NextStepStateProcess = StepStateMachine.NoApprovedStateProcess });
            process1.Add(new StepProcess() { State = DrawState.NoApproved, PreviousStepStateProcess = StepStateMachine.InitDrawStateProcess, NextStepStateProcess = null });

            //2(0=>1=>3=>2)
            var process2 = new List<StepProcess>();
            ProcessDict.Add(2, process2);

            //3((0=>1=>3=>4))
            var process3 = new List<StepProcess>();
            ProcessDict.Add(3, process3);
        }

        private StepStateProcess Previous;
        private StepStateProcess Next;

        public List<StepProcess> Chose(StepStateContext context)
        {
            var maxState = context.CurrentStatus > context.CurrentDrawStatus
                               ? context.CurrentStatus
                               : context.CurrentDrawStatus;
            switch (maxState)
            {
                case DrawState.InitDraw:
                    {
                        return ProcessDict[2];
                    }
                    break;
                    case DrawState.NoApproved:
                    {
                        return ProcessDict[1];
                    }
                    break;
                case DrawState.Approved:
                    {
                        return ProcessDict[2];
                    }
                    break;
                    case DrawState.DrawCompleted:
                    {
                        return ProcessDict[2];
                    }
                    break;
                    case DrawState.DrawSuccessed:
                    {
                        return ProcessDict[2];
                    }
                    break;
                case DrawState.DrawFail:
                    {
                        return ProcessDict[3];
                    }
                    break;
            }
            return new List<StepProcess>();
        }

        public void Parse(StepStateContext context)
        {
            var maxState = context.CurrentStatus > context.CurrentDrawStatus
                               ? context.CurrentStatus
                               : context.CurrentDrawStatus;
            var processLines = Chose(context);
            if (!processLines.Any())
            {
                return;
            }
            processLines.FirstOrDefault(s => s.State == maxState);
        }
    }



    public class StepStateMachine
    {
        /// <summary>
        /// 创建提现单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static StepStateProcess InitDrawStateProcess(StepStateContext context)
        {

        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static StepStateProcess ApprovedStateProcess(StepStateContext context)
        {

        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static StepStateProcess DrawCompletedStateProcess(StepStateContext context)
        {

        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static StepStateProcess DrawSuccessedStateProcess(StepStateContext context)
        {
            return null;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static  StepStateProcess DrawFailStateProcess(StepStateContext context)
        {
            return null;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static StepStateProcess NoApprovedStateProcess(StepStateContext context)
        {
            return null;
        }

        
    }
}
