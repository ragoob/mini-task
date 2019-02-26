using Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
   public class ProjectDeletedEvent : Event
    {
        public ProjectDeletedEvent(int id ,string name ,string description,bool isPrivate)
        {
            Id = id;
            AggregateId = id;
        }

        public int Id { get; protected set; }
        
    }
}
