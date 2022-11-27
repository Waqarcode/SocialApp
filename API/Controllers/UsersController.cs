using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        // [HttpGet("{id}")]
        // public async Task<ActionResult<AppUser>> Find(int id)
        // {
        //     return await _userRepository.GetUserByIdAsync(id);
        // }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserOutputDto>> GetUserByName(string userName)
        {
            //var query = await _userRepository.GetUserByUserNameAsync(userName);
            //return  _mapper.Map<UserOutputDto>(query);
            
            return await _userRepository.GetUserByUserNameOptimizWayAsync(userName);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOutputDto>>> GetUsers()
        {
            // var query = await _userRepository.GetUserAsync();
            // return Ok(_mapper.Map<IEnumerable<UserOutputDto>>(query));
            
            return Ok(await _userRepository.GetUserOptimizWayAsync());
        }
    }
}