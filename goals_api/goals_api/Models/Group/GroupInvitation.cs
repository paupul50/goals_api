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
        public string LeaderUsername { get; set; }
        public string MemberUsername { get; set; }
    }
}
