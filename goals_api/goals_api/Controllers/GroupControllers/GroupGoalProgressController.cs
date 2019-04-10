using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                      return Ok();
                 }
                    //currentUserGroup = userGroup;
                //}
                var dateToUseForFiletring = DateTime.Now.Date;

                var groupGoals = _dataContext.GroupGoals.Where(goal => goal.Group == currentUserGroup).ToList();

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

        private object GetGoalObject(GroupGoal goal, DateTime dateToUseForFiletring, User currentUser)
        {
            GroupGoalProgress goalProgress = _dataContext.GroupGoalProgresses
            .SingleOrDefault(goalProgres => goalProgres.CreatedAt.Day == dateToUseForFiletring.Day &&
            goalProgres.CreatedAt.Month == dateToUseForFiletring.Month &&
            goalProgres.CreatedAt.Year == dateToUseForFiletring.Year &&
            goalProgres.Goal.Id == goal.Id
            && goalProgres.MemberUsername == currentUser.Username);
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
                        username = goalProgress.MemberUsername,
                        goalProgress.Id,
                        goalProgress.CreatedAt,
                        goalProgress.IsDone,
                        IsDummy = false

                    }
                };
            }
            else
            {
                var newGoalProgress = new GroupGoalProgress
                {
                    Goal = _dataContext.GroupGoals.Find(goal.Id),
                    MemberUsername = currentUser.Username,
                    IsDone = false,
                    CreatedAt = DateTime.Now
                };
                // today goal progress creation
                _dataContext.GroupGoalProgresses.Add(newGoalProgress);
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
                    goalProgress = new
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

        // for group
        [HttpPost]
        public IActionResult GetUserGoalsWithProgress([FromBody] GroupProgressDto groupProgressDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentGroup = _dataContext.Groups.Include(g=>g.Members)
                    .SingleOrDefault(g => g.Members.Contains(currentUser)); //Include(group=>group.Members).

                if (currentGroup == null)
                {
                    var leaderGroup = _dataContext.Groups.Include(g => g.Members)
                    .SingleOrDefault(g => g.LeaderUsername==currentUser.Username);
                    if (leaderGroup==null)
                    {
                        return StatusCode(204);
                    }
                    currentGroup = leaderGroup;
                }

                var groupGoals = _dataContext.GroupGoals.Include(gg=>gg.Group).Where(g => g.Group == currentGroup).ToArray();

                var groupDayProgress = new List<object>();

                var today = DateTime.Now;

                foreach (var goal in groupGoals)
                {
                    var userGoalProgresses = new List<object>();
                    foreach (var user in currentGroup.Members)
                    {
                        var userProgress = _dataContext.GroupGoalProgresses.SingleOrDefault(
                            ggp => 
                            ggp.Goal == goal &&
                            ggp.MemberUsername == user.Username &&
                            ggp.CreatedAt.Day == groupProgressDto.GroupProgressDate.Day &&
                            ggp.CreatedAt.Month == groupProgressDto.GroupProgressDate.Month &&
                            ggp.CreatedAt.Year == groupProgressDto.GroupProgressDate.Year);
                        if(userProgress == null)
                        {
                            // sukurimas
                            if (today.Day == groupProgressDto.GroupProgressDate.Day &&
                                today.Month == groupProgressDto.GroupProgressDate.Month &&
                                today.Year == groupProgressDto.GroupProgressDate.Year)
                            {
                                var newGroupGoalProgress = new GroupGoalProgress
                                {
                                    CreatedAt = today,
                                    Goal = goal,
                                    IsDone = false,
                                    MemberUsername = user.Username
                                };
                                _dataContext.GroupGoalProgresses.Add(newGroupGoalProgress);
                                _dataContext.SaveChanges();
                                userGoalProgresses.Add(
                                    new
                                    {
                                        newGroupGoalProgress.CreatedAt,
                                        newGroupGoalProgress.MemberUsername,
                                        newGroupGoalProgress.IsDone,
                                        newGroupGoalProgress.Id,
                                        user,
                                        isDummy = false
                                    });

                            }
                            else
                            {
                                // check in the future
                                userGoalProgresses.Add(new
                                {
                                    CreatedAt = today,
                                    IsDone = false,
                                    user,
                                    isDummy = true
                                });
                            }
                        } else
                        {
                            userGoalProgresses.Add(new
                            {
                                userProgress.CreatedAt,
                                userProgress.MemberUsername,
                                userProgress.IsDone,
                                userProgress.Id,
                                user,
                                isDummy = false
                            });
                        }
                    }

                    var goalWithProgresses = new
                    {
                        goal = new
                        {
                            goal.Name,
                            goal.Id
                        },
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

        [HttpPatch]
        public IActionResult UpdateUserGoalProgress([FromBody]GoalProgressPatchDto goalProgressPatchDto)
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                var goalProgress = _dataContext.GroupGoalProgresses.Include(gp => gp.Goal).SingleOrDefault(gp => gp.Id == goalProgressPatchDto.Id);
                if (goalProgress == null)
                {
                    return StatusCode(204);
                }

                if (goalProgress.MemberUsername != currentUser.Username)
                {
                    return StatusCode(401);
                }

                goalProgress.IsDone = goalProgressPatchDto.isDone;
                _dataContext.GroupGoalProgresses.Update(goalProgress);
                _dataContext.SaveChanges();

                return Ok(goalProgress);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}