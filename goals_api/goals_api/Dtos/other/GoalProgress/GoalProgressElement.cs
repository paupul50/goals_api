using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos
{
    public class GoalProgressPoco
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDummy { get; set; }
        public int GoalNumberValue { get; set; }
        public string GoalStringValue { get; set; }
    }
}
