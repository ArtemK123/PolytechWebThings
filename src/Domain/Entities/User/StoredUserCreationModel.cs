using Domain.Enums;

namespace Domain.Entities.User
{
    public record StoredUserCreationModel
    {
        public StoredUserCreationModel(string id, string email, string password, string? sessionToken, UserRole role)
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

        public string? SessionToken { get; }

        public UserRole Role { get; }
    }
}