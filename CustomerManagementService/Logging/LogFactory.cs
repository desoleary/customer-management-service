using System;

namespace CustomerManagement.Logging
{
    public class LogFactory : ILogFactory
    {
        private readonly ILogAdapter logAdapter;

        public LogFactory(ILogAdapter logAdapter)
        {
            this.logAdapter = logAdapter;
        }

        public ILogger CreateFor(Type type)
        {
            return CreateFor(type.FullName);
        }

        public ILogger CreateFor(string name)
        {
            return logAdapter.CreateLogger(name);
        }
    }
}