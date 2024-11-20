using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model ;

namespace UserService.Dtos.User
{
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; } = string.Empty;


        public string Email { get; set; }   = string.Empty;


        public string Token  { get; set; } = string.Empty;

    }
}