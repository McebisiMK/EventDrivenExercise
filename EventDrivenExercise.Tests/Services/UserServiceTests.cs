using System.Linq.Expressions;
using AutoMapper;
using EventDrivenExercise.Common.DTOs;
using EventDrivenExercise.Common.EventArgurments;
using EventDrivenExercise.Common.Exceptions;
using EventDrivenExercise.Core.Mapping;
using EventDrivenExercise.Core.Services;
using EventDrivenExercise.Data.Abstractions;
using EventDrivenExercise.Data.Models.Entities;
using EventDrivenExercise.Tests.TestData;
using FluentAssertions;
using NSubstitute;

namespace EventDrivenExercise.Tests.Services
{

    [TestFixture]
    public class UserServiceTests
    {
        private static IMapper _mapper;

        public UserServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mapConfig =>
                {
                    mapConfig.AddProfile(new UserProfile());
                });

                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Test]
        public async Task Add_Given_Invalid_User_Should_Throw_Exception_And_Not_Raise_An_Event()
        {
            //-------------------------------Arrange------------------------------
            var userWithNoLastName = UserData.GetInvalidUser();
            var expectedErrorMessage = "Add user. Error: All user properties must have a valid value";
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userService = GetUserService(unitOfWork);
            var userCreatedEventRaised = false;

            userService.OnUserCreatedEvent += async (s, e) => { userCreatedEventRaised = true; };

            //-------------------------------Act----------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserException>(() => userService.Add(userWithNoLastName));

            //-------------------------------Assert-------------------------------
            exception.Message.Should().Be(expectedErrorMessage);
            await unitOfWork.UserRepository.Received(0).AddAsync(Arg.Any<User>());
            userCreatedEventRaised.Should().BeFalse();
        }

        [Test]
        public async Task Add_Given_Valid_User_Should_Return_Added_User_And_Raise_User_Created_Event()
        {
            //-----------------------------------Arrange-----------------------------------------
            var user = UserData.GetTestUserDTO();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userService = GetUserService(unitOfWork);
            var userCreatedEventRaised = false;

            userService.OnUserCreatedEvent += async (s, e) => { userCreatedEventRaised = true; };

            //-----------------------------------Act----------------------------------------------
            var actual = await userService.Add(user);

            //-----------------------------------Assert--------------------------------------------
            actual.Should().BeEquivalentTo(user);
            await unitOfWork.UserRepository.Received(1).AddAsync(Arg.Any<User>());
            userCreatedEventRaised.Should().BeTrue();
        }

        [Test]
        public async Task Delete_Given_User_Does_Not_Exist_Should_Throw_Exception_And_Not_Raise_An_Event()
        {
            //-----------------------------------Arrange--------------------------------------------
            var userId = 10;
            var expectedErrorMessage = "Delete user. Error: User does not exist";
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userService = GetUserService(unitOfWork);
            var userDeletedEventRaised = false;

            userService.OnUserDeletedEvent += async (s, e) => { userDeletedEventRaised = true; };
            unitOfWork.UserRepository.Exists(Arg.Any<Expression<Func<User, bool>>>()).Returns(false);

            //-----------------------------------Act-----------------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserException>(() => userService.Delete(userId));

            //-----------------------------------Assert--------------------------------------------
            exception.Message.Should().Be(expectedErrorMessage);
            unitOfWork.UserRepository.Received(0).Delete(Arg.Any<User>());
            userDeletedEventRaised.Should().BeFalse();
        }

        [Test]
        public async Task Delete_Given_The_User_Exists_Should_Delete_The_User_And_Raise_User_Deleted_Event()
        {
            //-----------------------------------Arrange--------------------------------------------
            var user = UserData.GetTestUser();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userService = GetUserService(unitOfWork);
            var userDeletedEventRaised = false;

            userService.OnUserDeletedEvent += async (s, e) => { userDeletedEventRaised = true; };
            unitOfWork.UserRepository.Exists(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);

            //-----------------------------------Act---------------------------------------------------
            await userService.Delete(1);

            //-----------------------------------Assert-------------------------------------------------
            unitOfWork.UserRepository.Received(1).Delete(Arg.Any<User>());
            userDeletedEventRaised.Should().BeTrue();
        }

        [Test]
        public async Task Update_Given_User_Does_Not_Exist_Should_Throw_Exception_And_Not_Raise_An_Event()
        {
            //-----------------------------------Arrange--------------------------------------------
            var updatedUser = UserData.GetTestUserDTO();
            var expectedErrorMessage = "Update user. Error: User does not exist";
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userService = GetUserService(unitOfWork);
            var userUpdatedEventRaised = false;

            userService.OnUserUpdatedEvent += async (s, e) => { userUpdatedEventRaised = true; };
            unitOfWork.UserRepository.Exists(Arg.Any<Expression<Func<User, bool>>>()).Returns(false);

            //-----------------------------------Act-----------------------------------------------
            var exception = Assert.ThrowsAsync<InvalidUserException>(() => userService.Update(updatedUser));

            //-----------------------------------Assert--------------------------------------------
            exception.Message.Should().Be(expectedErrorMessage);
            unitOfWork.UserRepository.Received(0).Update(Arg.Any<User>());
            userUpdatedEventRaised.Should().BeFalse();
        }

        [Test]
        public async Task Update_Given_The_User_Exists_Should_Update_The_User_And_Raise_User_Updated_Event()
        {
            //-----------------------------------Arrange--------------------------------------------
            var user = UserData.GetTestUserDTO();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userService = GetUserService(unitOfWork);
            var userUpdatedEventRaised = false;

            userService.OnUserUpdatedEvent += async (s, e) => { userUpdatedEventRaised = true; };
            unitOfWork.UserRepository.Exists(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);

            //-----------------------------------Act---------------------------------------------------
            var actual = await userService.Update(user);

            //-----------------------------------Assert-------------------------------------------------
            actual.Should().BeEquivalentTo(user);
            unitOfWork.UserRepository.Received(1).Update(Arg.Any<User>());
            userUpdatedEventRaised.Should().BeTrue();
        }

        private static UserService GetUserService(IUnitOfWork unitOfWork)
        {
            return new UserService(unitOfWork, _mapper);
        }
    }
}