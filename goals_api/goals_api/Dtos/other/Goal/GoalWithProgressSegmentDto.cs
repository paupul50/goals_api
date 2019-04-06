using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.Goals
{
    public class GoalWithProgressSegmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<GoalProgressPoco> GoalProgressCollection { get; set; }
    }
}
