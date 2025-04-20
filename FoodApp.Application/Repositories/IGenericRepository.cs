using FoodApp.Domain.Data.Entities;
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
        void Update(T item);
        void UpdateInclude(T item, params string[] modifiedProperties);

    }
}
