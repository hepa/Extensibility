using System.IO;
using System.Reflection;

namespace ExtensibilityDLL
{
    /// <summary>
    /// Provides general helper methods.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Gets the install path for the actual application.
        /// </summary>
        /// <returns></returns>
        public static string InstallPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar;
        }
    }
}