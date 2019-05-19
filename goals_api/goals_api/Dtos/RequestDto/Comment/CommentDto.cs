using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto.Comment
{
    public class CommentDto
    {
        [Required]
        [StringLength(50)]
        public string CommentTarget { get; set; }
        [Required]
        [StringLength(50)]
        public string CommentedUser { get; set; }
        [Required]
        [StringLength(100)]
        public string Body { get; set; }
    }
}
