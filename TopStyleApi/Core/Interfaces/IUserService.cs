﻿using TopStyle.Domain.DTO;

namespace TopStyle.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task AddUserAsync(UserDTO user);
        Task UpdateUserAsync(UserDTO userDTO);
        Task DeleteUserAsync(int userId);
    }
}