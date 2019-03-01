using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Plugins
{
   public interface IPluginInfoParser
    {
        string Name { get; set; }

        string FriendlyName { get; set; }

      
    }
}
