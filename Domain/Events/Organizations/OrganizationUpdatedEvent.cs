using Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
   public class OrganizationUpdatedEvent : Event
    {
        public OrganizationUpdatedEvent(int id ,string name ,short employeesCount)
        {
            Id = id;
            Name = name;
            EmployeesCount = employeesCount;



        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }

        /// <summary>
        /// getter or setter Project employeesCount
        /// </summary>
        public short EmployeesCount { get; protected set; }
    
    }
}
