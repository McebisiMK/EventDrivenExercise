using EventDrivenExercise.Common.Enums;
using EventDrivenExercise.Common.EventArgurments;
using EventDrivenExercise.Core.Abstractions;
using EventDrivenExercise.Data.Abstractions;
using EventDrivenExercise.Data.Models.Entities;
using Newtonsoft.Json;

namespace EventDrivenExercise.Core.Services
{
    public class UserAuditLogService : IUserAuditLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAuditLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Subscribe(IUserService userService)
        {
            userService.OnUserCreatedEvent += OnUserCreated;
            userService.OnUserDeletedEvent += OnUserDeleted;
            userService.OnUserUpdatedEvent += OnUserUpdated;
        }

        private async Task OnUserUpdated(object? sender, UserUpdatedEventArguments eventArgs)
        {
            var eventData = new UserAuditLog
            {
                Type = EventType.Updated.ToString(),
                Event = JsonConvert.SerializeObject(eventArgs)
            };
            
            await _unitOfWork.UserAuditLogRepository.AddAsync(eventData);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task OnUserDeleted(object? sender, UserDeletedEventArguments eventArgs)
        {
            var eventData = new UserAuditLog
            {
                Type = EventType.Deleted.ToString(),
                Event = JsonConvert.SerializeObject(eventArgs.DeletedUser)
            };
            
            await _unitOfWork.UserAuditLogRepository.AddAsync(eventData);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task OnUserCreated(object? sender, UserAddedEventArguments eventArgs)
        {
            var eventData = new UserAuditLog
            {
                Type = EventType.Created.ToString(),
                Event = JsonConvert.SerializeObject(eventArgs.User)
            };

            await _unitOfWork.UserAuditLogRepository.AddAsync(eventData);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}