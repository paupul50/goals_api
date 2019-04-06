using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
        public ICollection<User> Members { get; set; } = new List<User>();
        public string LeaderUsername { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
