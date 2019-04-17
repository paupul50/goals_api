using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models.Workouts
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Creator { get; set; }
        public string Name { get; set; }
    }
}
