using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Plugins
{
   public static class PluginDefaults
    {
      
        /// <summary>
        /// Gets the path to file that contains installed plugin system names
        /// </summary>
        public static string InstalledPluginsFilePath => "~/App_Data/installedPlugins.json";

        /// <summary>
        /// Gets the path to plugins folder
        /// </summary>
        public static string Path => "~/Plugins";

        /// <summary>
        /// Gets the plugins folder name
        /// </summary>
        public static string PathName => "Plugins";

        /// <summary>
        /// Gets the path to plugins shadow copies folder
        /// </summary>
        public static string ShadowCopyPath => "~/Plugins/bin";

     

        /// <summary>
        /// Gets the name of the plugin description file
        /// </summary>
        public static string DescriptionFileName => "plugin.json";
        
    }
}
