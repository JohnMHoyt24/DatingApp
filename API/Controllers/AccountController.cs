//using System;
//using System.Collections.Generic;
using System.Threading.Tasks;
//using System.Linq;
using System.Text;
using System.Security.Cryptography;
using API.Data;
//using API.Data.Migrations.DataContext;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
//using static API.Controllers.BaseApiController;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        //DataContext context = DataContext();
        public AccountController(DataContext context){
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(string username, string password)
        {
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

    }
}