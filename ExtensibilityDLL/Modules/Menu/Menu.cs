using System;
using System.Windows.Controls;

namespace ExtensibilityDLL.Modules.Menu
{
    public interface Menu : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MenuItem GetMenuItem();
    }
   
}