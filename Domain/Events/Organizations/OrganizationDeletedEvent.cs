using Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
   public class OrganizationDeletedEvent : Event
    {
        public OrganizationDeletedEvent(int id)
        {
            Id = id;
            AggregateId = id;
        }

        public int Id { get; protected set; }
        
    }
}
