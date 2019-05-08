using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto.Workouts;
using goals_api.Models.DataContext;
using goals_api.Models.Workouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace goals_api.Controllers.WorkoutControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutProgressController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public WorkoutProgressController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] WorkoutProgressCreateDto workoutProgressCreateDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var unfinishedWorkoutProgress = _dataContext.WorkoutProgresses.SingleOrDefault(wp => wp.IsDone == false && wp.User == currentUser);

                if (unfinishedWorkoutProgress != null)
                {
                    return StatusCode(401);
                }
                var workout = _dataContext.Workouts.SingleOrDefault(w => w.Id == workoutProgressCreateDto.Id);
                var workoutPoints = _dataContext.RoutePoints.Where(rp => rp.Workout == workout).ToList();

                var workoutProgress = new WorkoutProgress
                {
                    IsDone = false,
                    ProgressIndex = 1,
                    User = currentUser,
                    Workout = workout
                };

                _dataContext.WorkoutProgresses.Add(workoutProgress);
                _dataContext.SaveChanges();

                foreach (var workoutPoint in workoutPoints)
                {
                    var routePointProgress = new RoutePointProgress
                    {
                        IsDone = false,
                        RoutePoint = workoutPoint,
                        WorkoutProgress = workoutProgress,
                        //WorkoutPointId = workoutPoint.Index
                    };
                    _dataContext.RoutePointProgresses.Add(routePointProgress);
                }
                _dataContext.SaveChanges();

                return StatusCode(204);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var activeWorkout = _dataContext.WorkoutProgresses.Include(wp => wp.Workout).SingleOrDefault(wg => wg.User == currentUser && wg.IsDone == false);
                if (activeWorkout != null)
                {
                    return Ok(new
                    {
                        workoutId = activeWorkout.Workout.Id,
                        activeWorkout.ProgressIndex
                    });
                }
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult UpdateUserGoalProgress(int id, [FromBody]WorkoutProgressDto workoutProgressDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var workoutProgress = _dataContext.WorkoutProgresses.Include(wp => wp.Workout).SingleOrDefault(wp => wp.Workout.Id == id && wp.User == currentUser && wp.IsDone == false);

                var allProgresses = _dataContext.RoutePointProgresses.Where(wpp => wpp.WorkoutProgress == workoutProgress).ToList();
                if (workoutProgressDto.WorkoutProgress != -1)
                {
                    if (workoutProgress == null || allProgresses == null)
                    {
                        return StatusCode(404);
                    }
                }
                else
                // if ended before completing
                if (workoutProgressDto.WorkoutProgress == -1)
                {
                    _dataContext.RoutePointProgresses.RemoveRange(allProgresses);
                    _dataContext.WorkoutProgresses.Remove(workoutProgress);

                    _dataContext.SaveChanges();

                    return Ok(new { status = 0 });

                }
                var progress = _dataContext.RoutePointProgresses.SingleOrDefault(wpp => wpp.RoutePoint.Id == workoutProgressDto.RoutePointId
                    && wpp.WorkoutProgress == workoutProgress);

                if (progress == null)
                {
                    return StatusCode(404);
                }
                // if completed (last point reached)
                if (allProgresses.Count == workoutProgressDto.WorkoutProgress)
                {
                    var todayDate = DateTime.Now;

                    workoutProgress.IsDone = true;
                    progress.IsDone = true;
                    _dataContext.RoutePointProgresses.Update(progress);
                    _dataContext.WorkoutProgresses.Update(workoutProgress);

                    // make group goal progress done
                    var groupGoalProgress = _dataContext.GoalProgresses.Include(ggp => ggp.Goal).SingleOrDefault(ggp =>
                        ggp.User == currentUser && ggp.Goal.Workout.Id == workoutProgress.Workout.Id &&
                        todayDate.Month == ggp.CreatedAt.Month && todayDate.Day == ggp.CreatedAt.Day &&
                        todayDate.Year == ggp.CreatedAt.Year);

                    if (groupGoalProgress != null && groupGoalProgress.IsDone != true)
                    {
                        groupGoalProgress.IsDone = true;
                        _dataContext.GoalProgresses.Update(groupGoalProgress);
                    }

                    // make goal progress done

                    var goalProgress = _dataContext.GoalProgresses.Include(gp => gp.Goal).SingleOrDefault(gp =>
                    gp.User == currentUser && gp.Goal.Workout.Id == workoutProgress.Workout.Id &&
                    todayDate.Month == gp.CreatedAt.Month && todayDate.Day == gp.CreatedAt.Day &&
                    todayDate.Year == gp.CreatedAt.Year
                    );

                    if (goalProgress != null && goalProgress.IsDone != true)
                    {
                        goalProgress.IsDone = true;
                        _dataContext.GoalProgresses.Update(goalProgress);
                    }

                    _dataContext.SaveChanges();
                    return Ok(new { status = 2 });

                }
                // if new point is reached
                else if (workoutProgressDto.WorkoutProgress == workoutProgress.ProgressIndex)
                {
                    workoutProgress.ProgressIndex++;
                    progress.IsDone = true;
                    _dataContext.RoutePointProgresses.Update(progress);
                    _dataContext.WorkoutProgresses.Update(workoutProgress);
                    _dataContext.SaveChanges();
                    return Ok(new { status = 1 });
                }
                return StatusCode(501);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}