using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto;
using goals_api.Dtos.RequestDto.User;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace goals_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GoogleFitController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GoogleFitController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] GoogleAccessDto googleAccessDto)
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                currentUser.GoogleStatus = googleAccessDto.State;
                currentUser.GoogleToken = googleAccessDto.Token;
                _dataContext.Users.Update(currentUser);
                _dataContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        //private string decodeUserId(string state)
        //{
        //    var otherState = state.Split(";")[1];
        //    string incoming = otherState
        //        .Replace('_', '/').Replace('-', '+');
        //    switch (otherState.Length % 4)
        //    {
        //        case 2: incoming += "=="; break;
        //        case 3: incoming += "="; break;
        //    }
        //    byte[] bytes = Convert.FromBase64String(incoming);
        //    return Encoding.ASCII.GetString(bytes);
        //}
    }
}