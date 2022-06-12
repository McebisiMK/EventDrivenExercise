using EventDrivenExercise.Data.Abstractions;
using EventDrivenExercise.Data.Models;
using EventDrivenExercise.Data.Models.Entities;

namespace EventDrivenExercise.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepository<User> _userRepository;
        private readonly EventDrivenDbContext _eventDrivenDbContext;
        private readonly IRepository<UserAuditLog> _userAuditLogRepository;

        public UnitOfWork(EventDrivenDbContext eventDrivenDbContext, IRepository<User> userRepository, IRepository<UserAuditLog> userAuditLogRepository)
        {
            _eventDrivenDbContext = eventDrivenDbContext;
            _userRepository = userRepository;
            _userAuditLogRepository = userAuditLogRepository;
        }

        public IRepository<User> UserRepository
        {
            get { return _userRepository ?? new Repository<User>(_eventDrivenDbContext); }
        }

        public IRepository<UserAuditLog> UserAuditLogRepository
        {
            get { return _userAuditLogRepository ?? new Repository<UserAuditLog>(_eventDrivenDbContext); }
        }

        public void Dispose()
        {
            _eventDrivenDbContext.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _eventDrivenDbContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}