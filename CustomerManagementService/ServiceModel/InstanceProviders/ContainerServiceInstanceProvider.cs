using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using CustomerManagement.Infrastructure.Container;

namespace CustomerManagement.ServiceModel.InstanceProviders
{
    [ExcludeFromContainer]
    public class ContainerServiceInstanceProvider : IInstanceProvider, IServiceBehavior
    {
        private readonly Type serviceType;

        public ContainerServiceInstanceProvider(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return IoC.GetInstanceOf(serviceType);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var dispatchers = serviceHostBase.ChannelDispatchers.Where(x => x is ChannelDispatcher);

            foreach (ChannelDispatcher dispatcher in dispatchers)
            {
                foreach (var endpoint in dispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.InstanceProvider = this;
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}