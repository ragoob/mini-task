using Core.Events;
using Core.Infrastructure;
using Data.Context;
using Domain.Events;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.EventSources
{
    public class EventStore : IEventStore
    {
        private readonly EventSourceContext _eventSourceContext;

         
        public EventStore(EventSourceContext eventSourceContext) {
            _eventSourceContext = eventSourceContext;
        }

       

        public Task Save<T>(T theEvent)where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);
            _eventSourceContext.Set<EventSource>().Add(new EventSource(theEvent,serializedData));
            return _eventSourceContext.SaveChangesAsync();
        }
    }
}
