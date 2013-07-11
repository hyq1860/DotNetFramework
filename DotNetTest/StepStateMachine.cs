using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTest
{
    public delegate StepStateProcess StepStateProcess(StepStateContext context, List<StepProcess> stepProcesses);

    public class StepProcess
    {
        public StepProcess()
        {
            PreviousStepStateProcess=new List<StepStateProcess>();
            NextStepStateProcess=new List<StepStateProcess>();
        }

        public DrawState State { get; set; }

        public List<StepStateProcess> PreviousStepStateProcess { get; set; }

        public List<StepStateProcess> NextStepStateProcess { get; set; }
    }

    public enum DrawState
    {
        /// <summary>
        /// 创建提现单
        /// </summary>
        InitDraw = 0,

        /// <summary>
        /// 财务审核通过
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
        /// 财务审核不通过
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
        public List<StepProcess> StepProcesses { get; set; }

        public StepHelper()
        {
            StepProcesses=new List<StepProcess>();

            var stepStateMachine = new StepStateMachine();
            var stepProcess1 = new StepProcess()
                {
                    State = DrawState.InitDraw
                };
            stepProcess1.NextStepStateProcess.Add(stepStateMachine.ApprovedStateProcess);
            stepProcess1.NextStepStateProcess.Add(stepStateMachine.NoApprovedStateProcess);

            var stepProcess2 = new StepProcess()
            {
                State = DrawState.NoApproved
            };
            stepProcess2.PreviousStepStateProcess.Add(stepStateMachine.InitDrawStateProcess);

            var stepProcess3 = new StepProcess()
            {
                State = DrawState.Approved
            };
            stepProcess3.PreviousStepStateProcess.Add(stepStateMachine.InitDrawStateProcess);
            stepProcess3.NextStepStateProcess.Add(stepStateMachine.DrawCompletedStateProcess);

            var stepProcess4 = new StepProcess()
            {
                State = DrawState.DrawCompleted
            };
            stepProcess4.PreviousStepStateProcess.Add(stepStateMachine.ApprovedStateProcess);
            stepProcess4.NextStepStateProcess.Add(stepStateMachine.DrawSuccessedStateProcess);
            stepProcess4.NextStepStateProcess.Add(stepStateMachine.DrawFailStateProcess);

            var stepProcess5 = new StepProcess()
            {
                State = DrawState.DrawSuccessed
            };
            stepProcess5.PreviousStepStateProcess.Add(stepStateMachine.DrawCompletedStateProcess);

            var stepProcess6 = new StepProcess()
            {
                State = DrawState.DrawFail
            };
            stepProcess6.NextStepStateProcess.Add(stepStateMachine.DrawCompletedStateProcess);

            StepProcesses.Add(stepProcess1);
            StepProcesses.Add(stepProcess2);
            StepProcesses.Add(stepProcess3);
            StepProcesses.Add(stepProcess4);
            StepProcesses.Add(stepProcess5);
            StepProcesses.Add(stepProcess6);
        }

        private StepStateProcess Previous;
        private StepStateProcess Next;
        public void Parse(StepStateContext context)
        {
            var maxState = context.CurrentStatus > context.CurrentDrawStatus
                               ? context.CurrentStatus
                               : context.CurrentDrawStatus;
            var stepProcess = StepProcesses.FirstOrDefault(s => s.State == maxState);

            

        }
    }

    public abstract class BaseStepProcess
    {
        public DrawState State { get; set; }

        public StepStateProcess PreviousStepStateProcess { get; set; }

        public StepStateProcess NextStepStateProcess { get; set; }

        public virtual void SetPN(StepStateContext context){}
    }

    public class InitDrawStateProcess :BaseStepProcess
    {
        private StepStateContext _context;
        public InitDrawStateProcess(StepStateContext context)
        {
            State=DrawState.InitDraw;
            _context = context;
        }

        public override void SetPN(StepStateContext context)
        {
            
        }
        
    }


    public class StepStateMachine
    {
        /// <summary>
        /// 创建提现单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public StepStateProcess InitDrawStateProcess(StepStateContext context,List<StepProcess> stepProcesses)
        {
            context.Steps.Add(new Step(){State = DrawState.InitDraw,Name="创建提现单",Date = context.InDate.ToString()});
            if (context.CurrentDrawStatus == DrawState.None)
            {
                return ApprovedStateProcess;
            }
            return NoApprovedStateProcess;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public StepStateProcess ApprovedStateProcess(StepStateContext context, List<StepProcess> stepProcesses)
        {
            return DrawCompletedStateProcess;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public StepStateProcess DrawCompletedStateProcess(StepStateContext context, List<StepProcess> stepProcesses)
        {
            return DrawSuccessedStateProcess;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public StepStateProcess DrawSuccessedStateProcess(StepStateContext context, List<StepProcess> stepProcesses)
        {
            return null;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public StepStateProcess DrawFailStateProcess(StepStateContext context, List<StepProcess> stepProcesses)
        {
            return null;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public StepStateProcess NoApprovedStateProcess(StepStateContext context, List<StepProcess> stepProcesses)
        {
            return null;
        }

        
    }
}
