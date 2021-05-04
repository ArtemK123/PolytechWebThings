using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace PolytechWebThings.Infrastructure.Database.Users
{
    internal class UserDatabaseModel
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? SessionToken { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}