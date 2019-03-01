using Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Organizations
{
   public abstract class OrganizationCommand : Command
    {
        public int Id { get; protected set; }

        public string Name { get; protected set; }


        public short EmployeesCount { get; protected set; }

    }
}
