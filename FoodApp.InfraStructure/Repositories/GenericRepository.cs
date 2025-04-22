using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.InfraStructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T item)
            => await _context.Set<T>().AddAsync(item);   

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdWithTrackingAsync(id);
            if (entity is null) return;
            entity.IsDeleted = true;         
        }

        public IQueryable<T> GetAll()
            => _context.Set<T>().Where(e => !e.IsDeleted);

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
            => GetAll().Where(predicate);

        public async Task<T?> GetByIdAsync(int id)
            => await _context.Set<T>()
                .Where(e => !e.IsDeleted && e.Id == id)
                .FirstOrDefaultAsync();

        public async Task<T?> GetByIdWithTrackingAsync(int id)
            => await _context.Set<T>()
                .Where(e => !e.IsDeleted && e.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await SaveChangesAsync();
        }

        public async Task BulkUpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
        {
            await _context.Set<T>().Where(filter).ExecuteUpdateAsync(updateExpression);
        }

        public void UpdateInclude(T item, params string[] modifiedProperties)
        {
            var entry = _context.ChangeTracker
                .Entries<T>()
                .FirstOrDefault(x => x.Entity.Id == item.Id)
                ?? _context.Entry(item);

            foreach (var prop in entry.Properties)
            {
                if (modifiedProperties.Contains(prop.Metadata.Name))
                {
                    prop.CurrentValue = item
                        .GetType()
                        .GetProperty(prop.Metadata.Name)!
                        .GetValue(item);
                    prop.IsModified = true;
                }
            }
        }

        public async Task SoftDeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public async Task SoftDeleteRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
            await UpdateRangeAsync(entities);
        }

        public async Task BulkSoftDeleteAsync(Expression<Func<T, bool>> filter)
        {
            await _context.Set<T>()
                .Where(filter)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsDeleted, true));
        }

        public async Task HardDeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task HardDeleteRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await SaveChangesAsync();
        }

        public async Task BulkHardDeleteAsync(Expression<Func<T, bool>> filter)
        {
            await _context.Set<T>().Where(filter).ExecuteDeleteAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
