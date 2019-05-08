using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto.Workouts
{
    public class WorkoutProgressDto
    {
        public int WorkoutProgress { get; set; }
        public int RoutePointId { get; set; }
    }
}
