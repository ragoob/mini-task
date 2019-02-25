using Data.Context;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(MiniTaskContext context)
           : base(context)
        {

        }
        public Project GetByName(string name)
        {
            return _entities.AsNoTracking().SingleOrDefault(p => p.Name == name);
        }
    }
}
