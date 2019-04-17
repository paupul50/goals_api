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
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                var userGoalsWithTheSameName = _dataContext.Goals.Where(goal => goal.Name == goalDto.Goalname && goal.User == currentUser);
                if (userGoalsWithTheSameName.ToArray().Length > 0)
                {
                    return StatusCode(400);
                }

                Goal userGoal;

                switch (goalDto.GoalType)
                {
                    case 1:
                        userGoal = new Goal
                        {
                            Name = goalDto.Goalname,
                            User = currentUser,
                            CreatedAt = DateTime.Now,
                            GoalType = 1
                        };
                        break;
                    case 2:
                        userGoal = new Goal
                        {
                            Name = goalDto.Goalname,
                            User = currentUser,
                            CreatedAt = DateTime.Now,
                            GoalType = 2,
                            WorkoutId = goalDto.WorkoutId
                        };
                        break;
                    default:
                        return StatusCode(400);
                }

                _dataContext.Goals.Add(userGoal);
                // progress adda
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

                return StatusCode(201);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }


        }

        [HttpDelete("{id:int}")]
        public IActionResult RemoveUserGoal(int id)
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                var userGoal = _dataContext.Goals.Find(id);
                if (userGoal.User == currentUser)
                {
                    var usergoalProgresses = _dataContext.GoalProgresses.Where(progress => progress.Goal == userGoal);
                    _dataContext.RemoveRange(usergoalProgresses);
                    _dataContext.Goals.Remove(userGoal);
                    _dataContext.SaveChanges();
                    return StatusCode(204);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserGoal(int id)
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                var userGoal = _dataContext.Goals.Find(id);

                if (userGoal == null || userGoal.User != currentUser)
                {
                    return StatusCode(204);
                }
                if (userGoal.User == currentUser)
                {
                    userGoal.User.Password = null; // TODO:: return without pass und token
                    userGoal.User.Token = null;
                    return Ok(userGoal);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet("todayProgress")]
        public IActionResult GetUserGoalWithProgress()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var dateToUseForFiletring = DateTime.Now.Date;

                var goals = _dataContext.Goals.Where(g => g.User == currentUser);

                var goalsRetunCollection = new List<object>();

                foreach (var goal in goals)
                {
                    goalsRetunCollection.Add(GetGoalObject(goal, dateToUseForFiletring, currentUser));
                }

                return Ok(goalsRetunCollection);
            }
            catch (Exception e)
            {

                return StatusCode(500);
            }
        }

        private object GetGoalObject(Goal goal, DateTime dateToUseForFiletring, User currentUser)
        {
            GoalProgress goalProgress = _dataContext.GoalProgresses
            .SingleOrDefault(goalProgres => goalProgres.CreatedAt.Day == dateToUseForFiletring.Day &&
            goalProgres.CreatedAt.Month == dateToUseForFiletring.Month &&
            goalProgres.CreatedAt.Year == dateToUseForFiletring.Year &&
            goalProgres.Goal.Id == goal.Id);
            if (goalProgress != null)
            {
                return new
                {
                    goal = new
                    {
                        goal.Id,
                        goal.Name,
                        goal.CreatedAt,
                        goal.GoalType,
                        goal.WorkoutId
                    },
                    goalProgress = new
                    {
                        goalProgress.Id,
                        goalProgress.CreatedAt,
                        goalProgress.IsDone,
                        IsDummy = false

                    }
                };
            }
            else
            {
                var newGoalProgress = new GoalProgress
                {
                    Goal = _dataContext.Goals.Find(goal.Id),
                    IsDone = false,
                    CreatedAt = DateTime.Now
                };
                // today goal progress creation
                _dataContext.GoalProgresses.Add(newGoalProgress);
                _dataContext.SaveChanges();
                return new
                {
                    goal = new
                    {
                        goal.Id,
                        goal.Name,
                        goal.CreatedAt,
                        goal.GoalType,
                        goal.WorkoutId
                    },
                    // TODO: pervadint sita durna pavadinima
                    goalProgressCollection = new
                    {
                        username = currentUser.Username,
                        Id = newGoalProgress.Id,
                        CreatedAt = DateTime.Now,
                        IsDone = false,
                        IsDummy = false

                    }
                };

            }
        }

        // limited to less than a year
        [HttpPost("progressHistory")]
        public IActionResult GetUserGoalsWithProgress([FromBody] GoalWithProgressDto goalWithProgressDto)
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                var goals = _dataContext.Goals.Where(goal => goal.User == currentUser)
                    .Select(goal => new GoalWithProgressSegmentDto
                    {
                        Id = goal.Id,
                        Name = goal.Name,
                        CreatedAt = goal.CreatedAt,
                        GoalProgressCollection = new List<GoalProgressPoco>()
                    }).ToList();

                var dateToUseForFiletring = goalWithProgressDto.DateTimeOffset.AddDays(-goalWithProgressDto.DayLimit / 2);

                foreach (var goal in goals)
                {
                    goal.GoalProgressCollection = _dataContext.GoalProgresses
                        .Where(goalProgres => goalProgres.CreatedAt > dateToUseForFiletring && goalProgres.Goal.Id == goal.Id)
                        .Take(goalWithProgressDto.DayLimit)
                        .Select(goalProgress => new GoalProgressPoco
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
                    var goalWithDummyProgress = new List<GoalProgressPoco>();
                    var startDate = goalWithProgressDto.DateTimeOffset.AddDays(-goalWithProgressDto.DayLimit / 2);
                    var endDate = goalWithProgressDto.DateTimeOffset.AddDays(goalWithProgressDto.DayLimit / 2);
                    var todayDate = DateTime.Now;
                    for (DateTime date = startDate; date < endDate; date = date.AddDays(1))
                    {

                        if (goal.GoalProgressCollection.Count > counter
                            && date.Month == goal.GoalProgressCollection.ElementAt(counter).CreatedAt.Month
                            && date.Day == goal.GoalProgressCollection.ElementAt(counter).CreatedAt.Day
                            && date.Year == goal.GoalProgressCollection.ElementAt(counter).CreatedAt.Year)
                        {
                            goalWithDummyProgress.Add(goal.GoalProgressCollection.ElementAt(counter++));
                        }
                        // today goal progress creation
                        else if (date.Month == todayDate.Month && date.Day == todayDate.Day && date.Year == todayDate.Year)
                        {
                            var goalProgressToday = new GoalProgress
                            {
                                Goal = _dataContext.Goals.Find(goal.Id),
                                IsDone = false,
                                CreatedAt = DateTime.Now
                            };
                            _dataContext.GoalProgresses.Add(goalProgressToday);

                            goalWithDummyProgress.Add(new GoalProgressPoco
                            {
                                Id = goalProgressToday.Goal.Id,
                                CreatedAt = goalProgressToday.CreatedAt,
                                IsDone = goalProgressToday.IsDone,
                                IsDummy = false

                            });
                            _dataContext.SaveChanges();
                        }
                        else
                        {
                            goalWithDummyProgress.Add(new GoalProgressPoco
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
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}