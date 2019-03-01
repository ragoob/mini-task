using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Core.Plugins
{
   public class PluginManager
    {
        #region Fields

        private static readonly ITaskFileProvider _fileProvider;
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        private static readonly List<string> _baseAppLibraries;
        private static string _shadowCopyFolder;

        #endregion

        #region Ctor

        static PluginManager()
        {
            try
            {
                _fileProvider = EngineContext.DefaultFileProvider;
            }
            catch (Exception)
            {
                


            }

            //get all libraries from /bin/{version}/ directory
            _baseAppLibraries = _fileProvider.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(fi => _fileProvider.GetFileName(fi)).ToList();

            //get all libraries from base site directory
            if (!AppDomain.CurrentDomain.BaseDirectory.Equals(Environment.CurrentDirectory, StringComparison.InvariantCultureIgnoreCase))
                _baseAppLibraries.AddRange(_fileProvider.GetFiles(Environment.CurrentDirectory, "*.dll").Select(fi => _fileProvider.GetFileName(fi)));

          
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get description files
        /// </summary>
        /// <param name="pluginFolder">Plugin directory info</param>
        /// <returns>Original and parsed description files</returns>
        private static IEnumerable<KeyValuePair<string, PluginInfoParser>> GetDescriptionFilesAndDescriptors(string pluginFolder)
        {
            if (pluginFolder == null)
                throw new ArgumentNullException(nameof(pluginFolder));

            //create list (<file info, parsed plugin descritor>)
            var result = new List<KeyValuePair<string, PluginInfoParser>>();

            //add display order and path to list
            foreach (var descriptionFile in _fileProvider.GetFiles(pluginFolder, PluginDefaults.DescriptionFileName, false))
            {
               
                //parse file
                var pluginDescriptor = GetPluginInfoParserFromFile(descriptionFile);

                //populate list
                result.Add(new KeyValuePair<string, PluginInfoParser>(descriptionFile, pluginDescriptor));
            }
            return result;
        }

        /// <summary>
        /// Get system names of installed plugins
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>List of plugin system names</returns>
        private static IList<string> GetInstalledPluginNames(string filePath)
        {
            //check whether file exists
            if (!_fileProvider.FileExists(filePath))
            {
            

                //get plugin system names from the old txt file
                var pluginSystemNames = new List<string>();
                using (var reader = new StringReader(_fileProvider.ReadAllText(filePath, Encoding.UTF8)))
                {
                    string pluginName;
                    while ((pluginName = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(pluginName))
                            pluginSystemNames.Add(pluginName.Trim());
                    }
                }

                //save system names of installed plugins to the new file
                SaveInstalledPluginNames(pluginSystemNames, _fileProvider.MapPath(PluginDefaults.InstalledPluginsFilePath));

                //and delete the old one
                _fileProvider.DeleteFile(filePath);

                return pluginSystemNames;
            }

            var text = _fileProvider.ReadAllText(filePath, Encoding.UTF8);
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            //get plugin system names from the JSON file
            return JsonConvert.DeserializeObject<IList<string>>(text);
        }

        /// <summary>
        /// Save system names of installed plugins to the file
        /// </summary>
        /// <param name="pluginSystemNames">List of plugin system names</param>
        /// <param name="filePath">Path to the file</param>
        private static void SaveInstalledPluginNames(IList<string> pluginSystemNames, string filePath)
        {
            //save the file
            var text = JsonConvert.SerializeObject(pluginSystemNames, Formatting.Indented);
            _fileProvider.WriteAllText(filePath, text, Encoding.UTF8);
        }

        /// <summary>
        /// Indicates whether assembly file is already loaded
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>True if assembly file is already loaded; otherwise false</returns>
        private static bool IsAlreadyLoaded(string filePath)
        {
            
            if (_baseAppLibraries.Any(sli => sli.Equals(_fileProvider.GetFileName(filePath), StringComparison.InvariantCultureIgnoreCase)))
                return true;

           
            try
            {
                var fileNameWithoutExt = _fileProvider.GetFileNameWithoutExtension(filePath);
                if (string.IsNullOrEmpty(fileNameWithoutExt))
                    throw new Exception($"Cannot get file extension for {_fileProvider.GetFileName(filePath)}");

                foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var assemblyName = a.FullName.Split(',').FirstOrDefault();
                    if (fileNameWithoutExt.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }

            return false;
        }

       
        private static Assembly PerformFileDeploy(string plug, ApplicationPartManager applicationPartManager, string shadowCopyPath = "")
        {
            var parent = string.IsNullOrEmpty(plug) ? string.Empty : _fileProvider.GetParentDirectory(plug);

            if (string.IsNullOrEmpty(parent))
                throw new InvalidOperationException($"The plugin directory for the {_fileProvider.GetFileName(plug)} file exists in a folder outside of the allowed nopCommerce folder hierarchy");

          
            if (string.IsNullOrEmpty(shadowCopyPath))
                shadowCopyPath = _shadowCopyFolder;

            _fileProvider.CreateDirectory(shadowCopyPath);
            var shadowCopiedPlug = ShadowCopyFile(plug, shadowCopyPath);

            Assembly shadowCopiedAssembly = null;

            try
            {
                shadowCopiedAssembly = RegisterPluginDefinition( applicationPartManager, shadowCopiedPlug);
            }
            catch (FileLoadException)
            {
              
            }

            return shadowCopiedAssembly ?? PerformFileDeploy(plug, applicationPartManager);
        }

       
        private static Assembly RegisterPluginDefinition( ApplicationPartManager applicationPartManager, string plug)
        {
            //we can now register the plugin definition
            Assembly pluginAssembly;
            try
            {
                pluginAssembly = Assembly.LoadFrom(plug);
            }
            catch (FileLoadException)
            {
                pluginAssembly = Assembly.UnsafeLoadFrom(plug);
            }

            Debug.WriteLine("Adding to ApplicationParts: '{0}'", pluginAssembly.FullName);
            applicationPartManager.ApplicationParts.Add(new AssemblyPart(pluginAssembly));

            return pluginAssembly;
        }

        /// <summary>
        /// Copy the plugin file to shadow copy directory
        /// </summary>
        /// <param name="pluginFilePath">Plugin file path</param>
        /// <param name="shadowCopyPlugFolder">Path to shadow copy folder</param>
        /// <returns>File path to shadow copy of plugin file</returns>
        private static string ShadowCopyFile(string pluginFilePath, string shadowCopyPlugFolder)
        {
           
            var shadowCopiedPlug = _fileProvider.Combine(shadowCopyPlugFolder, _fileProvider.GetFileName(pluginFilePath));
            if (_fileProvider.FileExists(shadowCopiedPlug))
            {
                _fileProvider.DeleteFile(shadowCopiedPlug);
            }
            
            try
            {
                _fileProvider.FileCopy(pluginFilePath, shadowCopiedPlug, true);
            }
            catch (IOException)
            {
                Debug.WriteLine("File locked");
              
             
            }

            return shadowCopiedPlug;
        }


        #endregion

        #region Methods

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="applicationPartManager">Application part manager</param>
        /// <param name="config">Config</param>
        public static void Initialize(ApplicationPartManager applicationPartManager)
        {
            if (applicationPartManager == null)
                throw new ArgumentNullException(nameof(applicationPartManager));

            using (new WriteLockDisposable(Locker))
            {
              
                var pluginFolder = _fileProvider.MapPath(PluginDefaults.Path);
                _shadowCopyFolder = _fileProvider.MapPath(PluginDefaults.ShadowCopyPath);

                var referencedPlugins = new List<PluginInfoParser>();
                var incompatiblePlugins = new List<string>();

                try
                {
                    var installedPluginSystemNames = GetInstalledPluginNames(_fileProvider.MapPath(PluginDefaults.InstalledPluginsFilePath));
                    
                    _fileProvider.CreateDirectory(pluginFolder);
                    _fileProvider.CreateDirectory(_shadowCopyFolder);

                    var binFiles = _fileProvider.GetFiles(_shadowCopyFolder, "*", false);
                 
                    //remove all old files in altrnative bin folder
                    foreach (var f in binFiles)
                    {
                        
                        try
                        {
                           
                            var fileName = _fileProvider.GetFileName(f);
                          
                            _fileProvider.DeleteFile(f);
                        }
                        catch (Exception exc)
                        {
                            Debug.WriteLine("Error deleting file " + f + ". Exception: " + exc);
                        }
                    }

                   

                    //load info files
                    foreach (var dfd in GetDescriptionFilesAndDescriptors(pluginFolder))
                    {
                        var descriptionFile = dfd.Key;
                        var pluginDescriptor = dfd.Value;
                        
                        //some validation
                        if (string.IsNullOrWhiteSpace(pluginDescriptor.Name))
                            throw new Exception($"A plugin '{descriptionFile}' has no system name. Try assigning the plugin a unique name and recompiling.");
                        if (referencedPlugins.Contains(pluginDescriptor))
                            throw new Exception($"A plugin with '{pluginDescriptor.Name}' system name is already defined");

                        //set 'Installed' property
                        pluginDescriptor.IsInstalled = installedPluginSystemNames
                            .FirstOrDefault(x => x.Equals(pluginDescriptor.Name, StringComparison.InvariantCultureIgnoreCase)) != null;

                        try
                        {
                            var directoryName = _fileProvider.GetDirectoryName(descriptionFile);
                            if (string.IsNullOrEmpty(directoryName))
                                throw new Exception($"Directory cannot be resolved for '{_fileProvider.GetFileName(descriptionFile)}' description file");

                            //get list of all DLLs in plugins (not in bin!)
                            var pluginFiles = _fileProvider.GetFiles(directoryName, "*.dll", false)
                                .Where(x => !binFiles.Select(q => q).Contains(x))
                                .ToList();

                            //other plugin description info
                            var mainPluginFile = pluginFiles
                                .FirstOrDefault(x => _fileProvider.GetFileName(x).Equals(pluginDescriptor.AssemblyFileName, StringComparison.InvariantCultureIgnoreCase));

                            //plugin have wrong directory
                            if (mainPluginFile == null)
                            {
                                incompatiblePlugins.Add(pluginDescriptor.Name);
                                continue;
                            }

                            pluginDescriptor.AssemblyFileName = mainPluginFile;

                            //shadow copy main plugin file
                            pluginDescriptor.RefrancedPlugin = PerformFileDeploy(mainPluginFile, applicationPartManager);

                            //load all other referenced assemblies now
                            foreach (var plugin in pluginFiles
                                .Where(x => !_fileProvider.GetFileName(x).Equals(_fileProvider.GetFileName(mainPluginFile), StringComparison.InvariantCultureIgnoreCase))
                                .Where(x => !IsAlreadyLoaded(x)))
                                PerformFileDeploy(plugin, applicationPartManager);

                            //init plugin type (only one plugin per assembly is allowed)
                            foreach (var t in pluginDescriptor.RefrancedPlugin.GetTypes())
                                if (typeof(IPlugin).IsAssignableFrom(t))
                                    if (!t.IsInterface)
                                        if (t.IsClass && !t.IsAbstract)
                                        {
                                            pluginDescriptor.Plugin = t;
                                            break;
                                        }

                            referencedPlugins.Add(pluginDescriptor);
                        }
                        catch (ReflectionTypeLoadException ex)
                        {
                            //add a plugin name. this way we can easily identify a problematic plugin
                            var msg = $"Plugin '{pluginDescriptor.FriendlyName}'. ";
                            foreach (var e in ex.LoaderExceptions)
                                msg += e.Message + Environment.NewLine;

                            var fail = new Exception(msg, ex);
                            throw fail;
                        }
                        catch (Exception ex)
                        {
                            //add a plugin name. this way we can easily identify a problematic plugin
                            var msg = $"Plugin '{pluginDescriptor.FriendlyName}'. {ex.Message}";

                            var fail = new Exception(msg, ex);
                            throw fail;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var msg = string.Empty;
                    for (var e = ex; e != null; e = e.InnerException)
                        msg += e.Message + Environment.NewLine;

                    var fail = new Exception(msg, ex);
                    throw fail;
                }

                ReferencedPlugins = referencedPlugins;
                IncompatiblePlugins = incompatiblePlugins;
            }
        }

        /// <summary>
        /// Mark plugin as installed
        /// </summary>
        /// <param name="systemName">Plugin system name</param>
        public static void MarkPluginAsInstalled(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
                throw new ArgumentNullException(nameof(systemName));

            var filePath = _fileProvider.MapPath(PluginDefaults.InstalledPluginsFilePath);

            //create file if not exists
            _fileProvider.CreateFile(filePath);

            //get installed plugin names
            var installedPluginSystemNames = GetInstalledPluginNames(filePath);

            //add plugin system name to the list if doesn't already exist
            var alreadyMarkedAsInstalled = installedPluginSystemNames.Any(pluginName => pluginName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));
            if (!alreadyMarkedAsInstalled)
                installedPluginSystemNames.Add(systemName);

            //save installed plugin names to the file
            SaveInstalledPluginNames(installedPluginSystemNames, filePath);
        }

        /// <summary>
        /// Mark plugin as uninstalled
        /// </summary>
        /// <param name="systemName">Plugin system name</param>
        public static void MarkPluginAsUninstalled(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
                throw new ArgumentNullException(nameof(systemName));

            var filePath = _fileProvider.MapPath(PluginDefaults.InstalledPluginsFilePath);

            //create file if not exists
            _fileProvider.CreateFile(filePath);

            //get installed plugin names
            var installedPluginSystemNames = GetInstalledPluginNames(filePath);

            //remove plugin system name from the list if exists
            var alreadyMarkedAsInstalled = installedPluginSystemNames.Any(pluginName => pluginName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));
            if (alreadyMarkedAsInstalled)
                installedPluginSystemNames.Remove(systemName);

            //save installed plugin names to the file
            SaveInstalledPluginNames(installedPluginSystemNames, filePath);
        }

        /// <summary>
        /// Mark plugin as uninstalled
        /// </summary>
        public static void MarkAllPluginsAsUninstalled()
        {
            var filePath = _fileProvider.MapPath(PluginDefaults.InstalledPluginsFilePath);
            if (_fileProvider.FileExists(filePath))
                _fileProvider.DeleteFile(filePath);
        }

        /// <summary>
        /// Find a plugin descriptor by some type which is located into the same assembly as plugin
        /// </summary>
        /// <param name="typeInAssembly">Type</param>
        /// <returns>Plugin descriptor if exists; otherwise null</returns>
        public static PluginInfoParser FindPlugin(Type typeInAssembly)
        {
            if (typeInAssembly == null)
                throw new ArgumentNullException(nameof(typeInAssembly));

            return ReferencedPlugins?.FirstOrDefault(plugin =>
                plugin.RefrancedPlugin != null &&
                plugin.RefrancedPlugin.FullName.Equals(typeInAssembly.Assembly.FullName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Get plugin descriptor from the plugin description file
        /// </summary>
        /// <param name="filePath">Path to the description file</param>
        /// <returns>Plugin descriptor</returns>
        public static PluginInfoParser GetPluginInfoParserFromFile(string filePath)
        {
            var text = _fileProvider.ReadAllText(filePath, Encoding.UTF8);

            return GetPluginInfoParserFromText(text);
        }

        /// <summary>
        /// Get plugin descriptor from the description text
        /// </summary>
        /// <param name="text">Description text</param>
        /// <returns>Plugin descriptor</returns>
        public static PluginInfoParser GetPluginInfoParserFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new PluginInfoParser();

            //get plugin descriptor from the JSON file
            var descriptor = JsonConvert.DeserializeObject<PluginInfoParser>(text);

         
            return descriptor;
        }

        /// <summary>
        /// Save plugin descriptor to the plugin description file
        /// </summary>
        /// <param name="pluginDescriptor">Plugin descriptor</param>
        public static void SavePluginInfoParser(PluginInfoParser pluginDescriptor)
        {
            if (pluginDescriptor == null)
                throw new ArgumentException(nameof(pluginDescriptor));

            //get the description file path
            if (pluginDescriptor.AssemblyFileName == null)
                throw new Exception($"Cannot load original assembly path for {pluginDescriptor.AssemblyFileName} plugin.");

            var filePath = _fileProvider.Combine(_fileProvider.GetDirectoryName(pluginDescriptor.AssemblyFileName), PluginDefaults.DescriptionFileName);
            if (!_fileProvider.FileExists(filePath))
                throw new Exception($"Description file for {pluginDescriptor.Name} plugin does not exist. {filePath}");

            //save the file
            var text = JsonConvert.SerializeObject(pluginDescriptor, Formatting.Indented);
            _fileProvider.WriteAllText(filePath, text, Encoding.UTF8);
        }

        /// <summary>
        /// Delete plugin directory from disk storage
        /// </summary>
        /// <param name="pluginDescriptor">Plugin descriptor</param>
        /// <returns>True if plugin directory is deleted, false if not</returns>
        public static bool DeletePlugin(PluginInfoParser pluginDescriptor)
        {
            //no plugin descriptor set
            if (pluginDescriptor == null)
                return false;

            //check whether plugin is installed
            if (pluginDescriptor.IsInstalled)
                return false;

            var directoryName = _fileProvider.GetDirectoryName(pluginDescriptor.AssemblyFileName);

            if (_fileProvider.DirectoryExists(directoryName))
                _fileProvider.DeleteDirectory(directoryName);

            return true;
        }

        
        #endregion

        #region Properties

        /// <summary>
        /// Returns a collection of all referenced plugin assemblies that have been shadow copied
        /// </summary>
        public static IEnumerable<PluginInfoParser> ReferencedPlugins { get; set; }

        /// <summary>
        /// Returns a collection of all plugin which are not compatible with the current version
        /// </summary>
        public static IEnumerable<string> IncompatiblePlugins { get; set; }

        #endregion


    }
}
