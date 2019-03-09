using Core.Infrastructure;
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
        private readonly IEventStore _eventStore;

        public ProjectEventHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  Task Handle(ProjectAddedEvent notification, CancellationToken cancellationToken)
        {

           return _eventStore.Save(notification);
            
        }

        public  Task Handle(ProjectUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return _eventStore.Save(notification);
        }

        public  Task Handle(ProjectDeletedEvent notification, CancellationToken cancellationToken)
        {
            return _eventStore.Save(notification);
        }
    }
}
