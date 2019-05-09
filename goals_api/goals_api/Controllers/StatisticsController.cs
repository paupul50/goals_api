using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace goals_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public StatisticsController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }


        [HttpGet("chart")]
        public IActionResult GetChart()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentGroup = _dataContext.Groups.Include(g => g.Members)
                    .SingleOrDefault(g => g.Members.Contains(currentUser));

                var today = DateTime.Now;

                var goals = _dataContext.Goals.Where(g => g.GoalMedium.User == currentUser);

                var counts = new List<dynamic>();
                var names = new List<dynamic>();
                var userGoals = new object();
                foreach (var goal in goals)
                {
                    counts.Add(_dataContext.GoalProgresses.Where(g => g.IsDone == true && g.CreatedAt >= today.AddDays(-30) && g.Goal == goal).Count());
                    names.Add(goal.Name);
                }

                userGoals = new
                {
                    names,
                    counts
                };

                var countsGroup = new List<dynamic>();
                var namesGroup = new List<dynamic>();
                var groupGoals = new object();
                if (currentGroup != null)
                {
                    var gGoals = _dataContext.Goals.Where(g => g.GoalMedium.Group == currentGroup);
                    foreach (var goal in gGoals)
                    {
                        countsGroup.Add(_dataContext.GoalProgresses.Where(g => g.IsDone == true && g.CreatedAt >= today.AddDays(-30) && g.User == currentUser && g.Goal == goal).Count());
                        namesGroup.Add(goal.Name);
                    }
                    groupGoals = new
                    {
                        names = namesGroup,
                        counts = countsGroup
                    };
                }
                return Ok(new
                {
                    userGoals,
                    groupGoals
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("leaderboard")]
        public IActionResult GetLeaderBoard()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentGroup = _dataContext.Groups.Include(g => g.Members)
                    .SingleOrDefault(g => g.Members.Contains(currentUser));
                var currentMonth = DateTime.Now.Month;
                var leaderboard = new List<dynamic>();
                foreach (var member in currentGroup.Members)
                {
                    leaderboard.Add(new
                    {
                        points = _dataContext.GoalProgresses.Include(gp=>gp.Goal.GoalMedium).Where(g => g.IsDone == true && g.CreatedAt.Month == currentMonth && g.User == member
                        && g.Goal.GoalMedium.Group==currentGroup).Count(),
                        username = member.Username

                    });
                }
                return Ok(leaderboard.OrderByDescending(item => item.points));
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

    }
}