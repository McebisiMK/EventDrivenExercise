using EventDrivenExercise.Common.DTOs;

namespace EventDrivenExercise.Core.Abstractions
{
    public interface IUserService
    {
        Task<UserDTO> Add(UserDTO user);
        Task Delete(int id);
        Task<UserDTO> Update(UserDTO updatedUser);
    }
}