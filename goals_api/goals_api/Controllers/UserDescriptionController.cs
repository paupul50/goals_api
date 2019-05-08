using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto.User;
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
                var user = _dataContext.Users.Find(User.Identity.Name);
                // currentUser.UserDescription = _dataContext.UserDescriptions.Include(ud => ud.Comments).Single(ud => ud.Id == currentUser.DescriptionId);
                var userComments = _dataContext.Comments.Where(c => c.CommentedUser == user);
                return Ok(new {
                    user,
                    userComments
                });
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult GetUserDescription(UserDescriptionDto userDescriptionDto)
        {
            try
            {
                var user = _dataContext.Users.Find(userDescriptionDto.Username);
                if(user == null)
                {
                    return StatusCode(404);
                }
                // currentUser.UserDescription = _dataContext.UserDescriptions.Include(ud => ud.Comments).Single(ud => ud.Id == currentUser.DescriptionId);
                var userComments = _dataContext.Comments.Where(c => c.CommentedUser == user);
                return Ok(new
                {
                    user,
                    userComments
                });
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}