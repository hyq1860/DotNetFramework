using System;

namespace DotNet.SpiderApplication.WebSite
{
    using DotNet.IoC;
    using DotNet.SpiderApplication.Contract;
    using DotNet.SpiderApplication.Service;

    using Microsoft.Practices.ServiceLocation;

    using Ninject;

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // IOC的注入
            BootStrapperManager.Initialize(new NinjectBootstrapper());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }

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
            //此处注册你的服务
            Bind<IProductDataAccess>().To<ProductDataAccess>();
            Bind<IProductService>().To<ProductService>();
        }
    }
}