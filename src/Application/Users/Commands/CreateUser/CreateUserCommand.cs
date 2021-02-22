using Domain.Enums;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest
    {
        public string Email { get; init; }

        public string Password { get; init; }

        public UserRole Role { get; init; }
    }
}