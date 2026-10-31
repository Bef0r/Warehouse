using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Logging
{
    public class GuiLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly GuiLoggerProvider _provider;

        public GuiLogger(
            string categoryName,
            GuiLoggerProvider provider)
        {
            _categoryName = categoryName;
            _provider = provider;
        }

        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var message = formatter(state, exception);

            if (exception is not null)
            {
                message += $" Exception: {exception.Message}";
            }

            var logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Level = logLevel,
                Message = message
            };

            _provider.AddLog(logEntry);
        }
    }
}
