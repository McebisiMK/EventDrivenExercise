using AutoMapper;
using EventDrivenExercise.Common.DTOs;
using EventDrivenExercise.Data.Models.Entities;

namespace EventDrivenExercise.Core.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>()
              .ReverseMap();
        }
    }
}