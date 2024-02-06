using AuthServer.Application.DTOs;
using AuthServer.Core.Entities;
using AutoMapper;

namespace AuthServer.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDTO, ApplicationUser>();
    }
    
}
