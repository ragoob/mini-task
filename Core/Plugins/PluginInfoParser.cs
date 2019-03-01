using Infrastructure.Extentions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Plugins
{
    public class PluginInfoParser : IPluginInfoParser
    {

        #region Ctor
        public PluginInfoParser()
        {

        }

        public PluginInfoParser(Assembly refrancedPlugin) : this()
        {
            this.RefrancedPlugin = refrancedPlugin;
        }
        #endregion
        
        #region Methods 


        public IPlugin Instance()
        {
            return Instance<IPlugin>();
        }
        public virtual T Instance<T>() where T : class, IPlugin
        {
            object instance = null;
            try
            {
                instance = EngineContext.Current.Resolve(Plugin);
            }
            catch
            {
                //try resolve
            }

            if (instance == null)
            {
                //not resolved via DI try to resolve manual via reflection
                instance = EngineContext.Current.ManualResolve(Plugin);
            }

            var typedInstance = instance as T;
            if (typedInstance != null)
                typedInstance.PluginInfoParser = this;

            return typedInstance;
        }


        #endregion

        #region Proprties 
        [JsonProperty("PluginName")]
       public string Name { get; set; }
        [JsonProperty("PluginFriendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty(PropertyName = "FileName")]
        public virtual string AssemblyFileName { get; set; }

        [JsonIgnore]
        public string PluginAssemblyName { get; set; }
        [JsonIgnore]
        public Assembly RefrancedPlugin { get; set; }
        [JsonIgnore]
        public bool IsInstalled { get; set; }
        [JsonIgnore]
        public Type Plugin { get; set; }
        #endregion


    }
}
