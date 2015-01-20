using System;
using System.Reflection;

namespace ExtensibilityDLL.Modules
{
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
    }

    /// <summary>
    /// Represents a module.
    /// </summary>
    public abstract class AbstractModule : IModule
    {
        /// <summary>
        /// Gets the module name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the icon's URL of the module. 
        /// </summary>
        public virtual string Icon
        {
            get
            {
                return "pack://application:,,,/Extensibility;component/Images/dll.gif";   
            }            
        }

        /// <summary>
        /// Gets the developer of the module.
        /// </summary>
        public virtual string Developer
        {
            get
            {
                var company = GetType().Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);

                if (company.Length != 0)
                {
                    return ((AssemblyCompanyAttribute)company[0]).Company;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the version of the module.
        /// </summary>
        public virtual Version Version
        {
            get
            {
                var version = GetType().Assembly.GetCustomAttributes(typeof(AssemblyVersionAttribute), true);

                if (version.Length != 0)
                {
                    return Version.Parse(((AssemblyVersionAttribute)version[0]).Version);
                }

                return new Version(1, 0);
            }
        }
    }
}