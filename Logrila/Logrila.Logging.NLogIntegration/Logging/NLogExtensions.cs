using TLogLevel = Logrila.Logging.LogLevel;

namespace Logrila.Logging.NLogIntegration
{
    internal static class NLogExtensions
    {
        public static NLog.LogLevel ToNLogLogLevel(this TLogLevel logLevel)
        {
            if (logLevel == TLogLevel.Trace)
                return NLog.LogLevel.Trace;
            else if (logLevel == TLogLevel.Debug)
                return NLog.LogLevel.Debug;
            else if (logLevel == TLogLevel.Info)
                return NLog.LogLevel.Info;
            else if (logLevel == TLogLevel.Warn)
                return NLog.LogLevel.Warn;
            else if (logLevel == TLogLevel.Error)
                return NLog.LogLevel.Error;
            else if (logLevel == TLogLevel.Fatal)
                return NLog.LogLevel.Fatal;

            return NLog.LogLevel.Off;
        }
    }
}
