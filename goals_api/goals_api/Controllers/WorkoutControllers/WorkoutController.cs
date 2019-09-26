using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto;
using goals_api.Models.Workouts;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace goals_api.Controllers.WorkoutControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public WorkoutController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkoutDto workoutDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);

            try
            {
                var newWorkout = new Workout
                {
                    CreatedAt = DateTime.Now,
                    Creator = currentUser,
                    Name = workoutDto.Name
                };
                await _dataContext.Workouts.AddAsync(newWorkout);

                var newWorkoutRoutePointList = new List<RoutePoint>();

                var index = 0;

                foreach (var routePoint in workoutDto.RoutePoints)
                {
                    if (index + 1 != routePoint.Index)
                    {
                        return StatusCode(204);
                    }
                    index++;

                    newWorkoutRoutePointList.Add(new RoutePoint
                    {
                        Lat = routePoint.Lat,
                        Lng = routePoint.Lng,
                        Radius = routePoint.Radius,
                        FillColour = routePoint.FillColour,
                        CircleDraggable = routePoint.CircleDraggable,
                        Editable = routePoint.Editable,
                        Index = routePoint.Index,
                        Workout = newWorkout
                    });
                }
                await _dataContext.RoutePoints.AddRangeAsync(newWorkoutRoutePointList);

                await _dataContext.SaveChangesAsync();

                return StatusCode(201);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUser = await _dataContext.Users.FindAsync(User.Identity.Name);
            try
            {
                var workoutToDelete = await _dataContext.Workouts.FindAsync(id);
                if (workoutToDelete == null || workoutToDelete.Creator!=currentUser)
                {
                    return StatusCode(401);
                }
                var workoutGoal = _dataContext.Goals.Where(g => g.Workout.Id == id).ToList();
                if (workoutToDelete.Creator != currentUser || workoutGoal.Count > 1)
                {
                    return StatusCode(6000);
                }

                var workoutRoutePointsToDelete = _dataContext.RoutePoints.Include(rp => rp.Workout).Where(rp => rp.Workout == workoutToDelete);



                var workoutsProgresses = _dataContext.WorkoutProgresses.Where(w => w.Workout == workoutToDelete);
                foreach (var temp in workoutRoutePointsToDelete)
                {
                    var routePointsProgressesToRemove =
                        _dataContext.RoutePointProgresses.Where(wrp => wrp.RoutePoint == temp);

                    _dataContext.RoutePointProgresses.RemoveRange(routePointsProgressesToRemove);
                }

                _dataContext.WorkoutProgresses.RemoveRange(workoutsProgresses);
                _dataContext.RoutePoints.RemoveRange(workoutRoutePointsToDelete);
                _dataContext.Workouts.Remove(workoutToDelete);
                
                await _dataContext.SaveChangesAsync();

                return StatusCode(204);

            }
            catch (Exception exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserWorkouts()
        {
            var currentUser = await _dataContext.Users.FindAsync(User.Identity.Name);
            try
            {
                var userWorkouts = _dataContext.Workouts.Where(w => w.Creator == currentUser).ToList();

                foreach (var workout in userWorkouts)
                {
                    workout.Creator = null;
                }
                return Ok(userWorkouts);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
        [HttpGet("unused")]
        public async Task<IActionResult> GetUserUnusedWorkouts() 
        {
            var currentUser = await _dataContext.Users.FindAsync(User.Identity.Name);
            try
            {

                var gworkoutIds = _dataContext.Goals.Include(g => g.Workout).Where(gg => gg.Workout != null && gg.Workout.Creator == currentUser).Select(gg => gg.Workout.Id).ToList();

                var userWorkouts = _dataContext.Workouts.Where(w => !gworkoutIds.Contains(w.Id) && w.Creator == currentUser);


                foreach (var workout in userWorkouts)
                {
                    workout.Creator = null;
                }
                return Ok(userWorkouts);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
        [HttpGet("groupUnused")]
        public async Task<IActionResult> GetGroupUnusedWorkouts()
        {
            var currentUser = await _dataContext.Users.FindAsync(User.Identity.Name);
            try
            {
                var gworkoutIds = _dataContext.Goals.Include(g => g.GoalMedium).Where(gg => gg.Workout != null && gg.GoalMedium.Group.LeaderUsername == currentUser.Username)
                    .Select(gg => gg.Workout.Id).ToList();

                var groupWorkouts = _dataContext.Workouts.Where(w => !gworkoutIds.Contains(w.Id) && w.Creator == currentUser);

                foreach (var workout in groupWorkouts)
                {
                    workout.Creator = null;
                }
                return Ok(groupWorkouts);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
        [HttpGet("group")]
        public async Task<IActionResult> GetGroupWorkouts()
        {
            var currentUser = await _dataContext.Users.FindAsync(User.Identity.Name);
            try
            {
                var currentGroup = _dataContext.Groups.SingleOrDefault(g => g.Members.Contains(currentUser));

                if (currentGroup == null)
                {
                    return StatusCode(204);
                }

                var gworkoutIds = _dataContext.Goals.Include(g => g.GoalMedium).Where(gg => gg.GoalMedium.Group == currentGroup && gg.Workout != null).Select(gg => gg.Workout.Id).ToList();

                var workouts = _dataContext.Workouts.Where(w => gworkoutIds.Contains(w.Id));

                foreach (var workout in workouts)
                {
                    workout.Creator = null;
                }
                return Ok(workouts);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var currentUser = await _dataContext.Users.FindAsync(User.Identity.Name);
            try
            {
                var userWorkout = await _dataContext.Workouts.SingleOrDefaultAsync(w => w.Id == id);

                if(userWorkout == null)
                {
                    return StatusCode(204);
                }
                

                var workoutWithRoutePoints = _dataContext.RoutePoints.Where(rp => rp.Workout == userWorkout);

                return Ok(new
                {
                    userWorkout,
                    workoutWithRoutePoints
                });

            }
            catch (Exception )
            {

                return StatusCode(500);
            }
        }
    }
}