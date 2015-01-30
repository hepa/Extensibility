using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Shapes;
using Path = System.IO.Path;

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

        private void Write(Level level, string message, string file, string method, int line)
        {            
            //TODO need to replace with the Log.Entry class in order to the match the logs at all places.
            Console.WriteLine(string.Format("{0:HH:mm:ss.fff} {1} {2} {3}/{4}():{5} - {6}", DateTime.Now, level.ToString().ToUpper(),
                    Thread.CurrentThread.ManagedThreadId, Path.GetFileName(file), method, line, message));
        }

        public override void Trace(string message, string file, string method, int line)
        {
            Write(Level.Trace,message, file, method, line);
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