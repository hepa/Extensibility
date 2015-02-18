using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ExtensibilityDLL.Modules.Menu;
using Menu = ExtensibilityDLL.Modules.Menu.Menu;

namespace TestMenuModule
{
    public class TestMenu : Menu
    {
        public string Name
        {
            get { return "TestMenu"; }
        }

        public string Icon { get; private set; }
        public string Developer { get; private set; }
        public Version Version { get; private set; }

        public bool IsEnabled { get; set; }

        public MenuItem GetMenuItem()
        {
            var menuItem = new MenuItem();
            menuItem.Header = Name;
            menuItem.Items.Add(new MenuItem() {Header = "ShowLog"});
            return menuItem;
        }
    }
}
