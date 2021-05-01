using MediatR;

namespace Application.Users.Commands.LoginUser
{
    public record LoginUserCommand : IRequest
    {
        public string Email { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;
    }
}