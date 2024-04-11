using AutoMapper;
using TopStyle.Core.Interfaces;
using TopStyle.Data.Interfaces;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;

namespace TopStyle.Core.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddUserAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            await _repo.AddUserAsync(user);
        }
        public async Task DeleteUserAsync(int userId)
        {
            await _repo.DeleteUserAsync(userId);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _repo.GetUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            var userToUpdate = _mapper.Map<User>(userDTO);
            await _repo.UpdateUserAsync(userToUpdate);
        }
    }
}
