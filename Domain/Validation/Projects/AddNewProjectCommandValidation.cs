using Domain.Commands.Projects;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Projects
{
    public class AddNewProjectCommandValidation : ProjectValidationCommand<AddNewProjectCommand>
    {

        private readonly IRepository<Project> _repository;

        public AddNewProjectCommandValidation(IRepository<Project> repository) : base(repository)
        {
            _repository = repository;
            ValidateName();
            ValidateDescription();
            ValidateNameExist();
        }

        
    }
}
