using MediatR;

namespace Application.Commands.LoginUser
{
    public record LoginUserCommand : IRequest
    {
        public LoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}