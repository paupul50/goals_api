using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class User
    {
        [Key]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string Token { get; set; }
        public string GoogleToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
    }
}
