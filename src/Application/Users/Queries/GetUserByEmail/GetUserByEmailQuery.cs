using Domain.Entities.User;
using MediatR;

namespace Application.Users.Queries.GetUserByEmail
{
    public record GetUserByEmailQuery : IRequest<IUser>
    {
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}