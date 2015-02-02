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