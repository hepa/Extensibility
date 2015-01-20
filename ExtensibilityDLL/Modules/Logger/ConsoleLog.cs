using System;
using System.IO;
using System.Threading;

namespace ExtensibilityDLL.Modules.Log
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsoleLog : Log
    {
        public override string Name
        {
            get { return "ConsoleLog"; }
        }       

        public override void Trace(string message, string file, string method, int line)
        {
            Console.WriteLine(string.Format("{0:HH:mm:ss.fff} {1} {2} {3}/{4}():{5} - {6}", DateTime.Now, "TRACE",
                    Thread.CurrentThread.ManagedThreadId, Path.GetFileName(file), method, line, message));
        }

        public override void Debug(string message, string file, string method, int line)
        {
            throw new System.NotImplementedException();
        }

        public override void Info(string message, string file, string method, int line)
        {
            throw new System.NotImplementedException();
        }

        public override void Warn(string message, string file, string method, int line)
        {
            throw new System.NotImplementedException();
        }

        public override void Error(string message, string file, string method, int line)
        {
            throw new System.NotImplementedException();
        }

        public override void Fatal(string message, string file, string method, int line)
        {
            throw new System.NotImplementedException();
        }
    }
}