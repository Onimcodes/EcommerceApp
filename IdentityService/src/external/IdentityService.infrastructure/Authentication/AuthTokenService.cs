using IdentityService.application.Common;
using IdentityService.application.Interfaces;
using IdentityService.application.Interfaces.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityService.infrastructure.Authentication
{
    public sealed class AuthTokenService : IAuthTokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IHttpContextAccessor _httpContextAccessor;



        public AuthTokenService(IOptions<JwtOptions> jwtOptions, IUnitOfWork unitOfWork)
        {
            _jwtOptions = jwtOptions.Value;
            _unitOfWork = unitOfWork;
            //_httpContextAccessor = httpContextAccessor;
        }

        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                TokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7)
            };
        }

        public RefreshToken GenerateLogoutToken()
        {
            return new RefreshToken
            {
                TokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow
            };
        }

        public async Task<string> GenerateToken(GenerateTokenObject tokenObject)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
           

         
            var claims = new List<Claim>
        {
             new Claim(JwtRegisteredClaimNames.Sub, tokenObject.userId.ToString()),
             
                new Claim(JwtRegisteredClaimNames.Email, tokenObject.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, tokenObject.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               
        };

            //foreach (var permission in await _unitOfWork.Users.GetUserPermission(tokenObject.Id))
            //{
            //    Console.WriteLine(permission);
            //    claims.Add(new Claim("permission", permission));
            //}
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret!)),
                SecurityAlgorithms.HmacSha256Signature
                );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtOptions.issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
                SigningCredentials = signingCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void SetRefreshTokenAsHttpOnlyCookie(RefreshToken refreshToken)
        {
            //var cookieOptions = new CookieOptions
            //{
            //    HttpOnly = true,
            //    Expires = refreshToken.Expires,
            //    SameSite = SameSiteMode.None,
            //    Secure = true,
            //    Path = "/",

            //};

            //var httpContext = _httpContextAccessor.HttpContext;
            //httpContext?.Response.Cookies.Append(_jwtOptions.RefreshTokenCookieKey, refreshToken.TokenValue, cookieOptions);
        }
    }
}
