using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationServices.Intefaces;
using ApplicationServices.Models;
using Core.Bus;
using Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService,
             INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator
            ) : base(notifications, mediator)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                AddModelStateErrorsToNotification();
                return Result(model);
            }

            _projectService.Create(model);
            return Result(model);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                AddModelStateErrorsToNotification();
                return Result(model);
            }

            _projectService.Update(model);
            return Result(model);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _projectService.Delete(id);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjectModel>> Get()
        {
            return Ok(_projectService.GetProjects());
        }

    }
}