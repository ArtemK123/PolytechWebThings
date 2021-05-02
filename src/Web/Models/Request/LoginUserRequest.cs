namespace Web.Models.Request
{
    public class LoginUserRequest
    {
        public string? Email { get; init; }

        public string? Password { get; init; }
    }
}