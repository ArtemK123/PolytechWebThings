namespace Web.Models.Request
{
    public class CreateUserRequest
    {
        public string? Email { get; init; }

        public string? Password { get; init; }
    }
}