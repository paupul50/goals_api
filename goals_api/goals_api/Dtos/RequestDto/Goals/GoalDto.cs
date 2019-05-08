using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos
{
    public class GoalDto
    {
        [Required]
        [StringLength(20)]
        public string Goalname { get; set; }
        public int GoalType { get; set; }
        public int WorkoutId { get; set; }
        public bool IsGroupGoal { get; set; }
        public int GoalNumberValue { get; set; }
        public string GoalStringValue { get; set; }
    }
}
