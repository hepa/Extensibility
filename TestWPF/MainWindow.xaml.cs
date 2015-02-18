using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExtensibilityDLL;
using ExtensibilityDLL.Modules.Logger;
using ExtensibilityDLL.Modules.Logger.Implementations.WPFLog;
using ExtensibilityDLL.Modules.Logger.Interface;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ExtensibilityDLL.TemprorayLoader.TemporaryMenuItemLoader.LoadMenuItems(Menu);

            var e = ExtensibilityDLL.Modules.Logger.Logger.LogModules;

            int i = 0;
            while (true)
            {
                if (i % 2 ==0)
                    ExtensibilityDLL.Modules.Logger.Logger.Trace("Trace message");
                else
                    ExtensibilityDLL.Modules.Logger.Logger.Fatal("Fatal message");

                if (i == 5)
                {                    
                }

                i++;
                Thread.Sleep(1500);
            }            
        }       
    }
}
