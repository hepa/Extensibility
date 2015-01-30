using System;
using System.IO;
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

        [Flags]
        public enum Level : int
        {
            None = 0,
            Fatal = 1,
            Error = 1 << 1,
            Warn = 1 << 2,
            Info = 1 << 3,
            Debug = 1 << 4,
            Trace = 1 << 5
        }

        public class Entry
        {
            /// <summary>
            /// Gets or sets the time when the logged message occurred.
            /// </summary>
            /// <value>
            /// The time when the logged message occurred.
            /// </value>
            public readonly DateTime Time;

            /// <summary>
            /// Gets or sets the weight of the logged message.
            /// </summary>
            /// <value>
            /// The weight of the logged message.
            /// </value>
            public readonly WPFLog.Log.Level Level;

            /// <summary>
            /// Gets or sets the file where the logged message occurred.
            /// </summary>
            /// <value>
            /// The file where the logged message occurred.
            /// </value>
            public readonly string File;

            /// <summary>
            /// Gets or sets the method in which the logged message occurred.
            /// </summary>
            /// <value>
            /// The method in which the logged message occurred.
            /// </value>
            public readonly string Method;

            /// <summary>
            /// Gets or sets the line where the logged message occurred.
            /// </summary>
            /// <value>
            /// The line where the logged message occurred.
            /// </value>
            public readonly int Line;

            /// <summary>
            /// Gets or sets the logged message.
            /// </summary>
            /// <value>
            /// The logged message.
            /// </value>
            public readonly string Message;

            /// <summary>
            /// Gets or sets the managed ID of the thread in the thread pool.
            /// </summary>
            /// <value>
            /// The managed ID of the thread in the thread pool.
            /// </value>
            public readonly int Thread;

            /// <summary>
            /// Initializes a new instance of the <see cref="WPFLog.Log.Entry" /> class.
            /// </summary>
            /// <param name="time">The time when the logged message occurred.</param>
            /// <param name="level">The weight of the logged message.</param>
            /// <param name="file">The file where the logged message occurred.</param>
            /// <param name="method">The method in which the logged message occurred.</param>
            /// <param name="line">The line where the logged message occurred.</param>
            /// <param name="message">The logged message.</param>
            public Entry(DateTime time, WPFLog.Log.Level level, string file, string method, int line, string message)
            {
                Time = time;
                Level = level;
                File = file;
                Method = method;
                Line = line;
                Message = message;
                Thread = System.Threading.Thread.CurrentThread.ManagedThreadId;
            }

            /// <summary>
            /// Returns a <see cref="System.String" /> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String" /> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return string.Format("{0:HH:mm:ss.fff} {1} {2} {3}/{4}():{5} - {6}", Time, Level.ToString().ToUpper(),
                    Thread, Path.GetFileName(File), Method, Line, Message);
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