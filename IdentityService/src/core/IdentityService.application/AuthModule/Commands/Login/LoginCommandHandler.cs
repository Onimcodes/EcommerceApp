using IdentityService.application.AuthModule.Dtos;
using IdentityService.application.Common.Dtos;
using IdentityService.application.Interfaces;
using IdentityService.application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.AuthModule.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, RequestResponse<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthTokenService _authTokenService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IAuthTokenService authTokenService)
        {
            _unitOfWork = unitOfWork;
            _authTokenService = authTokenService;
        }
        public async Task<RequestResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {   
           var existingUser =  _unitOfWork.Users.Find(x => x.Email == request.request.email).FirstOrDefault()   ;


            if(existingUser == null)
            {
                return new RequestResponse<LoginResponse>
                {
                    ResponseCode = 404,
                    ResponseMessage = "User not found",
                    Errors = new List<string> { "User with the provided email does not exist." }
                };
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.request.password, existingUser.PasswordHash);
            if (isPasswordValid)
            {

                var token = await _authTokenService.GenerateToken(new GenerateTokenObject(userId: existingUser.Id, Email: existingUser.Email));


                return new RequestResponse<LoginResponse>
                {
                    ResponseCode = 200,
                    ResponseMessage = "Login successful",
                    ResponseData = new LoginResponse(userId: existingUser.Id, token: token)

                };

            }
            else
            {
                return new RequestResponse<LoginResponse>
                {
                    ResponseCode = 401,
                    ResponseMessage = "Invalid credentials",
                    Errors = new List<string> { "The provided password is incorrect." }
                };
            }

        }
    }
}
