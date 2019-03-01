using Domain.Commands.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Organizations
{
   public class DeleteOrganizationCommandValidation : OrganizationCommandValidation<DeleteOrganizationCommand>
    {
        public DeleteOrganizationCommandValidation()
        {
            ValidateId();
        }
    }
}
