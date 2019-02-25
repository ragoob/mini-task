using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.EventHandlers
{
    public class ProjectEventHandler :
         INotificationHandler<AddNewProjectEvent>,
         INotificationHandler<UpdateProjectEvent>,
         INotificationHandler<DeleteProjectEvent>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AddNewProjectEvent notification, CancellationToken cancellationToken)
        {
           
            return Task.CompletedTask;
        }

        public Task Handle(UpdateProjectEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(DeleteProjectEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
