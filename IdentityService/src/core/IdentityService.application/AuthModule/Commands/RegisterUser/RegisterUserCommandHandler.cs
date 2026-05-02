using IdentityService.application.AuthModule.Dtos;
using IdentityService.application.Common.Dtos;
using IdentityService.application.Common.Dtos.Constants;
using IdentityService.application.Interfaces.Persistence;
using IdentityService.domain.User.Models;
using MediatR;
using System.Net;

namespace IdentityService.application.AuthModule.Commands.RegisterCommand
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RequestResponse<RegisterUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
        public async Task<RequestResponse<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            request.Deconstruct(out var requestBody);
            var userExists = await Task.Run(() => _unitOfWork.Users.Find(x => x.Email == requestBody.Email).FirstOrDefault());

            if (userExists != null)
            {
                return new RequestResponse<RegisterUserResponse>
                {
                    ResponseCode = 400,
                    ResponseMessage = "User Already exists"
                };
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(requestBody.Password);
            User newUser = new User
            {
                PasswordHash = passwordHash,
                Email = requestBody.Email,
                Username = requestBody.Email
            };

             _unitOfWork.Users.Add(newUser);

            return new RequestResponse<RegisterUserResponse>
            {
                ResponseCode = (int)HttpStatusCode.OK,
                ResponseMessage = ResponseMessages.GetSuccessMessage(),
                ResponseData = new RegisterUserResponse(userId: newUser.Id)

            };

        }
    }
}
