using System;
using System.Collections.Generic;

namespace Logrila.Logging.Log4NetToNLog
{
    public class NLogAppender : log4net.Appender.AppenderSkeleton
    {
        private readonly object _syncRoot = new object();
        private Dictionary<string, NLog.Logger> _cache = new Dictionary<string, NLog.Logger>();

        public static void Use()
        {
            log4net.Config.BasicConfigurator.Configure(new NLogAppender());
        }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            var logger = GetNLogLogger(loggingEvent.LoggerName);
            var logEvent = ConvertToNLog(loggingEvent);
            logger.Log(logEvent);
        }

        private NLog.Logger GetNLogLogger(string loggerName)
        {
            NLog.Logger logger;

            if (_cache.TryGetValue(loggerName, out logger))
                return logger;

            lock (_syncRoot)
            {
                if (_cache.TryGetValue(loggerName, out logger))
                    return logger;

                logger = NLog.LogManager.GetLogger(loggerName);
                _cache[loggerName] = logger;
            }

            return logger;
        }

        private static NLog.LogEventInfo ConvertToNLog(log4net.Core.LoggingEvent loggingEvent)
        {
            return new NLog.LogEventInfo
            {
                Exception = loggingEvent.ExceptionObject,
                FormatProvider = null,
                LoggerName = loggingEvent.LoggerName,
                Message = Convert.ToString(loggingEvent.MessageObject),
                Level = ConvertToNLogLevel(loggingEvent.Level),
                TimeStamp = loggingEvent.TimeStamp
            };
        }

        private static NLog.LogLevel ConvertToNLogLevel(log4net.Core.Level level)
        {
            if (level == log4net.Core.Level.Trace)
                return NLog.LogLevel.Trace;
            if (level == log4net.Core.Level.Debug)
                return NLog.LogLevel.Debug;
            if (level == log4net.Core.Level.Info)
                return NLog.LogLevel.Info;
            if (level == log4net.Core.Level.Warn)
                return NLog.LogLevel.Warn;
            if (level == log4net.Core.Level.Error)
                return NLog.LogLevel.Error;
            if (level == log4net.Core.Level.Fatal)
                return NLog.LogLevel.Fatal;
            if (level == log4net.Core.Level.Off)
                return NLog.LogLevel.Off;

            throw new NotSupportedException("Level " + level + " is currently not supported.");
        }
    }
}
