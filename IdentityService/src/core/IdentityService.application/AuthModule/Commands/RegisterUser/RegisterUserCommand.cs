using IdentityService.application.AuthModule.Dtos;
using IdentityService.application.Common.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.AuthModule.Commands.RegisterCommand
{
    public record RegisterUserCommand(RegisterUserRequest request) : IRequest<RequestResponse<RegisterUserResponse>>;
  
}
