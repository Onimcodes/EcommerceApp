using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.infrastructure.Authentication
{
    public class JwtOptions
    {
        public const string SectionName = "JwtOptions";
        public string? Secret { get; set; }
        public string? issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiryMinutes { get; set; }
        public string? RefreshTokenCookieKey { get; set; }
    }
}
