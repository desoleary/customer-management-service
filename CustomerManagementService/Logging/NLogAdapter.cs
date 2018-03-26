using System;
using NLog;

namespace CustomerManagement.Logging
{
    public class NLogAdapter : ILogAdapter
    {

        private Logger logger;

        public ILogger CreateLogger(string loggerName)
        {
            logger = LogManager.GetLogger(loggerName);
            return this;
        }

        public void Trace(IFormatProvider provider, string format, params object[] args)
        {
            if (logger.IsTraceEnabled)
                logger.Trace(provider, format, args);
        }

        public void Trace(string format, params object[] args)
        {
            Log(LogLevel.Trace, format, args);
        }

        public void Debug(string format, params object[] args)
        {
            Log(LogLevel.Debug, format, args);
        }

        public void Info(string format, params object[] args)
        {
            Log(LogLevel.Info, format, args);
        }

        public void Warn(string format, params object[] args)
        {
            Log(LogLevel.Warn, format, args);
        }

        public void Error(string format, params object[] args)
        {
            Log(LogLevel.Error, format, args);
        }

        public void Fatal(string format, params object[] args)
        {
            Log(LogLevel.Fatal, format, args);
        }

        public void Exception(string message, Exception exception)
        {
            logger.FatalException(message, exception);
        }

        private void Log(LogLevel level, string format, params object[] args)
        {
            if (logger.IsEnabled(level))
                logger.Log(level, format, args);
        }
    }

    public interface ILogAdapter : ILogger
    {
        ILogger CreateLogger(string loggerName);
    }
}