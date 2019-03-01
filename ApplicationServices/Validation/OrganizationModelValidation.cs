using ApplicationServices.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Validation
{
   public class OrganizationModelValidation : AbstractValidator<OrganizationModel>
    {
        public OrganizationModelValidation()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .Length(2, 100)
                .WithMessage("Name must be not empty and length between 2 to 100");

            RuleFor(c =>(int)c.EmployeesCount)
             .GreaterThan(0)
             .WithMessage("The Employees count must be greater than 0");


        }
    }
}
