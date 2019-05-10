using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Dtos.RequestDto.User
{
    public class UserDescriptionEditDto
    {
        public IFormFile File { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Description { get; set; }
    }
}
