using System;

namespace ExtensibilityDLL.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Gets the module name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the icon's URL of the module. 
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Gets the developer of the module.
        /// </summary>
        string Developer { get; }

        /// <summary>
        /// Gets the version of the module.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Indicates whether the module is enabled or not.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}