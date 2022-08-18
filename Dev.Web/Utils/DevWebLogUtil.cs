using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Web.Utils
{
    public class DevWebLogUtil
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 注册日志配置文件。
        /// </summary>
        public static void RegisterConfig()
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + @"\Configs\NLog.config";
            LogManager.Configuration = new XmlLoggingConfiguration(configPath);
        }

        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="operation">action</param>
        /// <param name="message">消息</param>
        /// <param name="account">操作者</param>
        /// <param name="realName">真实姓名</param>
        public static void Write(LogLevel level, string operation, string message, string account, string realName)
        {
            logger.Log(LogEventInfoInstance(message, level, account, realName));
        }

        /// <summary>
        /// 最常见的记录信息，一般用于普通输出。
        /// </summary>
        /// <param name="message"></param>
        public static void Trace(string message)
        {
            logger.Trace(message);
        }

        /// <summary>
        /// 同样是记录信息，不过出现的频率要比Trace少一些，一般用来调试程序。
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// 信息类型的消息。
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// 警告信息，一般用于比较重要的场合。
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// 错误信息。
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// 致命异常信息。一般来讲，发生致命异常之后程序将无法继续执行。
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }



        private static LogEventInfo LogEventInfoInstance(string message, LogLevel level, string account, string realName)
        {
            LogEventInfo logEvent = new LogEventInfo
            {
                Message = message,
                Level = level
            };
            logEvent.Properties["Account"] = account;
            logEvent.Properties["RealName"] = realName;
 
            return logEvent;
        }
    }
}
