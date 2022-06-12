using EventDrivenExercise.Common.DTOs;
using EventDrivenExercise.Common.EventArgurments;
using Microsoft.VisualStudio.Threading;

namespace EventDrivenExercise.Core.Abstractions
{
    public interface IUserService
    {
        event AsyncEventHandler<UserAddedEventArguments> OnUserCreatedEvent;
        event AsyncEventHandler<UserDeletedEventArguments> OnUserDeletedEvent;
        event AsyncEventHandler<UserUpdatedEventArguments> OnUserUpdatedEvent;
        
        Task<UserDTO> Add(UserDTO user);
        Task Delete(int id);
        Task<UserDTO> Update(UserDTO updatedUser);
    }
}