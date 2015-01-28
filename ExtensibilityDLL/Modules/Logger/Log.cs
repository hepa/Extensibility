using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ExtensibilityDLL.Modules.Log
{
    public abstract class Log : IModule, ILog
    {
        public abstract string Name { get; }

        public virtual string Icon
        {
            get
            {
                return "pack://application:,,,/ExtensibilityDLL;component/Images/dll.gif";
            }
        }

        public virtual string Developer
        {
            get
            {
                var company = GetType().Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);

                if (company.Length != 0)
                {
                    return ((AssemblyCompanyAttribute)company[0]).Company;
                }

                return null;
            }
        }

        public virtual Version Version
        {
            get
            {
                var version = GetType().Assembly.GetCustomAttributes(typeof(AssemblyVersionAttribute), true);

                if (version.Length != 0)
                {
                    return Version.Parse(((AssemblyVersionAttribute)version[0]).Version);
                }

                return new Version(1, 0);
            }
        }


        public abstract void Trace(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0);
        public abstract void Debug(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0);
        public abstract void Info(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0);
        public abstract void Warn(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0);
        public abstract void Error(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0);
        public abstract void Fatal(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0);
    }
}