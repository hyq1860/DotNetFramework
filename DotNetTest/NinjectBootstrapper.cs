using Microsoft.Practices.ServiceLocation;
using Ninject;
using DotNet.IoC;

namespace DotNetTest
{
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
            Bind<IAdd>().To<AddTest>();
        }
    }
}
