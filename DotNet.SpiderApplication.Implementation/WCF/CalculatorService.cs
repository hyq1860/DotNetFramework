using System;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using DotNet.SpiderApplication.Contract;
using DotNet.SpiderApplication.Contract.Entity;

namespace DotNet.SpiderApplication.Service
{
    using System.Collections.Generic;
    //using System.ServiceModel.Activation;

    

     //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerSession)]
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CalculatorService : ICalculator
    {
        public int ProcessCount { get; set; }
        public void Add(double x, double y)
        {
            double result = x + y;
            ICalculatorCallback callback = OperationContext.Current.GetCallbackChannel<ICalculatorCallback>();
            callback.DisplayResult(result, x, y);
        }

        public IList<SpiderParameter> GetProcessData(string name)
        {
            var recordCount = DataAccess.GetProductsCount();
            var data = new List<SpiderParameter>();
            var dt1 = DataAccess.GetProduct(" where IsProcess=0 ORDER BY RANDOM()  LIMIT " + 2);
            foreach (DataRow dr in dt1.Rows)
            {
                var rowId = Convert.ToInt32(dr["RowId"]);
                var url = Convert.ToString(dr["Url"]);
                var productId = Convert.ToString(dr["ProductId"]);
                data.Add(new SpiderParameter() { ProductId = productId, Url = url });
            }
            return data;
            //ICalculatorCallback callback = OperationContext.Current.GetCallbackChannel<ICalculatorCallback>();
            //callback.Process(data);
        }

        public void ServerProcess(List<SpiderResult> data)
        {
            foreach (var spiderResult in data)
            {
                DataAccess.UpdateProduct(spiderResult.ProductId,spiderResult.Name,spiderResult.Price,spiderResult.IsSucceed?1:0,spiderResult.Elapse,spiderResult.LastModify);
            }
            
            ICalculatorCallback callback = OperationContext.Current.GetCallbackChannel<ICalculatorCallback>();
            callback.Display();
            //ProcessCount++;
        }

        #region ISessionManagement Members

        public Guid StartSession(SessionClientInfo clientInfo, out TimeSpan timeout)
        {
            timeout = SessionManager.Timeout;
            return SessionManager.StartSession(clientInfo);
        }

        public void EndSession(Guid sessionID)
        {
            SessionManager.EndSession(sessionID);
        }

        public IList<SessionInfo> GetActiveSessions()
        {
            return new List<SessionInfo>(SessionManager.CurrentSessionList.Values.ToArray<SessionInfo>());
        }

        public void KillSessions(IList<Guid> sessionIDs)
        {
            SessionManager.KillSessions(sessionIDs);
        }

        #endregion
    }
}
