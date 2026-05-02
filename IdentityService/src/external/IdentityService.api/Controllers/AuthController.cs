using Microsoft.AspNetCore.Mvc;
using IdentityService.application.Interfaces.Persistence;
using IdentityService.domain.User.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MediatR;
using IdentityService.application.AuthModule.Commands.RegisterCommand;
using IdentityService.application.AuthModule.Dtos;
using IdentityService.application.AuthModule.Commands.Login;

namespace IdentityService.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
       
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
 
     
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var command = new RegisterUserCommand(request);
            var requestResponse = await  _sender.Send(command);
            return  StatusCode(requestResponse.ResponseCode, requestResponse.ResponseData);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginCommand(request);        
            var requestResponse = await _sender.Send(command);  
            return StatusCode(requestResponse.ResponseCode, requestResponse.ResponseData);
        }
        
    }
}
