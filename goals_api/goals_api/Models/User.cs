using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class User
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        // perdaryt i Key i int su Id, nes problemu sitas s kelia
        [EmailAddress]
        [Key]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string Token { get; set; }
        public string GoogleToken { get; set; }
        public string GoogleStatus { get; set; }
    }
}
