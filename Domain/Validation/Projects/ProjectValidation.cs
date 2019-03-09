using Core.Infrastructure;
using Domain.Commands.Projects;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Extentions;
using System;

namespace Domain.Validation.Projects
{
    public abstract class ProjectValidationCommand<T> : AbstractValidator<T> where T : ProjectCommand 
    {
        private readonly IRepository<Project> _repository;

        public ProjectValidationCommand(IRepository<Project> repository)
        {
            _repository = repository;
        }
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
                .NotEqual(0);
        }
        
        protected void ValidateNameExist()
        {
            RuleFor(c => c.Name)
                .Custom((n, context) =>
                {
                    
                   
                    if(_repository.GetFirstOrDefault(p=> p.Name == n) != null)
                        context.AddFailure($"{n} is Exist type new one");

                });
        }



    }
}
