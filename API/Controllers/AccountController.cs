using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public ITokenService _tokenService;
        
        public  AccountController(DataContext context, ITokenService tokenService) 
        { 
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        //RegisterUser Using param Query 
        //public async Task<ActionResult<AppUser>> Register(string username, string password)
        //RegisterUser Using Json Body with Dto Object
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            if( await IsUserExit(input.UserName)) return  BadRequest("user name is exit");

            using var hmac = new HMACSHA512();
            var user = new AppUser(){
                UserName = input.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password)),
                PasswordSalt = hmac.Key                            
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto{
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user) 
            };
        } 

        public async Task<bool> IsUserExit(string userName)
        {
            return await _context.Users.AnyAsync(f=>f.UserName == userName.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(f=> f.UserName == input.UserName.ToLower());
            if(user == null)
                return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password));
            for(int i=0; i< computedHash.Length; i++) {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

             return new UserDto{
                UserName = input.UserName,
                Token = _tokenService.CreateToken(user) 
            };
        }
    }
}