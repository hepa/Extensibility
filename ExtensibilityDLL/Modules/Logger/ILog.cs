using System.Runtime.CompilerServices;

namespace ExtensibilityDLL.Modules.Log
{
    public interface ILog
    {
        void Trace(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0); 
        void Debug(string message, string file = "", string method = "", int line = 0);
        void Info(string message, string file = "", string method = "", int line = 0);
        void Warn(string message, string file = "", string method = "", int line = 0);
        void Error(string message, string file = "", string method = "", int line = 0);
        void Fatal(string message, string file = "", string method = "", int line = 0);
    }
}