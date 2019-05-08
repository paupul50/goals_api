using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class GoalProgress
    {
        [Key]
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get; set; }
        public Goal Goal { get; set; }
        public User User { get; set; }
        public int GoalNumberValue { get; set; }
        public string GoalStringValue { get; set; }
    }
}
