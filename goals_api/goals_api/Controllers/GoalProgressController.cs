using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto.GoalProgress;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace goals_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GoalProgressController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GoalProgressController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpPatch]
        public IActionResult SaveUserGoal([FromBody]GoalProgressPatchDto goalProgressPatchDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            var goalProgress = _dataContext.GoalProgresses.Find(goalProgressPatchDto.Id);
            goalProgress.IsDone = goalProgressPatchDto.isDone;
            _dataContext.GoalProgresses.Update(goalProgress);
            _dataContext.SaveChanges();

            return Ok(goalProgress);
        }
    }
}