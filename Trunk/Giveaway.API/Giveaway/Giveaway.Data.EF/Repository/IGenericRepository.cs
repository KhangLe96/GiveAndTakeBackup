using Giveaway.Data.Models;
using Microsoft.EntityFrameworkCore.Query;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Giveaway.Data.EF.Repository
{
	public interface IGenericRepository<T> : IRepository where T : IEntity
	{
		IQueryable<T> All();

		IIncludableQueryable<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath);

		T Find(Guid id);

		Task<T> FindAsync(Guid id);

		T FirstOrDefault(Expression<Func<T, bool>> predicate);

		IQueryable<T> Where(Expression<Func<T, bool>> predicate);

		IQueryable<T> Where(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50);

		bool Contains(Expression<Func<T, bool>> predicate);

		T Create(T t, out bool isSaved);

		IEnumerable<T> CreateMany(IEnumerable<T> ts, out bool isSaved);

		bool Update(T t);

		IEnumerable<T> UpdateMany(IEnumerable<T> ts, out bool isSaved);

		bool Delete(T t);

		IEnumerable<T> Delete(IEnumerable<T> objects, out bool isSaved);

		int Delete(Expression<Func<T, bool>> predicate, out bool isSaved);

		int Count(Expression<Func<T, bool>> predicate);

		int Count();

		void ExecuteProcedure(string procedureCommand, params MySqlParameter[] sqlParams);
	}
}