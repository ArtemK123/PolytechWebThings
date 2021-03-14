using Domain.Enums;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest
    {
        public string Email { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public UserRole Role { get; init; }
    }
}