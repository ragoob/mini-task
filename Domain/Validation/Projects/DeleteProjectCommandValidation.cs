using Domain.Commands.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Projects
{
    /// <summary>
    /// validation for delete
    /// </summary>
    public class DeleteProjectCommandValidation : ProjectValidationCommand<DeleteProjectCommand>
    {
        public DeleteProjectCommandValidation()
        {
            ValidateId();
        }
    }
}
