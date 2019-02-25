using Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
   public class DeleteProjectEvent : Event
    {
        public DeleteProjectEvent(int id ,string name ,string description,bool isPrivate)
        {
            Id = id;
            AggregateId = id;
        }

        public int Id { get; protected set; }
        
    }
}
