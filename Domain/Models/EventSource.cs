using Core.Events;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
   public class EventSource :Event
    {

        public EventSource() { }
        public int Id { get; private set; }
        public EventSource(Event @event, string data)
        {
            Data = data; 
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
           

        }
        public  string Data { get; private set; }

    }
}
