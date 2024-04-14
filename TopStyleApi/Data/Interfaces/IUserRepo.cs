using TopStyle.Domain.Entities;

namespace TopStyle.Data.Interfaces
{
    public interface IUserRepo
    {
        Task <IEnumerable<User>> GetAllUsersAsync();
        Task <User> GetUserByIdAsync(int userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);

    }
}
