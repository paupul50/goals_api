using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Constants;
using goals_api.Dtos;
using goals_api.Dtos.RequestDto.GoalProgress;
using goals_api.Dtos.RequestDto.Group;
using goals_api.Models;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace goals_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupGoalProgressController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GroupGoalProgressController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        // for user
        [HttpGet]
        public IActionResult GetUserGoalWithProgress()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                //var currentUserGroup = _dataContext.Groups.SingleOrDefault(g => g.LeaderUsername == currentUser.Username);

                //if (currentUserGroup == null)
                //{
                var currentUserGroup = _dataContext.Groups.SingleOrDefault(group => group.Members.Contains(currentUser));
                if (currentUserGroup == null)
                {
                    return StatusCode(204);
                }
                //currentUserGroup = userGroup;
                //}
                var dateToUseForFiletring = DateTime.Now.Date;

                var groupGoals = _dataContext.Goals.Include(g=>g.Workout).Where(goal => goal.GoalMedium.Group == currentUserGroup).ToList();

                var groupGoalsRetunCollection = new List<object>();

                foreach (var goal in groupGoals)
                {
                    groupGoalsRetunCollection.Add(GetGoalObject(goal, dateToUseForFiletring, currentUser));
                }

                return Ok(groupGoalsRetunCollection);
            }
            catch (Exception)
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
            goalProgres.Goal.Id == goal.Id
            && goalProgres.User == currentUser);
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
                var exGoal = _dataContext.Goals.Find(goal.Id);
                var goalStringValue = exGoal.GoalType == 201 ? RandomGoals.GetRandomGoal() : "";
                var newGoalProgress = new GoalProgress
                {
                    Goal = exGoal,
                    User = currentUser,
                    IsDone = false,
                    GoalStringValue = goalStringValue,
                    CreatedAt = DateTime.Now
                };
                // today goal progress creation
                _dataContext.GoalProgresses.Add(newGoalProgress);
                _dataContext.SaveChanges();
                return new
                {
                    goal,
                    goalProgress = newGoalProgress
                };

            }
        }

        // for group
        [HttpPost]
        public IActionResult GetUserGoalsWithProgress([FromBody] GroupProgressDto groupProgressDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentGroup = _dataContext.Groups.Include(g => g.Members)
                    .SingleOrDefault(g => g.Members.Contains(currentUser)); //Include(group=>group.Members).

                if (currentGroup == null)
                {
                    var leaderGroup = _dataContext.Groups.Include(g => g.Members)
                    .SingleOrDefault(g => g.LeaderUsername == currentUser.Username);
                    if (leaderGroup == null)
                    {
                        return StatusCode(204);
                    }
                    currentGroup = leaderGroup;
                }

                var groupGoals = _dataContext.Goals.Include(g=>g.Workout).Where(g => g.GoalMedium.Group == currentGroup).ToArray();
                //var groupGoals = _dataContext.Goals.Include(gg => gg.Group).Where(g => g.Group == currentGroup).ToArray();

                var groupDayProgress = new List<object>();

                var today = DateTime.Now;

                foreach (var goal in groupGoals)
                {
                    var userGoalProgresses = new List<object>();
                    var goalStringValue = goal.GoalType == 201 ? RandomGoals.GetRandomGoal() : "";
                    foreach (var user in currentGroup.Members)
                    {
                        //var userDescription = _dataContext.UserDescriptions.SingleOrDefault(ud => ud.username == user.Username);
                        var userProgress = _dataContext.GoalProgresses.SingleOrDefault(
                            ggp =>
                            ggp.Goal == goal &&
                            ggp.User == user &&
                            ggp.CreatedAt.Day == groupProgressDto.GroupProgressDate.Day &&
                            ggp.CreatedAt.Month == groupProgressDto.GroupProgressDate.Month &&
                            ggp.CreatedAt.Year == groupProgressDto.GroupProgressDate.Year);
                        if (userProgress == null)
                        {
                            // sukurimas
                            if (today.Day == groupProgressDto.GroupProgressDate.Day &&
                                today.Month == groupProgressDto.GroupProgressDate.Month &&
                                today.Year == groupProgressDto.GroupProgressDate.Year)
                            {
                                var newGroupGoalProgress = new GoalProgress
                                {
                                    CreatedAt = today,
                                    Goal = goal,
                                    IsDone = false,
                                    GoalStringValue = goalStringValue,
                                    User = user
                                };
                                _dataContext.GoalProgresses.Add(newGroupGoalProgress);
                                _dataContext.SaveChanges();
                                userGoalProgresses.Add(newGroupGoalProgress);

                            }
                            else
                            {
                                // check in the future
                                userGoalProgresses.Add(new
                                {
                                    CreatedAt = today,
                                    IsDone = false,
                                    user,
                                    //userDescription,
                                    isDummy = true
                                });
                            }
                        }
                        else
                        {
                            userGoalProgresses.Add(userProgress);
                        }
                    }

                    var goalWithProgresses = new
                    {
                        goal,
                        userGoalProgresses
                    };

                    groupDayProgress.Add(goalWithProgresses);
                }

                return Ok(groupDayProgress);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

    }
}