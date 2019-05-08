using goals_api.Models.Goals;
using goals_api.Models.Workouts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int GoalType { get; set; }
        public Workout Workout { get; set; }
        public GoalMedium GoalMedium { get; set; }
        public int GoalNumberValue { get; set; }
        public string GoalStringValue { get; set; }
    }
}
