using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Plugins
{
   public interface IPlugin
    {
        void Install();

        void UnInstall();

        PluginInfoParser PluginInfoParser { get; set; }
    }
}
