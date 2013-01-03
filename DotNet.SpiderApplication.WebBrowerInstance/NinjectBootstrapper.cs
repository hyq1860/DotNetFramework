// -----------------------------------------------------------------------
// <copyright file="NinjectBootstrapper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.ServiceModel;
using Ninject.Extensions.Wcf;

namespace DotNet.SpiderApplication.WebBrowerInstance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.IoC;
    using DotNet.SpiderApplication.Contract;
    using DotNet.SpiderApplication.Contract.WCF;
    using DotNet.SpiderApplication.Service;

    using Microsoft.Practices.ServiceLocation;

    using Ninject;

    public class NinjectBootstrapper : CommonBootStrapper
    {
        protected override IServiceLocator CreateServiceLocator()
        {
            return new NinjectServiceLocator(new StandardKernel(new RegisterServiceModule()));
        }
    }

    internal class RegisterServiceModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            this.Bind<ICommonSpider>().To<CommonSpiderServiceProxy>();
            this.Bind<ISpiderClientToManageClient>().To<SpiderClientToManageClientProxy>();
        }
    }
}
