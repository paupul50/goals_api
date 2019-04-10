using goals_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto.Workout
{
    public class WorkoutDto
    {
        public string Name { get; set; }
        public List<RoutePoint> RoutePoints { get; set; }
    }
}
