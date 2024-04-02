using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Data.Entities;
using WebStore.Models.Users;
using System.Linq;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyAppContext _appContext;

        public UserController(MyAppContext appContext)
        {
            _appContext = appContext;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserCreateModel model)
        {
            if (_appContext.Users.Any(u => u.Username == model.Username))
            {
                return BadRequest("Username already exists.");
            }

            if (_appContext.Users.Any(u => u.Email == model.Email))
            {
                return BadRequest("Email already exists.");
            }

            var user = new UserEntity
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email
               
            };

            _appContext.Users.Add(user);
            _appContext.SaveChanges();

            return Ok(new { Message = "User registered successfully", UserId = user.Id });
        }
    }
}

