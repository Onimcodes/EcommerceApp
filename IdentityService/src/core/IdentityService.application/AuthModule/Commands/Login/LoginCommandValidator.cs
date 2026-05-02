using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.AuthModule.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.request.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.request.password)
                .NotEmpty().WithMessage("Password is required.")    
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");// use regex to stat requirement for password
                
        }
    }
}
