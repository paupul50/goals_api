using System;
using System.Linq;
using goals_api.Dtos.RequestDto.GoalProgress;
using goals_api.Models;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace goals_api.Controllers.GoalControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GoalProgressController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GoalProgressController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPatch]
        public IActionResult UpdateUserGoalProgress([FromBody]GoalProgressPatchDto goalProgressPatchDto)
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                GoalProgress goalProgress;
                if (goalProgressPatchDto.IsGroup)
                {
                    var currentGroup = _dataContext.Groups.Include(g => g.Members)
                        .SingleOrDefault(g => g.Members.Contains(currentUser));
                    goalProgress = _dataContext.GoalProgresses.Include(gp => gp.Goal.GoalMedium)
                        .SingleOrDefault(gp => gp.Goal.GoalMedium.Group == currentGroup && gp.Id == goalProgressPatchDto.Id);
                }
                else
                {
                    goalProgress = _dataContext.GoalProgresses.Include(gp => gp.Goal.GoalMedium).SingleOrDefault(gp => gp.Id == goalProgressPatchDto.Id);
                }
                if (goalProgress == null)
                {
                    return StatusCode(204);
                }
                if (goalProgress.User != currentUser)
                {
                    return StatusCode(401);
                }

                switch (goalProgress.Goal.GoalType)
                {
                    case 101:
                        goalProgress.IsDone = goalProgressPatchDto.IsDone;
                        _dataContext.GoalProgresses.Update(goalProgress);
                        _dataContext.SaveChanges();
                        break;
                    case 102:
                        goalProgress.GoalNumberValue = goalProgressPatchDto.GoalNumberValue;
                        if(goalProgress.GoalNumberValue>=goalProgress.Goal.GoalNumberValue)
                        {
                            goalProgress.IsDone = true;
                        }
                        else
                        {
                            goalProgress.IsDone = false;
                        }
                        _dataContext.GoalProgresses.Update(goalProgress);
                        _dataContext.SaveChanges();
                        break;
                    case 201:
                        goalProgress.IsDone = goalProgressPatchDto.IsDone;
                        _dataContext.GoalProgresses.Update(goalProgress);
                        _dataContext.SaveChanges();
                        break;
                    default:
                        return StatusCode(401);
                }
                return Ok(goalProgress);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}