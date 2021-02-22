using Domain.Enums;

namespace Domain.Entities.User
{
    public record NewUserCreationModel
    {
        public string Email { get; init; }

        public string Password { get; init; }

        public UserRole Role { get; init; }
    }
}