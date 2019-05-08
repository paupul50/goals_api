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
            var otherUser = _dataContext.Users.Find(commentDto.CommentedUser);

            try
            {
                Comment newComment = new Comment();
                switch (commentDto.CommentTarget)
                {
                    case "profile":
                        newComment.Body = commentDto.Body;
                        newComment.CreatedAt = DateTime.Now;
                        newComment.AuthorUsername = currentUser;
                        newComment.CommentedUser = otherUser;

                        _dataContext.Comments.Add(newComment);
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
