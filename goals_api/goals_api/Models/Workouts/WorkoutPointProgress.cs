using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models.Workouts
{
    public class WorkoutPointProgress
    {
        public int Id { get; set; }
        public int WorkoutPointId { get; set; } // nereikia?
        public bool IsDone { get; set; }
        public int WorkoutProgress { get; set; }
        public RoutePoint RoutePoint { get; set; }
    }
}
