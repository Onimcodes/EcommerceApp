using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.AuthModule.Dtos
{
    public record RegisterUserRequest(string Email, string Password);

   
}
