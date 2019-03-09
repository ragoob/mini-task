using Domain.Commands.Projects;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validation.Projects
{
    /// <summary>
    /// validation for delete
    /// </summary>
    public class DeleteProjectCommandValidation : ProjectValidationCommand<DeleteProjectCommand>
    {
        private readonly IRepository<Project> _repository;
        public DeleteProjectCommandValidation(IRepository<Project> repository) : base(repository)
        {
            _repository = repository;
            ValidateId();
        }
    }
}
