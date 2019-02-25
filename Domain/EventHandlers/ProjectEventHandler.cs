using Domain.Events;
using Infrastructure.Extentions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.EventHandlers
{
    public class ProjectEventHandler :
         INotificationHandler<ProjectAddedEvent>,
         INotificationHandler<ProjectUpdatedEvent>,
         INotificationHandler<ProjectDeletedEvent>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(ProjectAddedEvent notification, CancellationToken cancellationToken)
        {
           return SendEmailTask.Send($"new project Add {notification.Name}",$"new Project Added to ssystem" +
                $" with name {notification.Name} </br> {notification.Description}","regoo707@gmail.com");


             
        }

        public Task Handle(ProjectUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProjectDeletedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
