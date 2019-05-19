using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto.User
{
    public class UserDescriptionDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
    }
}
