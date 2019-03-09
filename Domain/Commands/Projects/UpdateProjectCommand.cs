
using Core.Infrastructure;
using Domain.Validation.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Projects
{
    public class UpdateProjectCommand : ProjectCommand
    {
      
        public UpdateProjectCommand()
        {
            
        }
        public UpdateProjectCommand(int id, string name, string description, bool isPrivate)
        {
            Id = id;
            Name = name;
            Description = description;
            IsPrivate = isPrivate;
        }
        public override bool IsValid()
        {
            ValidationResult = EngineContext.Current.Resolve<UpdateProjectCommandValidation>().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
