using Core.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Intefaces
{
    public interface IPluginService
    {
        IEnumerable<PluginInfoParser> List(bool InstalledOnly=false);

        PluginInfoParser GetByName(string Name);


    }
}
