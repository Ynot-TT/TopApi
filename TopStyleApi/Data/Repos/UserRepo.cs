using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopStyle.Data.Interfaces;
using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;

namespace TopStyle.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly TopStyleContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepo(TopStyleContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddUserAsync(ApplicationUser user, string password)
        {
             await _userManager.CreateAsync(user, password);
            
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public Task DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            //var users = await _context.Users.ToListAsync();
            //return users;
            throw new Exception();
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            //var user = await _context.Users.FindAsync(userId);
            //if (user == null)
            //{
            //    return null;
            //}
            //return user;
            throw new Exception();
        }

        public Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
        //public async Task UpdateUserAsync(ApplicationUser user)
        //{
        //    var dbUser = await _context.Users.FindAsync(user.UserId);

        //    dbUser.Username = user.Username;
        //    dbUser.Password = user.Password;
        //    await _context.SaveChangesAsync();
        //}
        //public async Task<User> GetUserByUsernameAsync(string username) // Implement this method
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        //}
    }
}
