using MediatR;

namespace Application.Users.Commands.LogoutUser
{
    public class LogoutUserCommand : IRequest
    {
        public string Email { get; init; } = string.Empty;
    }
}