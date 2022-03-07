using library_management_REST_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace library_management_REST_API.Services
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager; 

        public UserService(UserManager<IdentityUser> userManager )
        {
            _userManager = userManager; 
        }

        public async Task<IdentityUser> getUserByUsernameAsync(string Username)
        {
            return await _userManager.FindByNameAsync(Username);
        }

        public async Task<IdentityUser> getUserFromTokenAsync(HttpContext ctx)
        {
            // var userId = HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;
            return await _userManager.GetUserAsync(ctx.User);
        }
    }
}
