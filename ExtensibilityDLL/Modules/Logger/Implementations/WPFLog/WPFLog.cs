using System;
using System.Threading;
using ExtensibilityDLL.Modules.Logger.Interface;
using WPFLog;

namespace ExtensibilityDLL.Modules.Logger.Implementations.WPFLog
{
    /// <summary>
    /// 
    /// </summary>
    public class WPFLog : Log
    {
        public LoggingWindow LoggingWindow;

        public override string Name
        {
            get { return "WPFLog"; }
        }

        public WPFLog()
        {                        
            LoggingWindow = new LoggingWindow(this);
        }        
    }
}