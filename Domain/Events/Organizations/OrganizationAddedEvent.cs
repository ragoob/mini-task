using Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
   public class OrganizationAddedEvent : Event
    {
        public OrganizationAddedEvent(int id ,string name ,short employeesCount)
        {
           
            Name = name;
            EmployeesCount = employeesCount;
          
            AggregateId = id;
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }

        /// <summary>
        /// getter or setter Project EmployeesCount
        /// </summary>
        public short EmployeesCount { get; protected set; }
      
       
    }
}
