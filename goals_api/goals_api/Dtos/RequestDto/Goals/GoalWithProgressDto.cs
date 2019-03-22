using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto.Goals
{
    public class GoalWithProgressDto
    {
        public DateTime DateTimeOffset { get; set; }
        public int DayLimit { get; set; }
    }
}
