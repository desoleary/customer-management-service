using System;

namespace CustomerManagement.Logging
{
    public interface ILogFactory
    {
        ILogger CreateFor(Type type);
        ILogger CreateFor(string name);
    }
}