using System;
using System.Collections.Generic;

namespace Logrila.Logging.Log4NetToLogrila
{
    public class LogrilaAppender : log4net.Appender.AppenderSkeleton
    {
        private readonly object _syncRoot = new object();
        private Dictionary<string, Logrila.Logging.ILog> _cache = new Dictionary<string, Logrila.Logging.ILog>();

        public static void Use()
        {
            log4net.Config.BasicConfigurator.Configure(new LogrilaAppender());
        }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            var logger = GetLogrilaLogger(loggingEvent.LoggerName);

            if (loggingEvent.Level == log4net.Core.Level.Trace)
                logger.Trace(Convert.ToString(loggingEvent.MessageObject));
            else if (loggingEvent.Level == log4net.Core.Level.Debug)
                logger.Debug(Convert.ToString(loggingEvent.MessageObject));
            else if (loggingEvent.Level == log4net.Core.Level.Info)
                logger.Info(Convert.ToString(loggingEvent.MessageObject));
            else if (loggingEvent.Level == log4net.Core.Level.Warn)
                logger.Warn(Convert.ToString(loggingEvent.MessageObject));
            else if (loggingEvent.Level == log4net.Core.Level.Error)
                logger.Error(Convert.ToString(loggingEvent.MessageObject));
            else if (loggingEvent.Level == log4net.Core.Level.Fatal)
                logger.Fatal(Convert.ToString(loggingEvent.MessageObject));
            else if (loggingEvent.Level == log4net.Core.Level.Off) { }
            else throw new NotSupportedException("Level " + loggingEvent.Level + " is currently not supported.");
        }

        private Logrila.Logging.ILog GetLogrilaLogger(string loggerName)
        {
            Logrila.Logging.ILog logger;

            if (_cache.TryGetValue(loggerName, out logger))
                return logger;

            lock (_syncRoot)
            {
                if (_cache.TryGetValue(loggerName, out logger))
                    return logger;

                logger = Logrila.Logging.Logger.Get(loggerName);
                _cache[loggerName] = logger;
            }

            return logger;
        }
    }
}
