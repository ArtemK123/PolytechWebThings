using Domain.Enums;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest
    {
        public CreateUserCommand(string email, string password, UserRole role)
        {
            Email = email;
            Password = password;
            Role = role;
        }

        public string Email { get; }

        public string Password { get; }

        public UserRole Role { get; }
    }
}