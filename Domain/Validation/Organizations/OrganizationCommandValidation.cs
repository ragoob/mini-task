using Domain.Commands.Organizations;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Organizations
{
   public class OrganizationCommandValidation<T> : AbstractValidator<T> where T : OrganizationCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Name Is Required")
               .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }
        
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);
        }

       
    }
}
