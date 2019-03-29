using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto.Comment;
using goals_api.Models;
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
    public class CommentController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CommentController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CommentDto commentDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            var currentUserDescription = _dataContext.UserDescriptions.Single(ud => ud.username == currentUser.Username);

            try
            {
                Comment newComment = new Comment();
                switch (commentDto.CommentTarget)
                {
                    case "profile":
                        newComment.Body = commentDto.Body;
                        newComment.CreatedAt = DateTime.Now;
                        newComment.Username = currentUser.Username;
                        newComment.CommentUserDescriptionId = currentUserDescription.Id;

                        var tartgetUserDescription = _dataContext.UserDescriptions.Include(ud => ud.Comments).Single( ud => ud.Id == commentDto.CommentTargetId);
                        tartgetUserDescription.Comments.Add(newComment);

                        _dataContext.SaveChanges();
                        break;
                    default:
                        return NoContent();
                }

                return Ok(newComment);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
