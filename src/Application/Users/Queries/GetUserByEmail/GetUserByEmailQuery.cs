using Domain.Entities.User;
using MediatR;

namespace Application.Users.Queries.GetUserByEmail
{
    public record GetUserByEmailQuery : IRequest<IUser>
    {
        public string Email { get; init; } = string.Empty;
    }
}