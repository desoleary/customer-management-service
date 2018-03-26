using System;

namespace CustomerManagement.Logging
{
    public class NullLogFactory : ILogFactory
    {
        public ILogger CreateFor(Type type)
        {
            return new NullLogger();
        }

        public ILogger CreateFor(string name)
        {
            return new NullLogger();
        }
    }
}