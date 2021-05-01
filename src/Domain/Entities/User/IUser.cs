using Domain.Enums;

namespace Domain.Entities.User
{
    public interface IUser
    {
        public string Id { get; }

        public string Email { get; }

        public string Password { get; }

        public string? SessionToken { get; }

        public UserRole Role { get; }

        public IUser MutateSessionToken(string? newToken);
    }
}