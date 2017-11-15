using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;

namespace Logrila.Logging.SerilogToLogrila
{
    public static class LoggerConfigurationsLogrilaExtensions
    {
        private const string DefaultOutputTemplate = "{Message}";

        public static LoggerConfiguration Logrila(
            this LoggerSinkConfiguration loggerConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = DefaultOutputTemplate,
            IFormatProvider formatProvider = null)
        {
            var textFormatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            return loggerConfiguration.Sink(new LogrilaSink(textFormatter), restrictedToMinimumLevel);
        }
    }
}
