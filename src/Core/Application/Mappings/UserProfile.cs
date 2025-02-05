using AutoMapper;
using Core.Application.DTOs;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    namespace Mappings
    {
        public class UserProfile : Profile
        {
            public UserProfile()
            {
                CreateMap<AddUserDTO, User>() // Map from AddUserDTO to User model
                    .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id field

                CreateMap<RegisterRequest, AddUserDTO>() // Map from RegisterRequest to AddUserDTO
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.User)); // Set Role to User
            }
        }
    }
}