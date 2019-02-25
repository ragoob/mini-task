using Core.Models;
using Data.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly MiniTaskContext _miniTaskContext;
        protected readonly DbSet<TEntity> _entities;

        public Repository(MiniTaskContext miniTaskContext)
        {
            _miniTaskContext = miniTaskContext;
            _entities = miniTaskContext.Set<TEntity>();
        }
        public void Add(TEntity obj)
        {
            _entities.Add(obj);
        }

        public void AddRange(IEnumerable<TEntity> entitiesList)
        {
            _entities.AddRange(entitiesList);
        }

       

        public TEntity GetById(int id)
        {
            return _entities.Find(id);
        }

        public void Remove(int id)
        {
            _entities.Remove(GetById(id));
        }

        public void RemoveRange(IEnumerable<TEntity> entitiesList)
        {
            _entities.RemoveRange(entitiesList);
        }

        public void Update(TEntity obj)
        {
            _entities.Update(obj);
        }

        public void Dispose()
        {
            _miniTaskContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate  != null ? _entities.AsNoTracking()
                 .Where(predicate) : _entities.AsNoTracking();
          
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }
    }
}
