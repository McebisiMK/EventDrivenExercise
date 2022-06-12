using AutoMapper;
using EventDrivenExercise.Common.DTOs;
using EventDrivenExercise.Common.EventArgurments;
using EventDrivenExercise.Common.Exceptions;
using EventDrivenExercise.Core.Abstractions;
using EventDrivenExercise.Data.Abstractions;
using EventDrivenExercise.Data.Models.Entities;

namespace EventDrivenExercise.Core.Services
{
    public class UserService : IUserService
    {
        public event EventHandler<UserAddedEventArguments> OnUserCreatedEvent;
        public event EventHandler<UserUpdatedEventArguments> OnUserUpdatedEvent;
        public event EventHandler<UserDeletedEventArguments> OnUserDeletedEvent;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> Add(UserDTO user)
        {
            try
            {
                var newUser = _mapper.Map<User>(user);
                await _unitOfWork.UserRepository.AddAsync(newUser);
                await _unitOfWork.SaveChangesAsync();
                user.Id = newUser.Id;

                OnUserCreated(user);
            }
            catch (Exception exception)
            {
                LogException("Add user", exception);
            }

            return user;
        }

        public async Task Delete(int id)
        {
            try
            {
                var userExists = _unitOfWork.UserRepository.Exists(user => user.Id.Equals(id));

                if (!userExists)
                    throw new InvalidUserException("User does not exist");

                var existingUser = _unitOfWork.UserRepository.GetBy(user => user.Id.Equals(id)).FirstOrDefault();
                _unitOfWork.UserRepository.Delete(existingUser);
                await _unitOfWork.SaveChangesAsync();

                var user = _mapper.Map<UserDTO>(existingUser);
                OnUserDeleted(user);
            }
            catch (Exception exception)
            {
                LogException("Delete user", exception);
            }
        }

        public async Task<UserDTO> Update(UserDTO updatedUser)
        {
            try
            {
                var userExists = _unitOfWork.UserRepository.Exists(user => user.Id.Equals(updatedUser.Id));

                if (!userExists)
                    throw new InvalidUserException("User does not exist");

                var oldUser = _unitOfWork.UserRepository.GetBy(user => user.Id.Equals(updatedUser.Id)).FirstOrDefault();
                var user = _mapper.Map<User>(updatedUser);

                _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();

                OnUserUpdated(oldUser, updatedUser);
            }
            catch (Exception exception)
            {
                exception = exception.InnerException ?? exception;
                throw new InvalidUserException($"Update user. Error: {exception.Message}");
            }

            return updatedUser;
        }

        protected virtual void OnUserCreated(UserDTO user)
        {
            OnUserCreatedEvent?.Invoke(this, new UserAddedEventArguments { User = user });
        }

        protected virtual void OnUserDeleted(UserDTO user)
        {
            OnUserDeletedEvent?.Invoke(this, new UserDeletedEventArguments { DeletedUser = user });
        }

        protected virtual void OnUserUpdated(User oldUser, UserDTO user)
        {
            OnUserUpdatedEvent?.Invoke
            (
                this,
                new UserUpdatedEventArguments
                {
                    PreviousUser = _mapper.Map<UserDTO>(oldUser),
                    CurrentUser = user
                }
            );
        }

        private void LogException(string operation, Exception exception)
        {
            exception = exception.InnerException ?? exception;
            throw new InvalidUserException($"{operation}. Error: {exception.Message}");
        }
    }
}