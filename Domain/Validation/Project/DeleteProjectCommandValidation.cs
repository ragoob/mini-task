using Domain.Commands.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Project
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
