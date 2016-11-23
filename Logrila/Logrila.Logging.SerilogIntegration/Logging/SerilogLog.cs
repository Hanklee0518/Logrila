using System;

namespace Logrila.Logging.SerilogIntegration
{
    public class SerilogLog : ILog
    {
        private readonly Serilog.ILogger _logger;
        private readonly SerilogPreformatter _preformatter;

        public SerilogLog(Serilog.ILogger logger, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            _logger = logger;
            _preformatter = SerilogPreformatter.Singleton;
        }

        public bool IsTraceEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Trace.ToSerilogEventLevel()); }
        }

        public bool IsDebugEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Debug.ToSerilogEventLevel()); }
        }

        public bool IsInfoEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Info.ToSerilogEventLevel()); }
        }

        public bool IsWarnEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Warn.ToSerilogEventLevel()); }
        }

        public bool IsErrorEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Error.ToSerilogEventLevel()); }
        }

        public bool IsFatalEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Fatal.ToSerilogEventLevel()); }
        }

        public void Trace(object obj)
        {
            Write(LogLevel.Trace, obj);
        }

        public void Trace(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            Write(LogLevel.Trace, exception, message);
        }

        public void Trace(LogOutputProvider logOutputProvider)
        {
            Write(LogLevel.Trace, logOutputProvider());
        }

        public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            WriteFormat(LogLevel.Trace, formatProvider, format, args);
        }

        public void TraceFormat(string format, params object[] args)
        {
            WriteFormat(LogLevel.Trace, format, args);
        }

        public void Debug(object obj)
        {
            Write(LogLevel.Debug, obj);
        }

        public void Debug(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            Write(LogLevel.Debug, exception, message);
        }

        public void Debug(LogOutputProvider logOutputProvider)
        {
            Write(LogLevel.Debug, logOutputProvider());
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            WriteFormat(LogLevel.Debug, formatProvider, format, args);
        }

        public void DebugFormat(string format, params object[] args)
        {
            WriteFormat(LogLevel.Debug, format, args);
        }

        public void Info(object obj)
        {
            Write(LogLevel.Info, obj);
        }

        public void Info(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            Write(LogLevel.Info, exception, message);
        }

        public void Info(LogOutputProvider logOutputProvider)
        {
            Write(LogLevel.Info, logOutputProvider());
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            WriteFormat(LogLevel.Info, formatProvider, format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            WriteFormat(LogLevel.Info, format, args);
        }

        public void Warn(object obj)
        {
            Write(LogLevel.Warn, obj);
        }

        public void Warn(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            Write(LogLevel.Warn, exception, message);
        }

        public void Warn(LogOutputProvider logOutputProvider)
        {
            Write(LogLevel.Warn, logOutputProvider());
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            WriteFormat(LogLevel.Warn, formatProvider, format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            WriteFormat(LogLevel.Warn, format, args);
        }

        public void Error(object obj)
        {
            Write(LogLevel.Error, obj);
        }

        public void Error(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            Write(LogLevel.Error, exception, message);
        }

        public void Error(LogOutputProvider logOutputProvider)
        {
            Write(LogLevel.Error, logOutputProvider());
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            WriteFormat(LogLevel.Error, formatProvider, format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            WriteFormat(LogLevel.Error, format, args);
        }

        public void Fatal(object obj)
        {
            Write(LogLevel.Fatal, obj);
        }

        public void Fatal(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            Write(LogLevel.Fatal, exception, message);
        }

        public void Fatal(LogOutputProvider logOutputProvider)
        {
            Write(LogLevel.Fatal, logOutputProvider());
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            WriteFormat(LogLevel.Fatal, formatProvider, format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            WriteFormat(LogLevel.Fatal, format, args);
        }

        private void Write(LogLevel level, object message)
        {
            Write(level, null, message);
        }

        private void Write(LogLevel level, Exception exception, object message)
        {
            if (message is string)
                _logger.Write(level.ToSerilogEventLevel(), exception, "{Message:l}", message.ToString());
            else
                _logger.Write(level.ToSerilogEventLevel(), exception, "{@Message}", message);
        }

        private void WriteFormat(LogLevel level, Exception exception, string message, object[] parameters)
        {
            WriteFormat(level, exception, null, message, parameters);
        }

        private void WriteFormat(LogLevel level, IFormatProvider formatProvider, string message, object[] parameters)
        {
            WriteFormat(level, null, formatProvider, message, parameters);
        }

        private void WriteFormat(LogLevel level, string message, object[] parameters)
        {
            WriteFormat(level, null, null, message, parameters);
        }

        private void WriteFormat(LogLevel level, Exception exception, IFormatProvider formatProvider, string message, object[] parameters)
        {
            if (formatProvider == null)
            {
                string serilogTemplate;
                object[] serilogArgs;
                _preformatter.TryPreformat(message, parameters, out serilogTemplate, out serilogArgs);
                _logger.Write(level.ToSerilogEventLevel(), exception, serilogTemplate, serilogArgs);
            }
            else
            {
                Write(level, exception, string.Format(formatProvider, message, parameters));
            }
        }
    }
}
