using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
   public class Project : Entity
    {
        public Project(Guid id ,string name ,string description,bool isPrivate)
        {
            Id = id;
            Name = name;
            Description = description;
            IsPrivate = IsPrivate;
            
        }

        /// <summary>
        /// empty constractor for ef migrations
        /// </summary>
        protected Project() { }

        /// <summary>
        /// getter or setter project Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// getter or setter Project Description
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// getter or setter Project Pricvicy 
        /// </summary>
        public bool IsPrivate { get; private set; }


    }
}
