using Giveaway.Data.EF.DataContext;
using Giveaway.Data.Models;
using Giveaway.Util.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Giveaway.Data.EF.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        public IQueryable<T> All()
        {
            return GetDataContext().Set<T>().AsQueryable();
        }

        public IIncludableQueryable<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            return All().Include(navigationPropertyPath);
        }

        public T Find(Guid id)
        {
            return GetDataContext().Set<T>().Find(id);
        }

        public async Task<T> FindAsync(Guid id)
        {
            var result = await GetDataContext().Set<T>().FindAsync(id);
            return result;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return GetDataContext().Set<T>().FirstOrDefault<T>(predicate);
            }
            catch (Exception e)
            {
                //TODO Write log
                var error = "FirstOrDefault has error: " + e.Message;
                Debug.WriteLine(error);
                return null;
            }
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return GetDataContext().Set<T>().Where<T>(predicate);
            }
            catch (Exception e)
            {
                var error = "Where has error: " + e.Message;
                Debug.WriteLine(error);
                return null;
            }
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;

            try
            {
                var set = GetDataContext().Set<T>();
                var resetSet = predicate != null ? set.Where<T>(predicate).AsQueryable() : set.AsQueryable();
                resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
                total = resetSet.Count();

                return resetSet.AsQueryable();
            }
            catch (Exception e)
            {
                var error = "Where has error: " + e.Message;
                Debug.WriteLine(error);
                total = 0;
                return null;
            }
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            var set = GetDataContext().Set<T>();
            return set.Any(predicate);
        }

        public T Create(T entity, out bool isSaved)
        {
            try
            {
                var context = GetDataContext();
                var set = context.Set<T>();
                var newEntry = set.Add(entity);
                isSaved = context.SaveChanges() > 0;

                return newEntry.Entity;
            }
            catch (DbUpdateException e)
            {
                var error = "Create has error: " + e.Message;
                Debug.WriteLine(error);
                isSaved = false;
                return null;
            }
        }

        public IEnumerable<T> CreateMany(IEnumerable<T> entities, out bool isSaved)
        {
            var context = GetDataContext();
            var set = context.Set<T>();
            try
            {
                if (entities != null)
                {
                    set.AddRange(entities);
                }

                isSaved = context.SaveChanges() > 0;
                return entities;
            }
            catch (Exception e)
            {
                var error = "CreateMany has error: " + e.Message;
                Debug.WriteLine(error);
                set.RemoveRange(entities);
                isSaved = context.SaveChanges() > 0;
                return null;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                var context = GetDataContext();
                var set = GetDataContext().Set<T>();
                set.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                var error = "Update has error: " + e.Message;
                Debug.WriteLine(error);
                return false;
            }
        }

        public IEnumerable<T> UpdateMany(IEnumerable<T> entities, out bool isSaved)
        {
            try
            {

                var context = GetDataContext();
                var set = context.Set<T>();

                if (entities != null)
                {
                    foreach (var a in entities)
                    {
                        set.Attach(a);
                        context.Entry(a).State = EntityState.Modified;
                    }
                }

                isSaved = context.SaveChanges() > 0;
                return entities;
            }
            catch (Exception e)
            {
                var error = "UpdateMany has error: " + e.Message;
                Debug.WriteLine(error);
                isSaved = false;
                return null;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                var context = GetDataContext();
                var set = context.Set<T>();
                set.Remove(entity);
                return context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                var error = "Delete has error: " + e.Message;
                Debug.WriteLine(error);
                return false;
            }
        }

        public IEnumerable<T> Delete(IEnumerable<T> objects, out bool isSaved)
        {
            try
            {
                var context = GetDataContext();
                var set = context.Set<T>();
                if (objects != null) set.RemoveRange(objects);
                isSaved = context.SaveChanges() > 0;

                return objects;
            }
            catch (Exception e)
            {
                var error = "Delete has error: " + e.Message;
                Debug.WriteLine(error);
                isSaved = false;
                return null;
            }
        }

        public int Delete(Expression<Func<T, bool>> predicate, out bool isSaved)
        {
            try
            {
                var context = GetDataContext();
                var set = context.Set<T>();
                var objects = Where(predicate);

                foreach (var obj in objects)
                {
                    set.Remove(obj);
                }

                isSaved = context.SaveChanges() > 0;
                return objects.Count();
            }
            catch (Exception e)
            {
                var error = "Delete has error: " + e.Message;
                Debug.WriteLine(error);
                isSaved = false;
                return 0;
            }
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            var set = GetDataContext().Set<T>();
            return set.Count(predicate);
        }

        public int Count()
        {
            var set = GetDataContext().Set<T>();
            return set.Count();
        }

        public void ExecuteProcedure(string procedureCommand, params MySqlParameter[] sqlParams)
        {
        }

        private DatabaseContext GetDataContext()
        {
            var configurationRoot = ServiceProviderHelper.Current.GetService<IConfigurationRoot>();
            return new DatabaseContext(configurationRoot);
        }

        #region IDisposable
        private bool _isDisposed;

        ~GenericRepository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (!dispose || _isDisposed) return;

            _isDisposed = true;
        }
        #endregion
    }
}