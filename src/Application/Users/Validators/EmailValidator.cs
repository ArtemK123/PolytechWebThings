﻿using FluentValidation;

namespace Application.Users.Validators
{
    public class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(email => email)
                .Cascade(CascadeMode.Stop)
                .EmailAddress().WithMessage("A valid email address is required.");
        }
    }
}