using Domain.Validation.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Organizations
{
    public class AddNewOrganizationCommand : OrganizationCommand
    {
        public AddNewOrganizationCommand(string name , short employeesCount)
        {
            Name = name;
            EmployeesCount = employeesCount;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new AddNewOrganizationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
