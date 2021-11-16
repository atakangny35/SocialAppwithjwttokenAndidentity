using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model;
using WebApplication1.Model.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<User> userManager;
        private SignInManager<User> SignInManager;
        private readonly IConfiguration configuration;
        public UserController(UserManager<User> _userManager,SignInManager<User> _signInManager,IConfiguration _configration)
        {
            this.userManager = _userManager;
            this.SignInManager = _signInManager;
            configuration = _configration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto _user)
        {
            var user = new User
            {
                UserName = _user.UserName,
                Email = _user.Email,
                Gender = _user.Gender,
                Name = _user.Name,
                Sysdate = DateTime.Now,
                LastActive = DateTime.Now

            };
            var result = await userManager.CreateAsync(user,_user.Password);

            if (result.Succeeded)
            {
                return StatusCode(201);
            }

            return BadRequest(result.Errors);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if(user == null)
            {
                return BadRequest(new { message ="username is incorrect!"});
            }
            var result = await SignInManager.CheckPasswordSignInAsync(user,model.Password,false);

            if (result.Succeeded)
            {
                return Ok(new 
                {   
                    token=GenerateJwtToken(user),
                    
                });
            }
            return Unauthorized();
        }
        
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {

                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),

                }),
                Expires = DateTime.UtcNow.AddDays(1),
               
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}
