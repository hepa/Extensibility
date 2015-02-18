using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ExtensibilityDLL.Common;

namespace ExtensibilityDLL.Modules.Logger.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Log : IModule
    {
        #region IModule
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

        public bool IsEnabled { get; set; }
        #endregion        

        /// <summary>
        /// The current logging level.
        /// </summary>
        public volatile Level LoggingLevel = SystemDefaults.DEFAULT_LOGGGING_LEVEL;

        /// <summary>
        /// Contains a value indicating whether trace level messages are currently enabled.
        /// </summary>
        public volatile bool IsTraceEnabled = SystemDefaults.DEFAULT_LOGGGING_LEVEL >= Level.Trace;

        /// <summary>
        /// Contains a value indicating whether debug level messages are currently enabled.
        /// </summary>
        public volatile bool IsDebugEnabled = SystemDefaults.DEFAULT_LOGGGING_LEVEL >= Level.Debug;

        /// <summary>
        /// Contains a value indicating whether info level messages are currently enabled.
        /// </summary>
        public volatile bool IsInfoEnabled = SystemDefaults.DEFAULT_LOGGGING_LEVEL >= Level.Info;

        /// <summary>
        /// Contains a value indicating whether warn level messages are currently enabled.
        /// </summary>
        public volatile bool IsWarnEnabled = SystemDefaults.DEFAULT_LOGGGING_LEVEL >= Level.Warn;

        /// <summary>
        /// Contains a value indicating whether error level messages are currently enabled.
        /// </summary>
        public volatile bool IsErrorEnabled = SystemDefaults.DEFAULT_LOGGGING_LEVEL >= Level.Error;

        /// <summary>
        /// Contains a value indicating whether fatal level messages are currently enabled.
        /// </summary>
        public volatile bool IsFatalEnabled = SystemDefaults.DEFAULT_LOGGGING_LEVEL >= Level.Fatal;

        /// <summary>
        /// Occurs when a new message was added to the log.
        /// </summary>
        public event Action<Entry> NewMessage;

        /// <summary>
        /// Occurs when the logging level was changed.
        /// </summary>
        public event Action<Level> ChangedLoggingLevel;

        /// <summary>
        /// The message container.
        /// </summary>
        public readonly ConcurrentBag<Entry> Messages = new ConcurrentBag<Entry>();

        /// <summary>
        /// Sets the debugging level.
        /// </summary>
        /// <param name="level">The level.</param>
        public void SetLevel(Level level)
        {
            LoggingLevel = level;

            IsTraceEnabled = level >= Level.Trace;
            IsDebugEnabled = level >= Level.Debug;
            IsInfoEnabled = level >= Level.Info;
            IsWarnEnabled = level >= Level.Warn;
            IsErrorEnabled = level >= Level.Error;
            IsFatalEnabled = level >= Level.Fatal;

            if (ChangedLoggingLevel != null)
            {
                //Task.Factory.StartNew(ChangedLoggingLevel, level);
                ChangedLoggingLevel(level);
            }
        }

        /// <summary>
        /// Represents a log entry object.
        /// </summary>
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
            public readonly Level Level;

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
            /// Initializes a new instance of the <see cref="Log.Entry" /> class.
            /// </summary>
            /// <param name="time">The time when the logged message occurred.</param>
            /// <param name="level">The weight of the logged message.</param>
            /// <param name="file">The file where the logged message occurred.</param>
            /// <param name="method">The method in which the logged message occurred.</param>
            /// <param name="line">The line where the logged message occurred.</param>
            /// <param name="message">The logged message.</param>
            public Entry(DateTime time, Level level, string file, string method, int line, string message)
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// Writes the diagnostic message at Trace level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="file">The file where this message originates from.</param>
        /// <param name="method">The method where this message originates from.</param>
        /// <param name="line">The line where this message originates from.</param>
        public virtual void Trace(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            if (IsTraceEnabled) Write(Level.Trace, message, file, method, line);
        }

        /// <summary>
        /// Writes the diagnostic message at Debug level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="file">The file where this message originates from.</param>
        /// <param name="method">The method where this message originates from.</param>
        /// <param name="line">The line where this message originates from.</param>
        public virtual void Debug(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            if (IsDebugEnabled) Write(Level.Debug, message, file, method, line);
        }

        /// <summary>
        /// Writes the diagnostic message at Info level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="file">The file where this message originates from.</param>
        /// <param name="method">The method where this message originates from.</param>
        /// <param name="line">The line where this message originates from.</param>
        public virtual void Info(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            if (IsInfoEnabled) Write(Level.Info, message, file, method, line);
        }

        /// <summary>
        /// Writes the diagnostic message at Warn level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="file">The file where this message originates from.</param>
        /// <param name="method">The method where this message originates from.</param>
        /// <param name="line">The line where this message originates from.</param>
        public virtual void Warn(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            if (IsWarnEnabled) Write(Level.Warn, message, file, method, line);
        }

        /// <summary>
        /// Writes the diagnostic message at Error level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="file">The file where this message originates from.</param>
        /// <param name="method">The method where this message originates from.</param>
        /// <param name="line">The line where this message originates from.</param>
        public virtual void Error(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            if (IsErrorEnabled) Write(Level.Error, message, file, method, line);
        }

        /// <summary>
        /// Writes the diagnostic message at Fatal level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="file">The file where this message originates from.</param>
        /// <param name="method">The method where this message originates from.</param>
        /// <param name="line">The line where this message originates from.</param>
        public virtual void Fatal(string message, [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int line = 0)
        {
            if (IsFatalEnabled) Write(Level.Fatal, message, file, method, line);
        }

        /// <summary>
        /// Writes the specified diagnostic message to the log.
        /// </summary>
        /// <param name="level">The weight of the message.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="file">The file where this message originates from.</param>
        /// <param name="method">The method where this message originates from.</param>
        /// <param name="line">The line where this message originates from.</param>
        private void Write(Level level, string message, string file, string method, int line)
        {
            var log = new Entry(DateTime.Now, level, file, method, line, message);

            Messages.Add(log);

            if (NewMessage != null)
            {
                //Task.Factory.StartNew(NewMessage, log);
                NewMessage(log);
                //ThreadPool.QueueUserWorkItem(NewMessage, log);
                //new Thread(new ParameterizedThreadStart(NewMessage)).Start(log);
            }
        }
    }
}