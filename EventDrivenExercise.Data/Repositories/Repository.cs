using System.Linq.Expressions;
using EventDrivenExercise.Data.Abstractions;
using EventDrivenExercise.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenExercise.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly EventDrivenDbContext _eventDrivenDbContext;

        public Repository(EventDrivenDbContext eventDrivenDbContext)
        {
            _eventDrivenDbContext = eventDrivenDbContext;
            _dbSet = _eventDrivenDbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _eventDrivenDbContext.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _eventDrivenDbContext.Remove(entity);
        }

        public void Update(TEntity updatedEntity)
        {
            _eventDrivenDbContext.Update(updatedEntity);
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }
    }
}