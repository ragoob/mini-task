using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
   public interface IProjectRepository : IRepository<Project>
    {
        Project GetByName(string name);
    }
}
