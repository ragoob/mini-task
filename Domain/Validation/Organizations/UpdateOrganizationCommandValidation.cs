using Domain.Commands.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Organizations
{
   public class UpdateOrganizationCommandValidation : OrganizationCommandValidation<UpdateOrganizationCommand>
    {
        public UpdateOrganizationCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
