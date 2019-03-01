using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Plugins
{
    public abstract class BasePlugin : IPlugin
    {
        public PluginInfoParser PluginInfoParser { get; set; }

        public virtual void Install()
        {
            PluginManager.MarkPluginAsInstalled(PluginInfoParser.Name);
        }

        public virtual void UnInstall()
        {
            PluginManager.MarkPluginAsUninstalled(PluginInfoParser.Name);
        }
    }
}
