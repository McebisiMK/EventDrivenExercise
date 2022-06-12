using EventDrivenExercise.Data.Models.Entities;

namespace EventDrivenExercise.Data.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<User> UserRepository { get; }
        public IRepository<UserAuditLog> UserAuditLogRepository { get; }

        Task SaveChangesAsync();
    }
}