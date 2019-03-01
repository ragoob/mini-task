using ApplicationServices.Intefaces;
using Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationServices.Services
{
    public class PluginService : IPluginService
    {

        public PluginInfoParser GetByName(string Name)
        {
            var plugin = PluginManager.ReferencedPlugins.Where(c => c.Name == Name)
                .SingleOrDefault();
            return plugin;
        }

        public IEnumerable<PluginInfoParser> List(bool InstalledOnly = false)
        {
            var plugins = PluginManager.ReferencedPlugins;
            if (InstalledOnly)
            {
                plugins = plugins.Where(p => p.IsInstalled);
            }

            return plugins;
        }

       
    }
}
