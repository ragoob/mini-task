using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.EventHandlers
{
    public class OrganizationEventHandler :
           INotificationHandler<OrganizationAddedEvent>,
          INotificationHandler<OrganizationUpdatedEvent>,
          INotificationHandler<OrganizationDeletedEvent>
    {
        public Task Handle(OrganizationAddedEvent notification, CancellationToken cancellationToken)
        {
           return Task.CompletedTask;
        }

        public Task Handle(OrganizationUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrganizationDeletedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
