// -----------------------------------------------------------------------
// <copyright file="UnityServiceLocator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Practices.ServiceLocation;

namespace DotNet.IoC.ServiceLocator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text; 
    // 测试 
    //public class UnityServiceLocator : ServiceLocatorImplBase
    //{
    //    private IUnityContainer _container;
    //    public UnityServiceLocator(IUnityContainer container)
    //    {
    //        _container = container;
    //    }
    //    protected override object DoGetInstance(Type serviceType, string key)
    //    {
    //        return _container.Resolve(serviceType, key);
    //    }


    //    protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
    //    {
    //        return _container.ResolveAll(serviceType);
    //    }
    //}
}
