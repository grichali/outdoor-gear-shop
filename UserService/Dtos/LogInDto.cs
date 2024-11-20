using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos.User
{
    public class LogInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}