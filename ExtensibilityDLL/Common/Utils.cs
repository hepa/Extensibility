using System;
using System.IO;
using System.Reflection;

namespace ExtensibilityDLL.Common
{
    /// <summary>
    /// Provides general helper methods throughout the project.
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Gets the size of the file in human-readable format.
        /// </summary>
        /// <param name="bytes">The size.</param>
        /// <returns>Transformed file size.</returns>
        public static string GetFileSize(long bytes)
        {
            var size = "0 bytes";

            if (bytes >= 1099511627776.0)
            {
                size = String.Format("{0:0.00}", bytes / 1099511627776.0) + " TB";
            }
            else if (bytes >= 1073741824.0)
            {
                size = String.Format("{0:0.00}", bytes / 1073741824.0) + " GB";
            }
            else if (bytes >= 1048576.0)
            {
                size = String.Format("{0:0.00}", bytes / 1048576.0) + " MB";
            }
            else if (bytes >= 1024.0)
            {
                size = String.Format("{0:0.00}", bytes / 1024.0) + " kB";
            }
            else if (bytes > 0 && bytes < 1024.0)
            {
                size = bytes + " bytes";
            }

            return size;
        }

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