using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;

namespace API.Repository
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<IEnumerable<AppUser>> GetUserAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUserNameAsync(string userName);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<UserOutputDto>> GetUserOptimizWayAsync();
        Task<UserOutputDto> GetUserByUserNameOptimizWayAsync(string userName);
    }
}