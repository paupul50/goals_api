using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class GroupGoalProgress
    {
        [Key]
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get; set; }
        public GroupGoal Goal { get; set; }
        [Required]
        public string MemberUsername { get; set; }
    }
}
