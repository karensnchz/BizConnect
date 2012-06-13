using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace BizConnect.Services
{
    public interface IService<TEntity>
    {
        IQueryable<TEntity> All();
        TEntity ByKey(Func<TEntity, bool> predicate);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<DbEntityValidationResult> Validate();
    }

    public abstract class Service<TEntity, TDbContext> : IService<TEntity>
        where TEntity : class
        where TDbContext : DbContext, new()
    {
        protected TDbContext Entities { get; private set; }

        protected Service(TDbContext dbContext)
        {
            Entities = dbContext;
        }

        public IQueryable<TEntity> All()
        {
            return Entities.Set<TEntity>();
        }

        public TEntity ByKey(Func<TEntity, bool> predicate)
        {
            return Entities.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual TEntity Add(TEntity entity)
        {
            Entities.Set<TEntity>().Add(entity);
            var validationErrors = GetVadidationErrors();
            if (validationErrors.Count() > 0)
                throw new ValidateException(validationErrors);
            Entities.SaveChanges();
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {

            var validationErrors = GetVadidationErrors();
            if (validationErrors.Count() > 0)
                throw new ValidateException(validationErrors);
            Entities.SaveChanges();
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            Entities.Set<TEntity>().Remove(entity);
            Entities.SaveChanges();
        }

        public IEnumerable<DbEntityValidationResult> Validate()
        {
            return Entities.GetValidationErrors();
        }

        private IEnumerable<DbEntityValidationResult> GetVadidationErrors()
        {
            return Entities.GetValidationErrors();
        }
    }
}
