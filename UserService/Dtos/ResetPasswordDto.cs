using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class ResetPasswordDto
    {

    public string email { get; set; } = string.Empty;

    public string token { get; set; }

    public string newPassword { get; set; }
    }
}