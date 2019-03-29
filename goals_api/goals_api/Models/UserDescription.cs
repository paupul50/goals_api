using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class UserDescription
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Comment> Comments { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        public string username { get; set; }
    }
}
