using Serilog.Events;

namespace Logrila.Logging.SerilogIntegration
{
    internal static class SerilogExtensions
    {
        public static LogEventLevel ToSerilogEventLevel(this LogLevel logLevel)
        {
            if (logLevel == LogLevel.Trace)
                return LogEventLevel.Verbose;
            else if (logLevel == LogLevel.Debug)
                return LogEventLevel.Debug;
            else if (logLevel == LogLevel.Info)
                return LogEventLevel.Information;
            else if (logLevel == LogLevel.Warn)
                return LogEventLevel.Warning;
            else if (logLevel == LogLevel.Error)
                return LogEventLevel.Error;
            else if (logLevel == LogLevel.Fatal)
                return LogEventLevel.Fatal;

            return LogEventLevel.Verbose;
        }
    }
}
