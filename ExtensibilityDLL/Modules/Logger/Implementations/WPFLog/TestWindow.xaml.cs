using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ExtensibilityDLL.Common;
using ExtensibilityDLL.Modules.Logger.Interface;
using WPFLog;
using Control = System.Windows.Forms.Control;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace ExtensibilityDLL.Modules.Logger.Implementations.WPFLog
{
    /// <summary>
    /// Interaction logic for LoggingWindow.xaml
    /// </summary>
    public partial class TestWindow
    {
        private Log log;

        public TestWindow()
        {
        }

        public TestWindow(Log log)
        {
            this.log = log;
        }
    }
}
