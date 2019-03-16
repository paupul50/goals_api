using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos;
using goals_api.Models;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace goals_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GoalsController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpPost]
        public IActionResult SaveUserGoal([FromBody]GoalDto goal)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            var userGoal = new Goal {
                Name = goal.Goalname,
                User = currentUser
            };
            _dataContext.Goals.Add(userGoal);
            _dataContext.SaveChanges();

            userGoal.User.Token = null;
            userGoal.User.Password = null;

            return Ok(userGoal);
        }

        [HttpGet]
        public IActionResult GetUserGoals()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            var goals = _dataContext.Goals.Where( goal=> goal.User == currentUser);

            return Ok(goals);
        }
    }
}