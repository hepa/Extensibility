using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ExtensibilityDLL.Modules.Log
{
    public static class Logger
    {
        private static List<Log> LogModules;

        static Logger()
        {
            LogModules = new List<Log>(Extensibility.GetNewInstances<Log>());
        }

        public static void Trace(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            foreach (var logModule in LogModules)
            {
                logModule.Trace(message, file, method, line);
            }
        }
    }
}