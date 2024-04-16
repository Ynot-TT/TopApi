using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;

namespace TopStyle.Data.Interfaces
{
    public interface IUserRepo
    {
        Task <IEnumerable<User>> GetAllUsersAsync();
        Task <ApplicationUser> GetUserByIdAsync(string userId);
        Task AddUserAsync(ApplicationUser user, string password);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string userId);
        Task<ApplicationUser> GetUserByUsernameAsync(string username);

    }
}
