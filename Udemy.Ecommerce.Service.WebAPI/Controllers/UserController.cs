using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Udemy.Ecommerce.Application.DTO;
using Udemy.Ecommerce.Application.Interface;
using Udemy.Ecommerce.Service.WebAPI.Helpers;
using Udemy.Ecommerce.Transversal.Common;

namespace Udemy.Ecommerce.Service.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserApplication _userApplication;
        private readonly AppSettings _appSettings;

        public UserController(IUserApplication userApplication, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _userApplication = userApplication;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserDTO userDTO)
        {
            var response = _userApplication.Authenticate(userDTO.UserName, userDTO.Password);
            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);
                }
                else
                    return NotFound(response.Message);
            }

            return BadRequest(response.Message);
        }

        private string BuildToken(Response<UserDTO> userDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, userDTO.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}