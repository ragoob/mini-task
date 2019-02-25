using Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
   public class DeleteProjectEvent : Event
    {
        public DeleteProjectEvent(Guid id ,string name ,string description,bool isPrivate)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; protected set; }
        
    }
}
