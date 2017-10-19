using System;
using NLog;

namespace Logrila.Logging.NLogIntegration
{
    public class NLogLogger : Logrila.Logging.ILogger
    {
        private readonly Func<string, NLog.Logger> _loggerFactory;

        public NLogLogger()
        {
            _loggerFactory = NLog.LogManager.GetLogger;
        }

        public NLogLogger(Func<string, NLog.Logger> loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public NLogLogger(LogFactory logFactory)
        {
            _loggerFactory = logFactory.GetLogger;
        }

        public ILog Get(string name)
        {
            return new NLogLog(_loggerFactory(name), name);
        }

        public static void Use()
        {
            Logrila.Logging.Logger.UseLogger(new NLogLogger());
        }

        public static void Use(Func<string, NLog.Logger> loggerFactory)
        {
            Logrila.Logging.Logger.UseLogger(new NLogLogger(loggerFactory));
        }
    }
}
