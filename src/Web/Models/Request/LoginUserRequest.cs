namespace Web.Models.Request
{
    public record LoginUserRequest
    {
        public string? Email { get; init; }

        public string? Password { get; init; }
    }
}