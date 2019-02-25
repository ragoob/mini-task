using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity obj);
        void Remove(int id);
        void AddRange(IEnumerable<TEntity> entitiesList);
        void RemoveRange(IEnumerable<TEntity> entitiesList);


    }
}
