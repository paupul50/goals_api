using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public int CommentUserDescriptionId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
