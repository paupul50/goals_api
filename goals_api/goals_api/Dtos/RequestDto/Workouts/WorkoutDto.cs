using goals_api.Models.Workouts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto
{
    public class WorkoutDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public List<RoutePoint> RoutePoints { get; set; }
    }
}
