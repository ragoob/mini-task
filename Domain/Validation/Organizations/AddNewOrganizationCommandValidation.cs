using Domain.Commands.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Organizations
{
   public class AddNewOrganizationCommandValidation : OrganizationCommandValidation<AddNewOrganizationCommand>
    {
        public AddNewOrganizationCommandValidation()
        {
            ValidateName();
        }
    }
}
