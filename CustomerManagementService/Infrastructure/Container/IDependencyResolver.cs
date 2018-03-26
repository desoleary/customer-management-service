using System;

namespace CustomerManagement.Infrastructure.Container
{
    public interface IDependencyResolver
    {
        T GetInstanceOf<T>();
        object GetInstanceOf(Type service);
    }
}