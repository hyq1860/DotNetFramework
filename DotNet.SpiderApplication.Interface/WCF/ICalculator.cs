using System.ServiceModel;

namespace DotNet.SpiderApplication.Contract
{
    using System.Collections.Generic;

    using DotNet.SpiderApplication.Contract.Entity;

    [ServiceContract(Namespace = "http://www.artech.com/", CallbackContract = typeof(ICalculatorCallback))]
    public interface ICalculator : ISessionManagement
    {
        [OperationContract(IsOneWay = true)]
        void Add(double x, double y);

        [OperationContract(IsOneWay = false)]
        IList<SpiderParameter> GetProcessData(string name);

        [OperationContract(IsOneWay = true)]
        void ServerProcess(List<SpiderResult> data);

        int ProcessCount { get; set; }
    }
}
