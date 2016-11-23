using System;

namespace Logrila.Logging.SerilogIntegration
{
    public class SerilogLogger : Logrila.Logging.ILogger
    {
        private readonly Func<string, Serilog.ILogger> _loggerFactory;

        public SerilogLogger()
        {
            _loggerFactory = (name) => new Serilog.LoggerConfiguration().CreateLogger();
        }

        public SerilogLogger(Func<string, Serilog.ILogger> loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public ILog Get(string name)
        {
            return new SerilogLog(_loggerFactory(name), name);
        }

        public static void Use()
        {
            Logrila.Logging.Logger.UseLogger(new SerilogLogger());
        }

        public static void Use(Serilog.ILogger logger)
        {
            Logrila.Logging.Logger.UseLogger(new SerilogLogger(
                (name) => logger.ForContext(Serilog.Core.Constants.SourceContextPropertyName, name)));
        }
    }
}
