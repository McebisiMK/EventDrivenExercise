using System.Linq.Expressions;
using EventDrivenExercise.Data.Models.Entities;

namespace EventDrivenExercise.Data.Abstractions
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity updatedEntity);
        void Delete(TEntity entity);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression);
    }
}