using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Bus;
using Core.Notifications;
using Infrastructure.Extentions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
   
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        protected BaseController(INotificationHandler<DomainNotification> notifications,
                               IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

  
        protected  IActionResult Result(object model = null)
        {
            if (!_notifications.HasNotifications())
                return Ok(model);
            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        /// <summary>
        /// feach validation error and RaiseEvent with errors
        /// </summary>
        protected void AddModelStateErrorsToNotification()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            erros.ForEach(erro =>
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                _mediator.RaiseEvent(new DomainNotification(string.Empty, erroMsg));
            });
           
        }

      

    }
}