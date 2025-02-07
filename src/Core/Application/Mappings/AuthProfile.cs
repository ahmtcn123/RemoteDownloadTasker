using AutoMapper;
using Core.Application.DTOs;

namespace Core.Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {

        CreateMap<AuthSerivceResponse, AuthResponse>();
    }
}