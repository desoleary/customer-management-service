using System;

namespace CustomerManagement.Infrastructure.Container
{
    public static class IoC
    {
        private static IDependencyResolver resolver;

        public static void Initialize(IDependencyResolver resolver)
        {
            IoC.resolver = resolver;
        }

        public static T GetInstanceOf<T>()
        {
            return resolver.GetInstanceOf<T>();
        }

        public static object GetInstanceOf(Type service)
        {
            return resolver.GetInstanceOf(service);
        }
    }
}