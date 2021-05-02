using Domain.Entities.User;
using MediatR;

namespace Application.Queries.GetUserByEmail
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