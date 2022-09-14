using NLog;

namespace Framework.Service.IService
{
    public interface ILoggerService
    {
        void FileWrite(LogLevel level, string message, string account, string realName);
        void FileTrace(string message);
        void FileDebug(string message);
        void FileInfo(string message);
        void FileWarn(string message);
        void FileError(string message);
        void FileFatal(string message);


        void DbWrite(LogLevel level, string message, string account, string realName);
        void DbTrace(string message);
        void DbDebug(string message); 
        void DbInfo(string message);
        void DbWarn(string message);
        void DbError(string message);
        void DbFatal(string message);


        void ConsoleWrite(LogLevel level, string message, string account, string realName);
        void ConsoleTrace(string message);
        void ConsoleDebug(string message);
        void ConsoleInfo(string message);
        void ConsoleWarn(string message);
        void ConsoleError(string message);
        void ConsoleFatal(string message);
    }
}
