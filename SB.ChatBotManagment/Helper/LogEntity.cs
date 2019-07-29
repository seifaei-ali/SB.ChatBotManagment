using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ChatBotManagment.Helper
{
    public class LogEntity
    {
        public delegate void LogEventDelegate(LogEntity logEntity);

        public Exception Exception { get; set; }

        public string Message { get; set; }

        public DateTime LogTime { get; set; }

        public object Data { get; set; }

        public bool IsException => this.Exception != null;

        public static LogEntity GetLogEntity(string msg, object data = null, Exception exception = null)
        {
            return new LogEntity() { Exception = exception, Data = data, LogTime = DateTime.Now, Message = msg};
        }

        public static LogEntity GetLogEntity(Exception exception)
        {
            return new LogEntity() { Exception = exception, Data = null, LogTime = DateTime.Now, Message = exception.Message};
        }

    }
}
