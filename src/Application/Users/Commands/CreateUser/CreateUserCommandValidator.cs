﻿using FluentValidation;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(model => model.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(GetNotEmptyMessage("Email"))
                .EmailAddress().WithMessage("A valid email address is required.");
            RuleFor(model => model.Password)
                .NotEmpty().WithMessage(GetNotEmptyMessage("Password"));
        }

        private static string GetNotEmptyMessage(string property) => $"{property} is required.";
    }
}