using ApplicationServices.Intefaces;
using ApplicationServices.Models;
using Core.Bus;
using Domain.Commands.Project;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ApplicationServices.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMediatorHandler _bus;

        public ProjectService(IProjectRepository projectRepository, IMediatorHandler bus)
        {
            _projectRepository = projectRepository;
            _bus = bus;
        }
        public void Create(ProjectModel project)
        {
            
            // you can use automapper to map source and destination 
            var AddNewCommand = new AddNewProjectCommand(project.Name, project.Description, project.IsPrivate);
            _bus.SendCommand(AddNewCommand);
        }

        public void Delete(int id)
        {
            // you can use automapper to map source and destination 

            var DeleteCommand = new DeleteProjectCommand(id);
            _bus.SendCommand(DeleteCommand);
        }

        public ProjectModel GetById(int id)
        {
            // you can use automapper to map source and destination 

            var project = _projectRepository.GetById(id);
            if (project != null)
                return new ProjectModel()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    IsPrivate = project.IsPrivate
                };

            return null;
        }

        public ProjectModel GetByName(string name)
        {
            // you can use automapper to map source and destination 

            var project = _projectRepository.GetByName(name);
            if (project != null)
                return new ProjectModel()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    IsPrivate = project.IsPrivate
                };

            return null;
        }

        public IEnumerable<ProjectModel> GetProjects()
        {
            // you can use automapper to map source and destination 


            return _projectRepository.GetAll()
                .Select(m => new ProjectModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    IsPrivate = m.IsPrivate
                });

        }

        public void Update(ProjectModel project)
        {
            // you can use automapper to map source and destination 
            var UpdateCommand = new UpdateProjectCommand(project.Id, project.Name, project.Description, project.IsPrivate);
            _bus.SendCommand(UpdateCommand);
        
        }
    }
}
