using Domain.Commands.Project;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Extentions;
using System;

namespace Domain.Validation.Project
{
    public abstract class ProjectValidationCommand<T> : AbstractValidator<T> where T : ProjectCommand 
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Name Is Required")
               .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidateDescription()
        {
            RuleFor(c => c.Description)
              .Length(2, 500).WithMessage("The Name must have between 2 and 500 characters");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateNameExist()
        {
            RuleFor(c => c.Name)
                .Custom((n, c) =>
                {
                    
                    if (EngineContext.Current.Resolve<IProjectRepository>().GetByName(n) != null)
                    {
                        c.AddFailure($"{n} is Exist type new one");
                    }

                });
        }



    }
}
