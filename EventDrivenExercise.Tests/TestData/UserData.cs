using EventDrivenExercise.Common.DTOs;
using EventDrivenExercise.Data.Models.Entities;

namespace EventDrivenExercise.Tests.TestData
{
    public class UserData
    {
        public static UserDTO GetTestUserDTO()
        {
            return new UserDTO
            {
                Id = 1,
                FirstName = "Test User",
                LastName = "Last Name",
                IdNumber = "1234567893456",
                Email = "testuser@test.com"
            };
        }

        public static User GetTestUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "Test User",
                LastName = "Last Name",
                IdNumber = "1234567893456",
                Email = "testuser@test.com"
            };
        }

        public static UserDTO GetInvalidUser()
        {
            return new UserDTO
            {
                FirstName = "Test",
                Email = "email@string.com",
                IdNumber = "1234"
            };
        }
    }
}