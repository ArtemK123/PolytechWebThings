﻿using Application.Users.Validators;
using FluentValidation;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(model => model.Email).SetValidator(new EmailValidator()).NotNull();
            RuleFor(model => model.Password).NotEmpty();
        }
    }
}