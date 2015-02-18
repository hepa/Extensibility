using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ExtensibilityDLL.Common;
using Microsoft.Scripting;
using TaskDialogInterop;
using Module = ExtensibilityDLL.Modules.IModule;

namespace ExtensibilityDLL
{
    /// <summary>
    /// Provides support for loading in external classes.
    /// </summary>
    public static class Extensibility
    {
        /// <summary>
        /// Represents the searching pattern for external module names.
        /// </summary>
        private const string ExternalModuleFileNamePattern = "*Module*";

        /// <summary>
        /// Gets or sets the internal module types.
        /// </summary>
        public static List<Type> InternalModules { get; set; }

        /// <summary>
        /// Gets or sets the external module types.
        /// </summary>
        public static List<Type> ExternalModules { get; set; }

        /// <summary>
        /// Initializes the <see cref="Extensibility"/> class.
        /// </summary>
        static Extensibility()
        {            
            LoadModules();            
        }

        /// <summary>
        /// Loads in the available modules, including internals and externals as well.
        /// </summary>
        private static void LoadModules()
        {
            LoadInternalModules();
            LoadExternalModules();
            ResolveConflicts();
        }

        /// <summary>
        /// Loads in the available internal modules.
        /// </summary>
        private static void LoadInternalModules()
        {
            InternalModules = new List<Type>(GetDerivedTypesFromAssembly<Module>(Assembly.GetExecutingAssembly()));
        }

        /// <summary>
        /// Loads in the available external modules.
        /// </summary>
        private static void LoadExternalModules()
        {
            ExternalModules = new List<Type>();
            var installPath = Utils.InstallPath();

            var modules = Directory.GetFiles(installPath, ExternalModuleFileNamePattern);

            foreach (var module in modules.Where(m => m.EndsWith(".exe")))
            {
                try
                {
                    var asm = Assembly.LoadFile(module);
                    var derivedTypesFromAssembly = GetDerivedTypesFromAssembly<Module>(asm);

                    ExternalModules.AddRange(derivedTypesFromAssembly);
                }
                catch (Exception ex)
                {
                    HandleLoadException(module, ex);
                }
            }
        }

        /// <summary>
        /// Resolves the conflist during module load in. 
        /// </summary>
        private static void ResolveConflicts()
        {
            var removeableInternal = new List<Type>();
            var removeableExternal = new List<Type>();

            foreach (var internalModule in InternalModules)
            {
                foreach (var externalModule in ExternalModules)
                {
                    if (internalModule.Name == externalModule.Name)
                    {
                        var intModInst = (Module)Activator.CreateInstance(internalModule);
                        var extModInst = (Module)Activator.CreateInstance(externalModule);

                        if (intModInst.Version > extModInst.Version)
                        {
                            removeableExternal.Add(externalModule);    
                        }
                        else
                        {
                            removeableInternal.Add(internalModule);
                        }
                    }
                }
            }

            foreach (var iplugin in removeableInternal)
            {
                InternalModules.Remove(iplugin);
            }

            foreach (var eplugin in removeableExternal)
            {
                ExternalModules.Remove(eplugin);
            }
        }

        /// <summary>
        /// Gets the derived types from the specified assembly for the specified type.
        /// </summary>
        /// <typeparam name="T">The class whose derived types are needed.</typeparam>
        /// <param name="assembly">The assembly to search.</param>
        /// <returns>
        /// List of derived classes.
        /// </returns>
        public static IEnumerable<Type> GetDerivedTypesFromAssembly<T>(Assembly assembly)
        {
            var parent = typeof (T);

            return assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract && parent.IsAssignableFrom(type));
        }

        /// <summary>
        /// Gets the derived types from both internal and external sources for the specified type.
        /// </summary>
        /// <typeparam name="T">The class whose derived types are needed.</typeparam>        
        /// <param name="includeInternal">if set to <c>true</c> the internal assembly will be included in the search.</param>
        /// <param name="includeExternal">if set to <c>true</c> the external assemblies will be included in the search.</param>
        /// <returns>
        /// List of derived classes.
        /// </returns>
        public static IEnumerable<Type> GetDerivedTypes<T>(bool includeInternal = true, bool includeExternal = true)
        {
            var parent = typeof(T);            

            if (includeInternal)
            {
                foreach (var type in InternalModules.Where(parent.IsAssignableFrom))
                {
                    yield return type;
                }
            }

            if (includeExternal)
            {                
                foreach (var type in ExternalModules.Where(parent.IsAssignableFrom))
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// Gets the derived types from both internal and external sources for the specified type and create a new instance for each.
        /// </summary>
        /// <typeparam name="T">The class whose derived types are needed.</typeparam>
        /// <param name="includeInternal">if set to <c>true</c> the internal assembly will be included in the search.</param>
        /// <param name="includeExternal">if set to <c>true</c> the external assemblies will be included in the search.</param>
        /// <returns>
        /// List of derived instantiated classes.
        /// </returns>
        public static IEnumerable<T> GetNewInstances<T>(bool includeInternal = true, bool includeExternal = true)
        {
            return GetDerivedTypes<T>(includeInternal, includeExternal).Select(type => (T)Activator.CreateInstance(type));
        }

        /// <summary>
        /// Handles the exception which occurred while loading a module.
        /// </summary>
        /// <param name="file">The module file.</param>
        /// <param name="ex">The thrown exception.</param>
        private static void HandleLoadException(string file, Exception ex)
        {
            var exception = ex as SyntaxErrorException;
            if (exception != null)
            {
                var exp = exception;
                TaskDialog.Show(new TaskDialogOptions
                {
                    MainIcon = VistaTaskDialogIcon.Error,
                    Title = "Failed to parse the module",
                    MainInstruction = "Failed to parse the module",
                    Content =
                        String.Format("Syntax error in {0} line {1} column {2}:\r\n\r\n{3}", Path.GetFileName(file),
                            exp.Line, exp.Column, exception.Message),
                    CustomButtons = new[] {"OK"}
                });
            }
            else
            {
                var sb = new StringBuilder();
            parseException:
                sb.AppendLine(ex.GetType() + ": " + ex.Message);
                sb.AppendLine(ex.StackTrace);

                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    goto parseException;
                }

                TaskDialog.Show(new TaskDialogOptions
                {
                    MainIcon = VistaTaskDialogIcon.Error,
                    Title = "Failed to load the module",
                    MainInstruction = "Failed to load the module",
                    Content = String.Format("An exception of type {0} was thrown while trying to load plugin {1}.",
                            ex.GetType().ToString().Replace("System.", string.Empty), Path.GetFileName(file)),
                    ExpandedInfo = sb.ToString(),
                    CustomButtons = new[] {"OK"}
                });
            }
        }
    }
}