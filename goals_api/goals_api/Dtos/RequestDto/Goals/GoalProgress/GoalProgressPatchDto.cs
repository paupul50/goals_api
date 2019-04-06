using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto.GoalProgress
{
    public class GoalProgressPatchDto
    {
        public int Id { get; set; }
        public bool isDone { get; set; }
    }
}
