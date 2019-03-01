using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
   public class Organization : Entity
    {
        public Organization(int id ,string name,short employeesCount)
        {
            Id = id;
            Name = name;
            EmployeesCount = employeesCount;
        }
        public string Name { get; private set; }

    
        public short   EmployeesCount { get;private set; }

     
    }
}
