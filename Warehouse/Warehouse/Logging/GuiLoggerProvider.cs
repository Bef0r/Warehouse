using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Warehouse.Logging
{
    public class GuiLoggerProvider : ILoggerProvider
    {
        public int MaxLogsCount { get; set; }
        public ObservableCollection<LogEntry> Logs = new ObservableCollection<LogEntry>();

        public GuiLoggerProvider(IConfiguration configuration)
        {
            MaxLogsCount = configuration.GetSection("Logging").GetValue<int>("MaxLogsCount");
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new GuiLogger(categoryName, this);
        }
        public void AddLog(LogEntry logEntry)
        {
            Logs.Add(logEntry);

            while (Logs.Count > MaxLogsCount)
            {
                Logs.RemoveAt(0);
            }
        }


        public void ClearLogs()
        {
            Logs.Clear();
        }

        public void Dispose()
        {
            Logs.Clear();
        }
    }
}
