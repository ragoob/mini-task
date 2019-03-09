using Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public interface IEventStore
    {
        Task Save<T>(T theEvent) where T : Event;
    }
}
