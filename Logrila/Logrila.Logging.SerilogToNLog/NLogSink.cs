using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Logrila.Logging.SerilogToNLog
{
    public class NLogSink : ILogEventSink
    {
        private readonly ITextFormatter _textFormatter;
        private static readonly string DefaultLoggerName = "Default";
        private readonly object _syncRoot = new object();
        private Dictionary<string, NLog.Logger> _cache = new Dictionary<string, NLog.Logger>();

        public NLogSink(Serilog.Formatting.ITextFormatter textFormatter)
        {
            _textFormatter = textFormatter;
        }

        public void Emit(Serilog.Events.LogEvent logEvent)
        {
            var nlogEvent = ConvertToNLog(logEvent);
            var logger = GetNLogLogger(nlogEvent.LoggerName);
            logger.Log(nlogEvent);
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

        private NLog.LogEventInfo ConvertToNLog(Serilog.Events.LogEvent logEvent)
        {
            var renderSpace = new StringWriter();
            _textFormatter.Format(logEvent, renderSpace);

            var nlogEvent = new NLog.LogEventInfo
            {
                Exception = logEvent.Exception,
                FormatProvider = null,
                LoggerName = GetLoggerName(logEvent),
                Message = renderSpace.ToString(),
                Level = ConvertToNLogLevel(logEvent.Level),
                TimeStamp = logEvent.Timestamp.UtcDateTime,
            };

            foreach (var property in logEvent.Properties)
            {
                var sv = property.Value as ScalarValue;
                var format = (sv != null && sv.Value is string) ? "l" : null;

                nlogEvent.Properties[property.Key] = property.Value.ToString(format, null);
            }

            return nlogEvent;
        }

        private static string GetLoggerName(Serilog.Events.LogEvent logEvent)
        {
            var loggerName = DefaultLoggerName;

            LogEventPropertyValue sourceContext;
            if (logEvent.Properties.TryGetValue(Constants.SourceContextPropertyName, out sourceContext))
            {
                var sv = sourceContext as ScalarValue;
                if (sv != null && sv.Value is string)
                {
                    loggerName = (string)sv.Value;
                }
            }

            return loggerName;
        }

        private static NLog.LogLevel ConvertToNLogLevel(LogEventLevel level)
        {
            switch (level)
            {
                case LogEventLevel.Verbose:
                    return LogLevel.Trace;
                case LogEventLevel.Debug:
                    return LogLevel.Debug;
                case LogEventLevel.Information:
                    return LogLevel.Info;
                case LogEventLevel.Warning:
                    return LogLevel.Warn;
                case LogEventLevel.Error:
                    return LogLevel.Error;
                case LogEventLevel.Fatal:
                    return LogLevel.Fatal;
                default:
                    throw new NotSupportedException("Level " + level + " is currently not supported.");
            }
        }
    }
}
