using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationServices.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluginController : ControllerBase
    {
        private readonly IPluginService _pluginService;

        public PluginController(IPluginService pluginService)
        {
            this._pluginService = pluginService;
        }
        [HttpGet]
        public  IActionResult Get()
        {
            var List = _pluginService.List();
            return Ok(List);
        }

        [HttpPost("Install/{PluginName}")]
        public IActionResult Install(string PluginName)
        {
            var PluginParser = _pluginService.GetByName(PluginName);
            if (PluginParser == null)
                return BadRequest("Plugin not found");
            PluginParser.Instance().Install();
            return Ok("Installed successfuly");

        }

        [HttpPost("UnInstall/{PluginName}")]
        public IActionResult UnInstall(string PluginName)
        {
            var PluginParser = _pluginService.GetByName(PluginName);
            if (PluginParser == null)
                return BadRequest("Plugin not found");
            if(!PluginParser.IsInstalled)
                return BadRequest("Plugin not Installed");
            PluginParser.Instance().UnInstall();
            return Ok("UnInstalled successfuly");

        }
    }
}