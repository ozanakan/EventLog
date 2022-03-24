using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEventLog
{
    class Logger
    {
        public void Log(Exception e)
        {
            var exception = e as SqlException;
            SqlError(e.Message, exception.Errors);
        }
        private static void AddLog(string logMessage)
        {
            if (!EventLog.SourceExists("Sample"))
            {
                EventLog.CreateEventSource("Sample", "Sample");
            }
            var log = new EventLog();
            log.Source = "Sample";
            log.WriteEntry(logMessage, EventLogEntryType.Error);
        }

        private static void SqlError(string logMessage, SqlErrorCollection sqlErrors)
        {
            var sb = new StringBuilder();
            foreach (SqlError item in sqlErrors)
            {
                sb.AppendLine("Hata Tarihi: ");
                sb.Append(DateTime.Now);
                sb.Append(Environment.NewLine);
                sb.AppendLine("Satır Numarası: ");
                sb.Append(item.LineNumber);
                sb.Append(Environment.NewLine);
                sb.AppendLine("Mesaj: ");
                sb.Append(item.Message);

                AddLog(sb.ToString());
            }
        }
    }
}
