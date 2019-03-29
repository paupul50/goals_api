using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace goals_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDescriptionController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserDescriptionController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetCurrentUserDescription()
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                // currentUser.UserDescription = _dataContext.UserDescriptions.Include(ud => ud.Comments).Single(ud => ud.Id == currentUser.DescriptionId);
                var userDescription = _dataContext.UserDescriptions.Include(ud => ud.Comments).Single(ud => ud.username == User.Identity.Name);
                return Ok(userDescription);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserDescription(int id)
        {
            try
            {
                var userDescription = _dataContext.UserDescriptions.Include(ud => ud.Comments).Single(ud => ud.Id == id);
                return Ok(userDescription);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}