using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto.GroupGoals
{
    public class GroupGoalDto
    {
        public string Name { get; set; }
        public int GoalType { get; set; }

        public int WorkoutId { get; set; }
    }
}
