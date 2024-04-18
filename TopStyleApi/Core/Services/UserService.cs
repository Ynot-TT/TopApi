using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TopStyle.Core.Interfaces;
using TopStyle.Data.Interfaces;
using TopStyle.Data.Repos;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;

namespace TopStyle.Core.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly UserManager <ApplicationUser> _userManager;

        public UserService(IUserRepo repo, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task AddUserAsync(ApplicationUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }
    }
}
