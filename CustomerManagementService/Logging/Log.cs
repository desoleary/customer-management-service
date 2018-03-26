using CustomerManagement.Infrastructure.Container;

namespace CustomerManagement.Logging
{
    public static class Log
    {
        private static ILogFactory Factory
        {
            get { return IoC.GetInstanceOf<ILogFactory>(); }
        }

        public static ILogger For(object instance)
        {
            return Factory.CreateFor(instance.GetType());
        }

        public static ILogger For(string name)
        {
            return Factory.CreateFor(name);
        }
    }
}