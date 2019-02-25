using ApplicationServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationServices.Intefaces
{
   public interface IProjectService
    {
        void Create(ProjectModel project);

        void Update(ProjectModel project);

        void Delete(int id);

        ProjectModel GetById(int id);

        ProjectModel GetByName(string name);

        IEnumerable<ProjectModel> GetProjects();




    }
}
