using Domain.Enums;

namespace Domain.Entities.User
{
    internal record User : IUser
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public string? SessionToken { get; init; }

        public UserRole Role { get; init; }
    }
}