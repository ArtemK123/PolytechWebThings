namespace Web.Models.User.Request
{
    public record LoginUserRequest
    {
        public string? Email { get; init; }

        public string? Password { get; init; }
    }
}