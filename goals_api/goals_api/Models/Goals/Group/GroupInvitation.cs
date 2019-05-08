using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class GroupInvitation
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }
    }
}
