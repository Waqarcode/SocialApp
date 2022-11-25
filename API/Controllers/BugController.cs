using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BugController : BaseApiController
    {
        public DataContext _context;
        public BugController(DataContext context)
        {
            _context = context;
        }
         
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret(string userName)
        {
            return "Secert Text";
        }
        
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound(string userName)
        {
            var obj = _context.Users.Find(-1);
            if(obj == null) return NotFound();

            return Ok(obj);
        }
        
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError(string userName)
        {
             var obj = _context.Users.Find(-1);
             var obj1 = obj.ToString();

            return obj1;
        }
        
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest(string userName)
        {
            return "Bad Request";
        }
        
    }
}