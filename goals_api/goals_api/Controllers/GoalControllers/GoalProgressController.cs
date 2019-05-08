//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using goals_api.Dtos.RequestDto.GoalProgress;
//using goals_api.Models.DataContext;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace goals_api.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GoalProgressController : ControllerBase
//    {
//        private readonly DataContext _dataContext;

//        public GoalProgressController(DataContext dataContext)
//        {
//            this._dataContext = dataContext;
//        }

//        [HttpPatch]
//        public IActionResult UpdateUserGoalProgress([FromBody]GoalProgressPatchDto goalProgressPatchDto)
//        {
//            try
//            {
//                var currentUser = _dataContext.Users.Find(User.Identity.Name);
//                var goalProgress = _dataContext.GoalProgresses.Include(gp => gp.Goal).SingleOrDefault(gp => gp.Id == goalProgressPatchDto.Id);
//                if (goalProgress == null || goalProgress.Goal.GoalType != 1)
//                {
//                    return StatusCode(204);
//                }
//                var goal = _dataContext.Goals.Find(goalProgress.Goal.Id);
//                if (goal.User !=currentUser)
//                {
//                    return StatusCode(401);
//                }

//                goalProgress.IsDone = goalProgressPatchDto.isDone;
//                _dataContext.GoalProgresses.Update(goalProgress);
//                _dataContext.SaveChanges();

//                return Ok(goalProgress);
//            }
//            catch (Exception)
//            {
//                return StatusCode(500);
//            }
//        }
//    }
//}