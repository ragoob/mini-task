using Domain.Commands.Projects;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Projects
{
    public class UpdateProjectCommandValidation : ProjectValidationCommand<UpdateProjectCommand>
    {
        private readonly IRepository<Project> _repository;

        public UpdateProjectCommandValidation(IRepository<Project> repository) : base(repository)
        {
            _repository = repository;
            ValidateName();
            ValidateDescription();
            ValidateId();
            //ValidateNameExist();


        }
    }
}
