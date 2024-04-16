using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;

namespace TopStyle.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task AddUserAsync(ApplicationUser user, string password);
        Task UpdateUserAsync(UserDTO userDTO);
        Task DeleteUserAsync(string userId);

    }
}
