using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
        public async Task<IActionResult> GetCurrentUserDescription()
        {
            try
            {
                var user = await _dataContext.Users.FindAsync(User.Identity.Name);
                var userComments = _dataContext.Comments.Include(c => c.AuthorUsername).Where(c => c.CommentedUser == user);
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

        [HttpPost("other")]
        public async Task<IActionResult> GetUserDescription(UserDescriptionDto userDescriptionDto)
        {
            try
            {
                var user = await _dataContext.Users.FindAsync(userDescriptionDto.Username);
                if(user == null)
                {
                    return StatusCode(204);
                }
                var userComments = _dataContext.Comments.Include(c=>c.AuthorUsername).Where(c => c.CommentedUser == user);
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

        [HttpPost("edit")]
        public async Task<IActionResult> EditUserDescription([FromForm] UserDescriptionEditDto userDescriptionEditDto)
        {
            try
            {
                var user = await _dataContext.Users.FindAsync(User.Identity.Name);

                user.Firstname = userDescriptionEditDto.Firstname;
                user.Lastname = userDescriptionEditDto.Lastname;
                user.Description = userDescriptionEditDto.Description;

                if (userDescriptionEditDto.File != null && userDescriptionEditDto.File.FileName != "integration")
                {
                    var dbPath = UploadImage(userDescriptionEditDto.File);

                    if (dbPath == "")
                    {
                        return StatusCode(404);
                    }

                    user.Image = dbPath;
                }

                await _dataContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        private string UploadImage(IFormFile uploadedFile)
        {
            var file = uploadedFile;
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = +DateTime.Now.Ticks / 10000 + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return dbPath;
            }

            return "";
        }
    }
}