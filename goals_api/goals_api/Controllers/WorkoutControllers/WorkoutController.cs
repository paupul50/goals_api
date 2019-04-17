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
        public IActionResult Post([FromBody] WorkoutDto workoutDto)
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
                _dataContext.Workouts.Add(newWorkout);

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
                _dataContext.RoutePoints.AddRange(newWorkoutRoutePointList);

                _dataContext.SaveChanges();

                return StatusCode(201);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var workoutToDelete = _dataContext.Workouts.Find(id);
                var workoutGoal = _dataContext.Goals.Where(g => g.WorkoutId == id).ToList();
                var workoutGoal2 = _dataContext.GroupGoals.Where(g => g.WorkoutId == id).ToList();
                if (workoutToDelete.Creator != currentUser || workoutGoal.Count>1 || workoutGoal2.Count > 1)
                {
                    return StatusCode(401);
                }

                var workoutRoutePointsToDelete = _dataContext.RoutePoints.Include(rp => rp.Workout).Where(rp => rp.Workout == workoutToDelete);
                _dataContext.RoutePoints.RemoveRange(workoutRoutePointsToDelete);
                _dataContext.Workouts.Remove(workoutToDelete);
                _dataContext.SaveChanges();

                return StatusCode(204);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet("user")]
        public IActionResult GetUserWorkouts()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var userWorkouts = _dataContext.Workouts.Where(w => w.Creator == currentUser).ToList();

                //var userWorkoutsWithRoutePoints = new List<object>();

                foreach (var workout in userWorkouts)
                {
                    workout.Creator = null;
                    //var workoutWithRoutePoints = _dataContext.RoutePoints.Where(rp => rp.Workout == workout);

                    //var workoutObject = new
                    //{
                    //    workout,
                    //    workoutWithRoutePoints
                    //};
                    //userWorkoutsWithRoutePoints.Add(workoutObject);
                }
                return Ok(userWorkouts);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
        [HttpGet("unused")]
        public IActionResult GetUserUnusedWorkouts()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                //var userWorkouts = _dataContext.Workouts.Where(w => w.Creator == currentUser).ToList();

                var gworkoutIds = _dataContext.Goals.Where(gg => gg.WorkoutId != 0 && gg.User==currentUser).Select(gg => gg.WorkoutId).ToList();

                var userWorkouts = _dataContext.Workouts.Where(w => !gworkoutIds.Contains(w.Id) && w.Creator==currentUser);

                //var userWorkoutsWithRoutePoints = new List<object>();

                foreach (var workout in userWorkouts)
                {
                    workout.Creator = null;
                    //var workoutWithRoutePoints = _dataContext.RoutePoints.Where(rp => rp.Workout == workout);

                    //var workoutObject = new
                    //{
                    //    workout,
                    //    workoutWithRoutePoints
                    //};
                    //userWorkoutsWithRoutePoints.Add(workoutObject);
                }
                return Ok(userWorkouts);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
        [HttpGet("groupUnused")]
        public IActionResult GetGroupUnusedWorkouts()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                //var userWorkouts = _dataContext.Workouts.Where(w => w.Creator == currentUser).ToList();

                var gworkoutIds = _dataContext.GroupGoals.Where(gg => gg.WorkoutId != 0 && gg.Group.LeaderUsername == currentUser.Username).Select(gg => gg.WorkoutId).ToList();

                var groupWorkouts = _dataContext.Workouts.Where(w => !gworkoutIds.Contains(w.Id) && w.Creator == currentUser);

                //var userWorkoutsWithRoutePoints = new List<object>();

                foreach (var workout in groupWorkouts)
                {
                    workout.Creator = null;
                    //var workoutWithRoutePoints = _dataContext.RoutePoints.Where(rp => rp.Workout == workout);

                    //var workoutObject = new
                    //{
                    //    workout,
                    //    workoutWithRoutePoints
                    //};
                    //userWorkoutsWithRoutePoints.Add(workoutObject);
                }
                return Ok(groupWorkouts);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
        [HttpGet("group")]
        public IActionResult GetGroupWorkouts()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentGroup = _dataContext.Groups.SingleOrDefault(g => g.Members.Contains(currentUser));

                if(currentGroup==null)
                {
                    return Ok();
                }

                var gworkoutIds = _dataContext.GroupGoals.Where(gg => gg.WorkoutId != 0).Select(gg=> gg.WorkoutId).ToList();

                var workouts = _dataContext.Workouts.Where(w => gworkoutIds.Contains(w.Id));


                //var userWorkoutsWithRoutePoints = new List<object>();

                foreach (var workout in workouts)
                {
                    workout.Creator = null;
                    //var workoutWithRoutePoints = _dataContext.RoutePoints.Where(rp => rp.Workout == workout);

                    //var workoutObject = new
                    //{
                    //    workout,
                    //    workoutWithRoutePoints
                    //};
                    //userWorkoutsWithRoutePoints.Add(workoutObject);
                }
                return Ok(workouts);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                // patikrinti ar useris grupej, arba userio goal
                var userWorkout = _dataContext.Workouts.SingleOrDefault(w => w.Id == id);

                userWorkout.Creator = null;
                var workoutWithRoutePoints = _dataContext.RoutePoints.Where(rp => rp.Workout == userWorkout);
                var workoutGoal = _dataContext.Goals.SingleOrDefault(g => g.WorkoutId == id);
                // dar gaut koks cia tikslas, jei yra

                return Ok(new
                {
                    userWorkout,
                    workoutWithRoutePoints
                });

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}