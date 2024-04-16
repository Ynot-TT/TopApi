using AutoMapper;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;
using TopStyleApi.Domain.DTO;

namespace TopStyle.Domain.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserDTO, ApplicationUser>();

            CreateMap<ApplicationUser, UserLogInDTO>();
            CreateMap<UserLogInDTO, ApplicationUser>();
        }
    }
}
