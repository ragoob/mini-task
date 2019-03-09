using Core.Infrastructure;
using Domain.Validation.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Projects
{
    public class DeleteProjectCommand : ProjectCommand
    {
       
        public DeleteProjectCommand()
        {
            
        }
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }
        public override bool IsValid()
        {
            ValidationResult = EngineContext.Current.Resolve<DeleteProjectCommandValidation>().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
