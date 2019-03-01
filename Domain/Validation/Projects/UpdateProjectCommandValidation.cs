using Domain.Commands.Projects;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Projects
{
    public class UpdateProjectCommandValidation : ProjectValidationCommand<UpdateProjectCommand>
    {
       
        public UpdateProjectCommandValidation()
        {
          
            ValidateName();
            ValidateDescription();
            ValidateId();
            ValidateNameExist();


        }
    }
}
