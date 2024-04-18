using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;

namespace TopStyle.Core.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(ApplicationUser user, string password);
    }
}
