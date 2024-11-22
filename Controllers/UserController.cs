using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication_C14.Dto;
using WebApplication_C14.Entities;
using WebApplication_C14.interfaces;
using WebApplication_C14.server;

namespace WebApplication_C14.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        List<Claim> claims = new List<Claim>();
        public UserDb _context = new UserDb();
        public UserEntity _entity = new UserEntity();
        public IUserPRP _service;

        public UserController (IUserPRP service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        public string Login (string username,string password) => _service.Login(username, password);   

   
        [HttpPost("register")]
        public UserEntity Post([FromBody] UserEntity input) => _service.Register(input);
        [HttpGet("showProfile{id}")]
        [Authorize]
        public UserEntity Get(int id) => _service.ShowProfile(id);
        [HttpDelete]
        public bool Delete(int id) => _service.Delete(id);
    }
}
