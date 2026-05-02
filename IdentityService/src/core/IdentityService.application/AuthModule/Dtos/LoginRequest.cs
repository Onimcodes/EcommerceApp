using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.AuthModule.Dtos
{
    public record LoginRequest(string email, string password);
  
}
