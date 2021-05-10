namespace Web.Models.User.Request
{
    public record CreateUserRequest
    {
        public string? Email { get; init; }

        public string? Password { get; init; }
    }
}