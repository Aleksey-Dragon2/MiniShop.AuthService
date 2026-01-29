using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MiniShop.AuthService.Application.Users.Results;
using MiniShop.AuthService.Domain.Entities;

namespace MiniShop.AuthService.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterUserResult>().ReverseMap();
        }
    }
}
