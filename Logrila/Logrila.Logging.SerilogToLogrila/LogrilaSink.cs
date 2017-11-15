using System;
using System.Collections.Generic;
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Logrila.Logging.SerilogToLogrila
{
    public class LogrilaSink : ILogEventSink
    {
        private readonly ITextFormatter _textFormatter;
        private static readonly string DefaultLoggerName = "Default";
        private readonly object _syncRoot = new object();
        private Dictionary<string, Logrila.Logging.ILog> _cache = new Dictionary<string, Logrila.Logging.ILog>();

        public LogrilaSink(Serilog.Formatting.ITextFormatter textFormatter)
        {
            _textFormatter = textFormatter;
        }

        public void Emit(Serilog.Events.LogEvent logEvent)
        {
            var loggerName = GetLoggerName(logEvent);
            var logger = GetLogrilaLogger(loggerName);

            var renderSpace = new StringWriter();
            _textFormatter.Format(logEvent, renderSpace);

            if (logEvent.Level == LogEventLevel.Verbose)
                logger.Trace(renderSpace.ToString());
            else if (logEvent.Level == LogEventLevel.Debug)
                logger.Debug(renderSpace.ToString());
            else if (logEvent.Level == LogEventLevel.Information)
                logger.Info(renderSpace.ToString());
            else if (logEvent.Level == LogEventLevel.Warning)
                logger.Warn(renderSpace.ToString());
            else if (logEvent.Level == LogEventLevel.Error)
                logger.Error(renderSpace.ToString());
            else if (logEvent.Level == LogEventLevel.Fatal)
                logger.Fatal(renderSpace.ToString());
            else throw new NotSupportedException("Level " + logEvent.Level + " is currently not supported.");
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
