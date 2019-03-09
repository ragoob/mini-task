using Core.Infrastructure;
using Domain.Validation.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Projects
{
    public class AddNewProjectCommand : ProjectCommand
    {

        public AddNewProjectCommand()
        {
            
        }
        public AddNewProjectCommand(string name ,string description,bool isPrivate,int organizationId)
        {
            Name = name;
            Description = description;
            IsPrivate = isPrivate;
            OrganizationId = organizationId;
        }
        public override bool IsValid()
        {
            ValidationResult = EngineContext.Current.Resolve<AddNewProjectCommandValidation>().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
