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
        public User AuthorUsername { get; set; }
        public User CommentedUser { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
