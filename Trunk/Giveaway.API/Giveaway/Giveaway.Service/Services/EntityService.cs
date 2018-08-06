using Giveaway.Data.Models;
using Giveaway.Data.EF.Repository;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Giveaway.Data.EF;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;

namespace Giveaway.Service.Services
{
    public abstract class EntityService<T> : IEntityService<T> where T : class, IEntity
    {
        protected IGenericRepository<T> Repository;

        protected EntityService(IGenericRepository<T> repository)
        {
            Repository = repository;
        }

        protected EntityService()
        {
            Repository = new GenericRepository<T>();
        }

        public virtual IQueryable<T> All()
        {
            return Repository.All();
        }


        public IIncludableQueryable<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            return Repository.Include(navigationPropertyPath);
        }

        public virtual T Find(Guid id)
        {
            var response = Repository.Find(id);
            if (response is BaseEntity baseEntity)
            {
                if (baseEntity.EntityStatus == EntityStatus.Deleted)
                {
                    throw new BadRequestException(Const.Error.BadRequest);
                }
            }
            return response;
        }

        public async Task<T> FindAsync(Guid id)
            => await Repository.FindAsync(id);

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Repository.FirstOrDefault(predicate);
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return Repository.Where(predicate);
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50)
        {
            return Repository.Where(predicate, out total, index, size);
        }

        public virtual T Create(T entity, out bool isSaved)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var result = Repository.Create(OnCreate(entity), out isSaved);
            return result;
        }

        public virtual IEnumerable<T> CreateMany(IEnumerable<T> objects, out bool isSaved)
        {
            return Repository.CreateMany(objects.Select(OnCreate), out isSaved);
        }

        public virtual bool Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var result = Repository.Update(OnUpdate(entity));
            return result;
        }

        public T UpdateStatus(Guid id, string status)
        {
            var entity = Find(id);
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.EntityStatus = GetEnumStatus(status);
                baseEntity.UpdatedTime = DateTimeOffset.UtcNow;
            }
            else
            {
                throw new BadRequestException(Const.Error.BadRequest);
            }
            var isUpdated = Update(entity);
            if (!isUpdated)
            {
                throw new InternalServerErrorException(Const.Error.InternalServerError);
            }
            return entity;
        }

        public virtual IEnumerable<T> UpdateMany(IEnumerable<T> objects, out bool isSaved)
        {
            return Repository.UpdateMany(objects.Select(OnUpdate), out isSaved);
        }

        public virtual bool Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var result = Repository.Delete(entity);
            //var result = Repository.Update(OnDelete(entity));
            return result;
        }

        public bool DeletePermanent(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var result = Repository.Delete(entity);

            return result;
        }

        public bool DeletePermanent(IEnumerable<T> objects)
        {
            Repository.Delete(objects, out var ok);

            return ok;
        }

        public virtual IEnumerable<T> Delete(IEnumerable<T> objects, out bool isSaved)
        {
            return Repository.Delete(objects.Select(OnDelete), out isSaved);
        }

        public virtual int Delete(Expression<Func<T, bool>> predicate, out bool isSaved)
        {
            return Repository.Delete(predicate, out isSaved);
        }

        public virtual bool Contains(Expression<Func<T, bool>> predicate)
        {
            return Repository.Contains(predicate);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return Repository.Count(predicate);
        }

        public int Count()
        {
            return Repository.Count();
        }

        //public virtual bool SaveChanges()
        //{
        //	return UnitOfWork.Commit();
        //}

        //public virtual async Task<bool> SaveChangesAsync()
        //{
        //	var result = await UnitOfWork.CommitAsync();
        //	return result;
        //}

        //public virtual async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        //{
        //	var result = await UnitOfWork.CommitAsync(cancellationToken);
        //	return result;
        //}

        protected virtual T OnCreate(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                if (baseEntity.Id == Guid.Empty) baseEntity.Id = Guid.NewGuid();
                if (baseEntity.CreatedTime.Year == 0001)
                {
                    baseEntity.CreatedTime = baseEntity.UpdatedTime = DateTimeOffset.UtcNow;
                }
                baseEntity.EntityStatus = EntityStatus.Activated;
            }
            return entity;
        }

        protected virtual T OnUpdate(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.UpdatedTime = DateTimeOffset.UtcNow;
            }

            return entity;
        }

        protected virtual T OnDelete(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.EntityStatus = EntityStatus.Deleted;
                baseEntity.UpdatedTime = DateTimeOffset.UtcNow;
            }
            return entity;
        }

        #region IDisposable
        private bool isDisposed;

        ~EntityService()
        {
            Dispose(false);
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposed)
        {
            if (!isDisposed || this.isDisposed) return;
            if (Repository == null) return;

            this.isDisposed = true;
            Repository.Dispose();
            Repository = null;
        }

        private static EntityStatus GetEnumStatus(string status)
        {
            if (status == EntityStatus.Activated.ToString())
            {
                return EntityStatus.Activated;
            }
            if (status == EntityStatus.Blocked.ToString())
            {
                return EntityStatus.Blocked;
            }
            if (status == EntityStatus.Deleted.ToString())
            {
                return EntityStatus.Deleted;
            }
            throw new BadRequestException(Const.Error.BadRequest);
        }
        #endregion
    }
}