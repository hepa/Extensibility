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

        public ConsoleLog()
        {
            NewMessage += o => { };
        }

        public void Write(Entry e)
        {
            
        }
      
    }
}