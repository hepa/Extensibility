using System;

namespace ExtensibilityDLL.Modules.Logger.Implementations.ConsoleLog
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsoleLog : Modules.Logger.Interface.Log
    {
        public override string Name
        {
            get { return "ConsoleLog"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ConsoleLog()
        {                        
            NewMessage += OnNewMessage;
        }        

        private static void OnNewMessage(Entry entry)
        {
            Console.WriteLine(entry);
        }
    }
}