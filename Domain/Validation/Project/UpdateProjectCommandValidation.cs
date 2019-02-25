using Domain.Commands.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Project
{
    public class UpdateProjectCommandValidation : ProjectValidationCommand<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidation()
        {
            ValidateName();
            ValidateDescription();
            ValidateId();


        }
    }
}
