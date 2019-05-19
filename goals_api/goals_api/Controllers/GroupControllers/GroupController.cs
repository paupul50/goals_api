using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto;
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
    public class GroupController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GroupController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var userGroup = _dataContext.Groups.Include(ug => ug.Members).SingleOrDefault(group => group.LeaderUsername == currentUser.Username);
                if (userGroup == null)
                {
                    return StatusCode(401);
                }
                var groupGoals = _dataContext.Goals.Include(g=>g.GoalMedium).Where(g => g.GoalMedium.Group == userGroup).ToList();
                foreach (var goal in groupGoals)
                {
                    var groupGoalProgress = _dataContext.GoalProgresses.Where(ggp => ggp.Goal == goal).ToList();
                    _dataContext.GoalProgresses.RemoveRange(groupGoalProgress);
                    _dataContext.Goals.Remove(goal);
                }

                userGroup.Members.Clear();

                _dataContext.Groups.Remove(userGroup);

                _dataContext.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] GroupDto groupDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var userGroup = _dataContext.Groups.SingleOrDefault(group => group.Members.Contains(currentUser)); // .Include(group=>group.Members)
                if (userGroup != null)
                {
                    return StatusCode(401);
                }
                var groupInLead = _dataContext.Groups.SingleOrDefault(group => group.LeaderUsername == currentUser.Username);
                if (groupInLead != null)
                {
                    return StatusCode(401);
                }

                var newGroup = new Group
                {
                    GroupName = groupDto.GroupName,
                    LeaderUsername = currentUser.Username,
                    CreatedAt = DateTime.Now
                };

                _dataContext.Groups.Add(newGroup);
                _dataContext.SaveChanges();

                return Ok(newGroup);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                var userGroup = _dataContext.Groups.SingleOrDefault(group => group.Members.Contains(currentUser)); // .Include(group=>group.Members)
                if (userGroup == null)
                {
                    var groupInLead = _dataContext.Groups.SingleOrDefault(group => group.LeaderUsername == currentUser.Username);
                    if (groupInLead == null)
                    {
                        return Ok(new { Group = false, isLeader = false });
                    }
                    return Ok(new { Group = groupInLead, isLeader = true });
                }
                return Ok(new { Group = userGroup, isLeader = false });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}