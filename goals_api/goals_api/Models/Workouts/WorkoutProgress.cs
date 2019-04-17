using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models.Workouts
{
    public class WorkoutProgress
    {
        [Key]
        public int Id { get; set; }
        public int ProgressIndex { get; set; }
        public bool IsDone { get; set; }
        public Workout Workout { get; set; }
        public User User { get; set; }

    }
}
