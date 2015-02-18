using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ExtensibilityDLL.Modules.Logger
{
    public static class Logger
    {
        public static List<Interface.Log> LogModules;

        static Logger()
        {
            LogModules = new List<Interface.Log>(Extensibility.GetNewInstances<Interface.Log>());
        }

        public static void Trace(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            // creating an entry with level.Trace
            foreach (var logModule in LogModules)
            {
                logModule.Trace(message);
            }
        }

        public static void Debug(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            // creating an entry with level.Trace
            foreach (var logModule in LogModules)
            {
                logModule.Debug(message);
            }
        }

        public static void Fatal(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            // creating an entry with level.Trace
            foreach (var logModule in LogModules)
            {
                logModule.Fatal(message);
            }
        }

        private static void Write(Modules.Logger.Interface.Log.Entry entry)
        {
            foreach (var logModule in LogModules)
            {
                
            }
        }
    }
}