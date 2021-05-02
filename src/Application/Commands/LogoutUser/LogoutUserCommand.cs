using MediatR;

namespace Application.Commands.LogoutUser
{
    public class LogoutUserCommand : IRequest
    {
        public string Email { get; init; } = string.Empty;
    }
}