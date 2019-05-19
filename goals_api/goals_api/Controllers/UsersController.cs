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
        private IGoogleApiService _googleApiService;

        public UsersController(IUserService userService, DataContext dataContext, IGoogleApiService googleApiService)
        {
            _userService = userService;
            _dataContext = dataContext;
            _googleApiService = googleApiService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserLoginDto userParam)
        {
            try
            {
                var user = _userService.Authenticate(userParam.Username, userParam.Password);

                if (user == null)
                    return StatusCode(400);
                object userResponseObject;

                var result = await _googleApiService.UpdateAllData(user);
                var isGoogleLogged = result == "session_end" || user.GoogleToken == null ? false : true;
                userResponseObject = new
                {
                    isGoogleLogged,
                    user.Token,
                    user.Username
                };

                return Ok(userResponseObject);
            }
            catch (Exception )
            {
                return Ok();
            }
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody]UserCreateDto userCreateDto)
        {
            try
            {
                var existingUser = _dataContext.Users.Find(userCreateDto.Username);
                if (existingUser != null)
                {
                    return StatusCode(409);
                }

                var newUser = new User
                {
                    Username = userCreateDto.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password),
                    Firstname = userCreateDto.Firstname,
                    Lastname = userCreateDto.Lastname,
                    CreatedAt = DateTime.Now,
                    Email = userCreateDto.Email
                };
                _dataContext.Users.Add(newUser);
                _dataContext.SaveChanges();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpDelete("logout")]
        public IActionResult LogoutUser()
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                currentUser.Token = null;
                currentUser.GoogleToken = null;
                _dataContext.Users.Update(currentUser);
                _dataContext.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
