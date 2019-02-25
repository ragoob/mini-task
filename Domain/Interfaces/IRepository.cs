using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(Guid id);
        void AddRange(IEnumerable<TEntity> entitiesList);
        void RemoveRange(IEnumerable<TEntity> entitiesList);


    }
}
