using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models.Goals
{
    public class GoalMedium
    {
        [Key]
        public int Id { get; set; }
        public bool IsGroupMedium { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }
    }
}
