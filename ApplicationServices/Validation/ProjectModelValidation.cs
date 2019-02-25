using ApplicationServices.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Validation
{
    public class ProjectModelValidation : AbstractValidator<ProjectModel>
    {
       public ProjectModelValidation()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .Length(2, 100)
                .WithMessage("Name must be not empty and length between 2 to 100");

            RuleFor(c => c.Description)
             .Length(2, 500).WithMessage("The Name must have between 2 and 500 characters");

          
        }
    }
}
