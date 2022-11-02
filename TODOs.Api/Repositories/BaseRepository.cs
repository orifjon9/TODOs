using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TODOs.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TODOs.Api.Repositories
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class
    {
        private readonly TodoDbContext _dbContext;
        public BaseRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            => _dbContext.Set<TEntity>().Where(predicate).AsNoTracking();

        protected async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        protected async Task<TEntity> EditAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        protected async Task RemoveAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
    }
}
