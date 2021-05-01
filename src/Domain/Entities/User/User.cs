using Domain.Enums;

namespace Domain.Entities.User
{
    internal record User : IUser
    {
        public User(string id, string email, string password, string? sessionToken, UserRole role)
        {
            Id = id;
            Email = email;
            Password = password;
            SessionToken = sessionToken;
            Role = role;
        }

        public string Id { get; }

        public string Email { get; }

        public string Password { get; }

        public string? SessionToken { get; private init; }

        public UserRole Role { get; }

        public IUser MutateSessionToken(string? newToken)
        {
            return this with { SessionToken = newToken };
        }
    }
}