using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using CustomerManagement.Infrastructure.Container;

namespace CustomerManagement.ServiceModel.InstanceProviders
{
    [ExcludeFromContainer]
    public class DependencyInjectionServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new DependencyInjectionServiceHost(serviceType, baseAddresses);
        }
    }
}