using System;
using System.Windows.Controls;

namespace ExtensibilityDLL.Modules.Menu
{
    public interface IMenu : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MenuItem GetMenuItem();
    }
   
}