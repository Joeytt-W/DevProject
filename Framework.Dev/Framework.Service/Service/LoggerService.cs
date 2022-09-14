using Framework.Service.IService;
using NLog;
using NLog.Web;
using System;

namespace Framework.Service.Service
{
    public class LoggerService:ILoggerService
    {
        private readonly ILogger fileLogger;
        private readonly ILogger dbLogger;
        private readonly ILogger consoleLogger;


        public LoggerService()
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + @"/Configs/NLog.config";
            LogManager.LoadConfiguration(configPath);

            fileLogger = LogManager.GetLogger("ToFileLog");
            dbLogger = LogManager.GetLogger("ToDBLog");
            consoleLogger = LogManager.GetLogger("ToCwLog");
        }

        #region filelog
        public void FileWrite(LogLevel level, string message, string account, string realName)
        {
            fileLogger.Log(LogEventInfoInstance(message, level, account, realName));
        }

        public void FileTrace(string message)
        {
            fileLogger.Trace(message);
        }

        public void FileDebug(string message)
        {
            fileLogger.Debug(message);
        }

        public void FileInfo(string message)
        {
            fileLogger.Info(message);
        }

        public void FileWarn(string message)
        {
            fileLogger.Warn(message);
        }

        public void FileError(string message)
        {
            fileLogger.Error(message);
        }

        public void FileFatal(string message)
        {
            fileLogger.Fatal(message);
        }

        #endregion

        #region dblog
        public void DbWrite(LogLevel level, string message, string account, string realName)
        {
            dbLogger.Log(LogEventInfoInstance(message, level, account, realName));
        }

        public void DbTrace(string message)
        {
            dbLogger.Trace(message);
        }

        public void DbDebug(string message)
        {
            dbLogger.Debug(message);
        }
        public void DbInfo(string message)
        {
            dbLogger.Info(message);
        }

        public void DbWarn(string message)
        {
            dbLogger.Warn(message);
        }

        public void DbError(string message)
        {
            dbLogger.Error(message);
        }

        public void DbFatal(string message)
        {
            dbLogger.Fatal(message);
        }

        #endregion

        #region consolelog
        public void ConsoleWrite(LogLevel level, string message, string account, string realName)
        {
            consoleLogger.Log(LogEventInfoInstance(message, level, account, realName));
        }

        public void ConsoleTrace(string message)
        {
            consoleLogger.Trace(message);
        }

        public void ConsoleDebug(string message)
        {
            consoleLogger.Debug(message);
        }
        public void ConsoleInfo(string message)
        {
            consoleLogger.Info(message);
        }

        public void ConsoleWarn(string message)
        {
            consoleLogger.Warn(message);
        }

        public void ConsoleError(string message)
        {
            consoleLogger.Error(message);
        }

        public void ConsoleFatal(string message)
        {
            consoleLogger.Fatal(message);
        }
        #endregion



        private static LogEventInfo LogEventInfoInstance(string message, LogLevel level, string account, string realName)
        {
            LogEventInfo logEvent = new LogEventInfo
            {
                Message = message,
                Level = level
            };
            logEvent.Properties["Account"] = account;
            logEvent.Properties["RealName"] = realName;
            logEvent.Properties["Browser"] = "";
            return logEvent;
        }

        
    }
}
