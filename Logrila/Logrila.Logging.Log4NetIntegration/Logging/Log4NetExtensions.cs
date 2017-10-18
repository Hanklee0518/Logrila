using log4net.Core;

namespace Logrila.Logging.Log4NetIntegration
{
    internal static class Log4NetExtensions
    {
        public static Level ToLog4NetLogLevel(this LogLevel logLevel)
        {
            if (logLevel == LogLevel.Trace)
                return Level.Trace;
            else if (logLevel == LogLevel.Debug)
                return Level.Debug;
            else if (logLevel == LogLevel.Info)
                return Level.Info;
            else if (logLevel == LogLevel.Warn)
                return Level.Warn;
            else if (logLevel == LogLevel.Error)
                return Level.Error;
            else if (logLevel == LogLevel.Fatal)
                return Level.Fatal;

            return Level.Off;
        }
    }
}
