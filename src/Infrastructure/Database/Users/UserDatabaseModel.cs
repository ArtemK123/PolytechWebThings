using Domain.Enums;

namespace PolytechWebThings.Infrastructure.Database.Users
{
    internal class UserDatabaseModel
    {
        public string Id { get; set; } = "";

        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

        public string? SessionToken { get; set; }

        public UserRole Role { get; set; }
    }
}