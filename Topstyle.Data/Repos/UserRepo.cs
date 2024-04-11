using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopStyle.Data.Interfaces;
using TopStyle.Domain.Entities;

namespace TopStyle.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly TopStyleContext _context;

        public UserRepo(TopStyleContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task UpdateUserAsync(User user)
        {
            var dbUser = await _context.Users.FindAsync(user.UserId);

            dbUser.Username = user.Username;
            dbUser.Password = user.Password;
            await _context.SaveChangesAsync();
        }
    }
}
