using Domain.Validation.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Projects
{
    public class DeleteProjectCommand : ProjectCommand
    {
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }
        public override bool IsValid()
        {
            ValidationResult = new DeleteProjectCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
