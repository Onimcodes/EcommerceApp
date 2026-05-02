using IdentityService.application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.Interfaces
{
    public interface IAuthTokenService
    {
        Task<string> GenerateToken(GenerateTokenObject tokenObject);
        //Task<string> BlackListToken(string token);
        RefreshToken GenerateRefreshToken();
        RefreshToken GenerateLogoutToken();
        void SetRefreshTokenAsHttpOnlyCookie(RefreshToken refreshToken);
    }

    public record GenerateTokenObject(string userId, string? Email);
}
