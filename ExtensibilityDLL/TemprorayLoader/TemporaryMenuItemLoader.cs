﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace ExtensibilityDLL.TemprorayLoader
{
    /// <summary>
    /// Deprecated as hell.
    /// </summary>
    public static class TemporaryMenuItemLoader
    {
        public static void LoadMenuItems(Menu menu)
        {            
            foreach (var newInstance in Extensibility.GetNewInstances<Modules.Menu.Menu>())
            {
                Console.WriteLine(newInstance.ToString());
                menu.Items.Add(newInstance.GetMenuItem());                
            }            
        }
    }
}