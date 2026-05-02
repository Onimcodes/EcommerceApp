using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.AuthModule.Dtos
{
    public record LoginResponse(string userId, string token); // add refresh token and expire time to this response

}
