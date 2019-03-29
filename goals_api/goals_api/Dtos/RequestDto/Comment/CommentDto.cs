using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto.Comment
{
    public class CommentDto
    {
        public string CommentTarget { get; set; }
        public int CommentTargetId { get; set; }
        public string Body { get; set; }
    }
}
