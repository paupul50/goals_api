using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos;
using goals_api.Dtos.Goals;
using goals_api.Dtos.RequestDto.Goals;
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

        [HttpPost("create")]
        public IActionResult SaveUserGoal([FromBody]GoalDto goalDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            var userGoalsWithTheSameName = _dataContext.Goals.Where(goal => goal.Name == goalDto.Goalname && goal.User == currentUser);
            if (userGoalsWithTheSameName.ToArray().Length > 0)
            {
                return StatusCode(400);
            }

            var userGoal = new Goal {
                Name = goalDto.Goalname,
                User = currentUser,
                CreatedAt = DateTime.Now
            };
            _dataContext.Goals.Add(userGoal);
            var goalProgressToday = new GoalProgress
            {
                Goal = userGoal,
                IsDone = false,
                CreatedAt = DateTime.Now
            };
            _dataContext.GoalProgresses.Add(goalProgressToday);
            _dataContext.SaveChanges();

            userGoal.User.Token = null;
            userGoal.User.Password = null;

            return Ok(userGoal);
        }

        [HttpDelete("{id:int}")]
        public IActionResult RemoveUserGoal(int id)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            var userGoal = _dataContext.Goals.Find(id);
            if (userGoal.User == currentUser)
            {
                var usergoalProgresses = _dataContext.GoalProgresses.Where(progress => progress.Goal == userGoal);
                _dataContext.RemoveRange(usergoalProgresses);
                _dataContext.Goals.Remove(userGoal);
                _dataContext.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(400);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserGoal(int id)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            var userGoal = _dataContext.Goals.Where(goal => goal.User == currentUser && goal.Id == id)
                .Select(goal => new {
                    goal.Id,
                    goal.Name,
                    goal.CreatedAt
                });

            return Ok(userGoal.ToArray()[0]);
        }

        [HttpGet("todayProgress")]
        public IActionResult GetUserGoalWithProgress()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            var goals = _dataContext.Goals.Where(goal => goal.User == currentUser)
                .Select(goal => new GoalWithProgressSegmentDto
                {
                    Id = goal.Id,
                    Name = goal.Name,
                    CreatedAt = goal.CreatedAt,
                    GoalProgressCollection = new List<GoalProgressDto>()
                }).ToList();

            var dateToUseForFiletring = DateTime.Now.Date;

            foreach (var goal in goals)
            {
                goal.GoalProgressCollection = _dataContext.GoalProgresses
                    .Where(goalProgres => goalProgres.CreatedAt.Day == dateToUseForFiletring.Day &&
                    goalProgres.CreatedAt.Month == dateToUseForFiletring.Month &&
                    goalProgres.CreatedAt.Year == dateToUseForFiletring.Year &&
                    goalProgres.Goal.Id == goal.Id)
                    .Select(goalProgress => new GoalProgressDto
                    {
                        Id = goalProgress.Id,
                        IsDone = goalProgress.IsDone,
                        CreatedAt = goalProgress.CreatedAt,
                        IsDummy = false
                    }
                    ).OrderBy(progress => progress.CreatedAt).ToList();
            }

            return Ok(goals);
        }
        // limited to less than a year
        [HttpPost("progressHistory")]
        public IActionResult GetUserGoalsWithProgress([FromBody] GoalWithProgressDto goalWithProgressDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            var goals = _dataContext.Goals.Where(goal => goal.User == currentUser)
                .Select(goal => new GoalWithProgressSegmentDto
                {
                    Id = goal.Id,
                    Name = goal.Name,
                    CreatedAt = goal.CreatedAt,
                    GoalProgressCollection = new List<GoalProgressDto>()
                }).ToList();

            var dateToUseForFiletring = goalWithProgressDto.DateTimeOffset.AddDays(-goalWithProgressDto.DayLimit / 2);

            foreach (var goal in goals)
            {
                goal.GoalProgressCollection = _dataContext.GoalProgresses
                    .Where(goalProgres => goalProgres.CreatedAt > dateToUseForFiletring && goalProgres.Goal.Id == goal.Id )
                    .Take(goalWithProgressDto.DayLimit)
                    .Select(goalProgress => new GoalProgressDto
                        {
                            Id = goalProgress.Id,
                            IsDone = goalProgress.IsDone,
                            CreatedAt = goalProgress.CreatedAt,
                            IsDummy = false
                        }
                    ).OrderBy(progress => progress.CreatedAt).ToList();
            }

            foreach (var goal in goals)
            {
                int counter = 0;
                var goalWithDummyProgress = new List<GoalProgressDto>();
                var startDate = goalWithProgressDto.DateTimeOffset.AddDays(-goalWithProgressDto.DayLimit / 2);
                var endDate = goalWithProgressDto.DateTimeOffset.AddDays(goalWithProgressDto.DayLimit / 2);
                for (DateTime date = startDate; date < endDate; date = date.AddDays(1))
                {
                    if (goal.GoalProgressCollection.Count > counter 
                        && date.Month == goal.GoalProgressCollection.ElementAt(counter).CreatedAt.Month 
                        && date.Day == goal.GoalProgressCollection.ElementAt(counter).CreatedAt.Day)
                    {
                        goalWithDummyProgress.Add(goal.GoalProgressCollection.ElementAt(counter++));
                    }
                    else
                    {
                        goalWithDummyProgress.Add(new GoalProgressDto
                        {
                            CreatedAt = date,
                            IsDummy = true
                        });
                    }
                }
                goal.GoalProgressCollection = goalWithDummyProgress;
            }

            return Ok(goals);
        }
    }
}