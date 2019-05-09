﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos;
using goals_api.Dtos.Goals;
using goals_api.Dtos.RequestDto.Goals;
using goals_api.Models;
using goals_api.Models.DataContext;
using goals_api.Models.Goals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var goalMedium = new GoalMedium();
                if (goalDto.IsGroupGoal)
                {
                    goalMedium = _dataContext.GoalMedia.SingleOrDefault(gm => gm.Group.LeaderUsername == currentUser.Username);
                }
                else
                {
                    goalMedium = _dataContext.GoalMedia.SingleOrDefault(gm => gm.User == currentUser);
                }
                if (goalMedium == null)
                {
                    if (goalDto.IsGroupGoal)
                    {
                        var currentGroup = _dataContext.Groups.SingleOrDefault(g => g.LeaderUsername == currentUser.Username); //Include(group=>group.Members).

                        if (currentGroup == null)
                        {
                            StatusCode(204);
                        }

                        goalMedium = new GoalMedium
                        {
                            Group = currentGroup,
                            IsGroupMedium = true
                        };
                        _dataContext.GoalMedia.Add(goalMedium);
                        _dataContext.SaveChanges();
                    }
                    else
                    {
                        goalMedium = new GoalMedium
                        {
                            User = currentUser,
                            IsGroupMedium = false
                        };
                        _dataContext.GoalMedia.Add(goalMedium);
                        _dataContext.SaveChanges();
                    }
                }
                var userGoalsWithTheSameName = _dataContext.Goals.Where(goal => goal.Name == goalDto.Goalname && goal.GoalMedium.User == currentUser);
                if (userGoalsWithTheSameName.ToArray().Length > 0)
                {
                    return StatusCode(400);
                }

                Goal userGoal;

                switch (goalDto.GoalType)
                {
                    case 1:
                        var workout = _dataContext.Workouts.SingleOrDefault(w => w.Id == goalDto.WorkoutId);
                        userGoal = new Goal
                        {
                            Name = goalDto.Goalname,
                            CreatedAt = DateTime.Now,
                            GoalType = 1,
                            GoalMedium = goalMedium,
                            Workout = workout
                        };
                        break;
                    case 2:
                        userGoal = new Goal
                        {
                            Name = goalDto.Goalname,
                            CreatedAt = DateTime.Now,
                            GoalMedium = goalMedium,
                            GoalType = 2,
                            GoalNumberValue = goalDto.GoalNumberValue
                        };
                        break;
                    case 3:
                        userGoal = new Goal
                        {
                            Name = goalDto.Goalname,
                            CreatedAt = DateTime.Now,
                            GoalMedium = goalMedium,
                            GoalType = 3,
                            GoalNumberValue = goalDto.GoalNumberValue

                        };
                        break;
                    case 101:
                        userGoal = new Goal
                        {
                            Name = goalDto.Goalname,
                            CreatedAt = DateTime.Now,
                            GoalMedium = goalMedium,
                            GoalType = 101
                        };
                        break;
                    case 102:
                        userGoal = new Goal
                        {
                            Name = goalDto.Goalname,
                            CreatedAt = DateTime.Now,
                            GoalMedium = goalMedium,
                            GoalNumberValue = goalDto.GoalNumberValue,
                            GoalType = 102
                        };
                        break;
                    case 203:
                        userGoal = new Goal
                        {
                            Name = goalDto.Goalname,
                            CreatedAt = DateTime.Now,
                            GoalMedium = goalMedium,
                            GoalType = 101
                            // TODO: Atsitiktinio siekio isrinkimas
                        };
                        break;
                    default:
                        return StatusCode(400);
                }

                _dataContext.Goals.Add(userGoal);
                // progress adda
                if (!goalMedium.IsGroupMedium)
                {
                    var goalProgressToday = new GoalProgress
                    {
                        Goal = userGoal,
                        IsDone = false,
                        CreatedAt = DateTime.Now,
                        User = currentUser
                    };
                    _dataContext.GoalProgresses.Add(goalProgressToday);
                }

                _dataContext.SaveChanges();

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
                var userGoal = _dataContext.Goals.Include(g => g.GoalMedium).SingleOrDefault(g => g.GoalMedium.User == currentUser && g.Id == id);
                if (userGoal.GoalMedium.User == currentUser)
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
                var userGoal = _dataContext.Goals.Include(g => g.GoalMedium).SingleOrDefault(g => g.GoalMedium.User == currentUser && id == g.Id && !g.GoalMedium.IsGroupMedium);

                if (userGoal == null || userGoal.GoalMedium.User != currentUser)
                {
                    return StatusCode(204);
                }
                if (userGoal.GoalMedium.User == currentUser)
                {
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

                var goals = _dataContext.Goals.Include(g=>g.Workout).Where(g => g.GoalMedium.User == currentUser);

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
                    goal,
                    goalProgress
                };
            }
            else
            {
                var newGoalProgress = new GoalProgress
                {
                    Goal = _dataContext.Goals.Find(goal.Id),
                    IsDone = false,
                    CreatedAt = DateTime.Now,
                    User = currentUser
                };
                // today goal progress creation
                _dataContext.GoalProgresses.Add(newGoalProgress);
                _dataContext.SaveChanges();
                return new
                {
                    goal,
                    goalProgressCollection = newGoalProgress
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
                var goals = _dataContext.Goals.Where(goal => goal.GoalMedium.User == currentUser)
                    .Select(goal => new GoalWithProgressSegmentDto
                    {
                        Id = goal.Id,
                        Name = goal.Name,
                        CreatedAt = goal.CreatedAt,
                        GoalNumberValue = goal.GoalNumberValue,
                        GoalType = goal.GoalType,
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
                            GoalNumberValue = goalProgress.GoalNumberValue,
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
                                CreatedAt = DateTime.Now,
                                User = currentUser
                            };
                            _dataContext.GoalProgresses.Add(goalProgressToday);

                            goalWithDummyProgress.Add(new GoalProgressPoco
                            {
                                Id = goalProgressToday.Goal.Id,
                                CreatedAt = goalProgressToday.CreatedAt,
                                IsDone = goalProgressToday.IsDone,
                                GoalNumberValue = goalProgressToday.GoalNumberValue,
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