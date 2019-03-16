﻿using System;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _dataContext.Users.Take(10);

            return Ok(users);
        }
    }
}
