using Domain.Validation.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Project
{
    public class AddNewProjectCommand : ProjectCommand
    {
        public AddNewProjectCommand(string name ,string description,bool isPrivate)
        {
            Name = name;
            Description = description;
            IsPrivate = isPrivate;
        }
        public override bool IsValid()
        {
            ValidationResult = new AddNewProjectCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
