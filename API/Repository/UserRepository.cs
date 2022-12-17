using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            
            //return await _context.Users.Include(x=>x.Photos).ToListAsync(); //eager Loading (with circular reference problem)
            //Eager Loading With Out Refernce Problem Bcu We are using AutoMapper
            return await _context.Users.Include(x=>x.Photos).ToListAsync();
            //return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        
        
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<AppUser> GetUserByUserNameAsync(string userName)
        {
            // return await _context.Users 
            //         .Include(e=> e.Photos) //Eager Loading (with circular reference problem)
            //         .SingleOrDefaultAsync(x => x.UserName == userName);
            
            return await _context.Users 
                    .Include(e=> e.Photos) //Eager Loading With Out Refernce Problem Bcu We are using AutoMapper
                    .FirstOrDefaultAsync(x => x.UserName == userName);
            
            //return await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName); Photo null
        }

        public async Task<UserOutputDto> GetUserByUserNameOptimizWayAsync(string userName)
        {
            return await _context.Users
                    .Where(f => f.UserName == userName)
                    .ProjectTo<UserOutputDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserOutputDto>> GetUserOptimizWayAsync()
        {
           return await _context.Users
                    .ProjectTo<UserOutputDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

    }
}