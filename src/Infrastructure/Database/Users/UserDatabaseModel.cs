using Domain.Enums;

namespace PolytechWebThings.Infrastructure.Database.Users
{
    internal class UserDatabaseModel
    {
        public string? Id { get; init; }

        public string? Email { get; init; }

        public string? Password { get; init; }

        public string? SessionToken { get; init; }

        public UserRole? Role { get; init; }
    }
}