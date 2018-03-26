using System;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Persistance.Students;

namespace CustomerManagement.ServiceModel.InstanceProviders
{
    [ExcludeFromContainer]
    public class DependencyInjectionServiceHost : ServiceHost
    {
        private readonly Type serviceType;

        public DependencyInjectionServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this.serviceType = serviceType;
        }

        protected override void OnOpening()
        {
            var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CustomerManagement.ActiveRecord.config");  

            var source = new XmlConfigurationSource(fileStream);
            
            if(!ActiveRecordStarter.IsInitialized)
            {
                ActiveRecordStarter.Initialize(source, typeof(StudentDAO));
            }            
            

            WindsorBootstrapper.Initialize(new WindsorContainer(new XmlInterpreter()));

            Description.Behaviors.Add(new ContainerServiceInstanceProvider(serviceType));
            base.OnOpening();
        }
    }
}