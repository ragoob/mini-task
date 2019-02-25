using Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
   public class AddNewProjectEvent : Event
    {
        public AddNewProjectEvent(int id ,string name ,string description,bool isPrivate)
        {
        
            Name = name;
            Description = description;
            IsPrivate = IsPrivate;
            AggregateId = id;
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }

        /// <summary>
        /// getter or setter Project Description
        /// </summary>
        public string Description { get; protected set; }
        /// <summary>
        /// getter or setter Project Pricvicy 
        /// </summary>
        public bool IsPrivate { get; protected set; }
    }
}
