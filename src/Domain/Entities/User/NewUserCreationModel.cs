using Domain.Enums;

namespace Domain.Entities.User
{
    public record NewUserCreationModel
    {
        public NewUserCreationModel(string email, string password, UserRole role)
        {
            Email = email;
            Password = password;
            Role = role;
        }

        public string Email { get; }

        public string Password { get; }

        public UserRole Role { get; }
    }
}