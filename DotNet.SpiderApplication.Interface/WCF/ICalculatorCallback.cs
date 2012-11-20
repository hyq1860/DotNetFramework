using System.ServiceModel;
using DotNet.SpiderApplication.Contract.Entity;

namespace DotNet.SpiderApplication.Contract
{
    using System.Collections.Generic;

    public interface ICalculatorCallback : ISessionCallback
    {
        [OperationContract(IsOneWay = true)]
        void DisplayResult(double result, double x, double y);

        [OperationContract(IsOneWay = true)]
        void Process(List<string> data);

        [OperationContract(IsOneWay = true)]
        void Display();
    }
}