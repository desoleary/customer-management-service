using System;

namespace CustomerManagement.Logging
{
    public interface ILogger
    {
        void Trace(IFormatProvider provider, string format, params object[] args);
        void Trace(string format, params object[] args);
        void Debug(string format, params object[] args);
        void Info(string format, params object[] args);
        void Warn(string format, params object[] args);
        void Error(string format, params object[] args);
        void Fatal(string format, params object[] args);

        void Exception(string message, Exception exception);
    }
}