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
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationService _projectService;

        public OrganizationController(IOrganizationService projectService,
             INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator
            ) : base(notifications, mediator)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrganizationModel model)
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
        public IActionResult Put([FromBody] OrganizationModel model)
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
        public ActionResult<IEnumerable<OrganizationModel>> Get()
        {
            return Ok(_projectService.GetOrganizations());
        }
    }
}