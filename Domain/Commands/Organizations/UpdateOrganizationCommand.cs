using Domain.Validation.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Organizations
{
    public class UpdateOrganizationCommand : OrganizationCommand
    {
        public UpdateOrganizationCommand(int id ,string name, short employeesCount)
        {
            Id = id;
            Name = name;
            EmployeesCount = employeesCount;
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateOrganizationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
