using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models.Workouts
{
    public class RoutePointProgress
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public WorkoutProgress WorkoutProgress { get; set; }
        public RoutePoint RoutePoint { get; set; }
    }
}
