using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos;
using goals_api.Dtos.RequestDto.GroupGoals;
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
    public class GroupGoalsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GroupGoalsController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        //[HttpPost]
        //public IActionResult SaveGroupGoal([FromBody]GroupGoalDto groupGoalDto)
        //{
        //    var currentUser = _dataContext.Users.Find(User.Identity.Name);
        //    try
        //    {
        //        var currentGroup = _dataContext.Groups.SingleOrDefault(g => g.LeaderUsername == currentUser.Username); //Include(group=>group.Members).

        //        if (currentGroup == null)
        //        {
        //            StatusCode(204);
        //        }

        //        var groupGoalsWithTheSameName = _dataContext.GroupGoals.Where(goal => goal.Name == groupGoalDto.Name && goal.Group == currentGroup);
        //        if (groupGoalsWithTheSameName.ToArray().Length > 0)
        //        {
        //            return StatusCode(400);
        //        }


        //        GroupGoal groupGoal;

        //        switch (groupGoalDto.GoalType)
        //        {
        //            case 1:
        //                groupGoal = new GroupGoal
        //                {
        //                    Name = groupGoalDto.Name,
        //                    CreatedAt = DateTime.Now,
        //                    GoalType = 1,
        //                    Group = currentGroup
        //                };
        //                break;
        //            case 2:
        //                groupGoal = new GroupGoal
        //                {
        //                    Name = groupGoalDto.Name,
        //                    CreatedAt = DateTime.Now,
        //                    GoalType = 2,
        //                    WorkoutId = groupGoalDto.WorkoutId,
        //                    Group = currentGroup
        //                };
        //                break;
        //            default:
        //                return StatusCode(400);
        //        }

        //        _dataContext.GroupGoals.Add(groupGoal);
        //        //// progress adda
        //        //var goalProgressToday = new GoalProgress
        //        //{
        //        //    Goal = userGoal,
        //        //    IsDone = false,
        //        //    CreatedAt = DateTime.Now
        //        //};
        //        // _dataContext.asdasdasdasdasd.Add(goalProgressToday);
        //        _dataContext.SaveChanges();

        //        return StatusCode(201);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500);
        //    }
        //}

        [HttpDelete("{id:int}")]
        public IActionResult RemoveGroupGoal(int id)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentGroup = _dataContext.Groups.SingleOrDefault(g => g.LeaderUsername == currentUser.Username); //Include(group=>group.Members).

                if (currentGroup == null)
                {
                    StatusCode(204);
                }

                var groupGoal = _dataContext.Goals.Find(id);

                var groupGoalProgresses = _dataContext.GoalProgresses.Where(progress => progress.Goal == groupGoal);
                _dataContext.GoalProgresses.RemoveRange(groupGoalProgresses);
                _dataContext.Goals.Remove(groupGoal);
                _dataContext.SaveChanges();

                return StatusCode(204);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetGroupGoal(int id)
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                var groupGoal = _dataContext.Goals.Include(gg => gg.GoalMedium.Group).SingleOrDefault(gg => gg.Id == id);
                if (groupGoal == null)
                {
                    return StatusCode(204);
                }

                if (!groupGoal.GoalMedium.Group.Members.Contains(currentUser) && groupGoal.GoalMedium.Group.LeaderUsername != currentUser.Username)
                {
                    return StatusCode(204);
                }

                return Ok(groupGoal);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

    }
}