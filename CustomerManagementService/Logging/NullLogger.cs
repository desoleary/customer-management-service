using System;

namespace CustomerManagement.Logging
{
    public class NullLogger : ILogger
    {
        public void Trace(IFormatProvider provider, string format, params object[] args)
        {
        }

        public void Trace(string format, params object[] args)
        {
        }

        public void Debug(string format, params object[] args)
        {
        }

        public void Info(string format, params object[] args)
        {
        }

        public void Warn(string format, params object[] args)
        {
        }

        public void Error(string format, params object[] args)
        {
        }

        public void Fatal(string format, params object[] args)
        {
        }

        public void Exception(string message, Exception exception)
        {
        }
    }
}