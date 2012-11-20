using Microsoft.Practices.ServiceLocation;

namespace DotNet.IoC
{
    public abstract class CommonBootStrapper
    {
        public static IServiceLocator ServiceLocator;

        protected CommonBootStrapper()
        {
            ServiceLocator = CreateServiceLocator();
        }

        protected abstract IServiceLocator CreateServiceLocator();
    }
}
