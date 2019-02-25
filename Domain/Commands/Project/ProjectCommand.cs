
using Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Project
{
   public abstract class ProjectCommand : Command
    {
        public Guid Id { get; protected set; }
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
