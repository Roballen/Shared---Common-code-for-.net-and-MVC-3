﻿#region

using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Shared.Domain.Logic;
using Shared.Domain.Repository;
using Shared.Linq;

#endregion

namespace Shared.Infrastructure.Repository.Impl
{
    public abstract class DbContextRepository<TDBContext, TModel> :
        IGenericRepository<TModel>
        where TModel : class, IAggregateRoot
        where TDBContext : DbContext, IUnitOfWork
    {
        private bool _disposed;
        private TDBContext _entities;

        protected DbContextRepository(TDBContext entities)
        {
            _entities = entities;
        }

        protected TDBContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        #region IGenericRepository<TModel> Members

        public int GetCount()
        {
            return GetAll().Count();
        }

        public virtual IQueryable<TModel> GetAll()
        {
            IQueryable<TModel> query = _entities.Set<TModel>();
            return query;
        }

        public virtual IQueryable<TModel> GetAll<TValue>(Expression<Func<TModel, TValue>> orderBy, int pageNumber,
                                                         int pageSize)
        {
            return GetAll().OrderBy(orderBy).Page(pageNumber, pageSize);
        }

        public IQueryable<TModel> FindBy(Expression<Func<TModel, bool>> predicate)
        {
            var query = _entities.Set<TModel>().Where(predicate);
            return query;
        }

        public virtual void Add(TModel entity)
        {
            _entities.Set<TModel>().Add(entity);
        }

        public virtual void Delete(TModel entity)
        {
            _entities.Set<TModel>().Remove(entity);
        }

        public virtual void Edit(TModel entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IQueryable<TModel> GetAll(string orderBy, int pageNumber, int pageSize)
        {
            return GetAll().OrderBy(orderBy).Page(pageNumber, pageSize);
        }

        #endregion

        public abstract void Save(TModel model);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed) if (disposing) _entities.Dispose();

            _disposed = true;
        }
    }
}