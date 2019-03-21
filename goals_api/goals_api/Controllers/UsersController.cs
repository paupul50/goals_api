using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos;
using goals_api.Dtos.User;
using goals_api.Models;
using goals_api.Models.DataContext;
using goals_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace goals_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private DataContext _dataContext;

        public UsersController(IUserService userService, DataContext dataContext)
        {
            _userService = userService;
            _dataContext = dataContext;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserLoginDto userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody]UserCreateDto userCreateDto)
        {
            // TODO:: try catch, kas jeigu jau toks vartotojas yra handle
            var newUser = new User {
                Username = userCreateDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password),
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName
            };
            _dataContext.Users.Add(newUser);
            _dataContext.SaveChanges();

            return Ok(newUser);
        }

        [HttpDelete("logout")]
        public IActionResult LogoutUser()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            currentUser.Token = null;
            _dataContext.Users.Update(currentUser);
            _dataContext.SaveChanges();

            return Ok();
        }
    }
}
