using FoodApp.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task AddAsync(T item);
        Task DeleteAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdWithTrackingAsync(int id);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task BulkUpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);
        void UpdateInclude(T item, params string[] modifiedProperties);
        Task SoftDeleteAsync(T entity);
        Task SoftDeleteRangeAsync(IEnumerable<T> entities);
        Task BulkSoftDeleteAsync(Expression<Func<T, bool>> filter);
        Task HardDeleteAsync(T entity);
        Task HardDeleteRangeAsync(IEnumerable<T> entities);
        Task BulkHardDeleteAsync(Expression<Func<T, bool>> filter);
        Task SaveChangesAsync();
    }
}
