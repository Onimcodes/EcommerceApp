using FluentValidation;
using IdentityService.application.AuthModule.Commands.RegisterCommand;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.AuthModule.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.request.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");// use regex to stat requirement for password
        }
    }
}
