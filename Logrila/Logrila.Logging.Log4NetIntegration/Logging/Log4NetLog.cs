using System;

namespace Logrila.Logging.Log4NetIntegration
{
    public class Log4NetLog : ILog
    {
        private readonly log4net.ILog _logger;

        public Log4NetLog(log4net.ILog logger, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            _logger = logger;
        }

        public bool IsTraceEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }

        public void Trace(object obj)
        {
            _logger.Debug(obj);
        }

        public void Trace(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            _logger.Debug(message, exception);
        }

        public void Trace(LogOutputProvider logOutputProvider)
        {
            _logger.Debug(logOutputProvider());
        }

        public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.DebugFormat(formatProvider, format, args);
        }

        public void TraceFormat(string format, params object[] args)
        {
            _logger.DebugFormat(format, args);
        }

        public void Debug(object obj)
        {
            _logger.Debug(obj);
        }

        public void Debug(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            _logger.Debug(message, exception);
        }

        public void Debug(LogOutputProvider logOutputProvider)
        {
            _logger.Debug(logOutputProvider());
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.DebugFormat(formatProvider, format, args);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _logger.DebugFormat(format, args);
        }

        public void Info(object obj)
        {
            _logger.Info(obj);
        }

        public void Info(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            _logger.Info(message, exception);
        }

        public void Info(LogOutputProvider logOutputProvider)
        {
            _logger.Info(logOutputProvider());
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.InfoFormat(formatProvider, format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _logger.InfoFormat(format, args);
        }

        public void Warn(object obj)
        {
            _logger.Warn(obj);
        }

        public void Warn(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            _logger.Warn(message, exception);
        }

        public void Warn(LogOutputProvider logOutputProvider)
        {
            _logger.Warn(logOutputProvider());
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.WarnFormat(formatProvider, format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _logger.WarnFormat(format, args);
        }

        public void Error(object obj)
        {
            _logger.Error(obj);
        }

        public void Error(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            _logger.Error(message, exception);
        }

        public void Error(LogOutputProvider logOutputProvider)
        {
            _logger.Error(logOutputProvider());
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.ErrorFormat(formatProvider, format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _logger.ErrorFormat(format, args);
        }

        public void Fatal(object obj)
        {
            _logger.Fatal(obj);
        }

        public void Fatal(object obj, Exception exception)
        {
            string message = string.Format("{0}{1}{2}", obj == null ? "" : obj.ToString(), Environment.NewLine, ExceptionRender.Parse(exception));
            _logger.Fatal(message, exception);
        }

        public void Fatal(LogOutputProvider logOutputProvider)
        {
            _logger.Fatal(logOutputProvider());
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.FatalFormat(formatProvider, format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _logger.FatalFormat(format, args);
        }
    }
}
