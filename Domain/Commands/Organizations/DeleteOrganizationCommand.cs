using Domain.Validation.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Organizations
{
    public class DeleteOrganizationCommand : OrganizationCommand
    {
        public DeleteOrganizationCommand(int id) => Id = id;
        public override bool IsValid()
        {
            ValidationResult = new DeleteOrganizationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
