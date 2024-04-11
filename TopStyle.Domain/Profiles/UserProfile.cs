using AutoMapper;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;

namespace TopStyle.Domain.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
