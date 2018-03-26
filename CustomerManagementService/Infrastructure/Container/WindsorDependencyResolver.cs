using System;
using Castle.Windsor;

namespace CustomerManagement.Infrastructure.Container
{
    [ExcludeFromContainer]
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        public T GetInstanceOf<T>()
        {
            return container.Resolve<T>();
        }

        public object GetInstanceOf(Type service)
        {
            return container.Resolve(service);
        }
    }
}