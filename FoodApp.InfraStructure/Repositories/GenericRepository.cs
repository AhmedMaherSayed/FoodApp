using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
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


        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task AddAsync(T item)
            => await _dbSet.AddAsync(item);   

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdWithTrackingAsync(id);
            if (entity is null) return;
            entity.IsDeleted = true;         
        }

        public IQueryable<T> GetAll()
            => _dbSet.Where(e => !e.IsDeleted);

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
            => GetAll().Where(predicate);

        public async Task<T?> GetByIdAsync(int id)
            => await _dbSet
                .Where(e => !e.IsDeleted && e.Id == id)
                .FirstOrDefaultAsync();

        public async Task<T?> GetByIdWithTrackingAsync(int id)
            => await _dbSet
                .Where(e => !e.IsDeleted && e.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();

        public void Update(T item)
            => _dbSet.Update(item);           

        public void UpdateInclude(T item, params string[] modifiedProperties)
        {
            var entry = _dbContext.ChangeTracker
                .Entries<T>()
                .FirstOrDefault(x => x.Entity.Id == item.Id)
                ?? _dbContext.Entry(item);

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


    }
}
