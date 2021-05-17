namespace Domain.Exceptions
{
    public class UserNotFoundByEmailException : EntityNotFoundException
    {
        public UserNotFoundByEmailException(string email)
            : base($"User with email={email} is not found")
        {
        }
    }
}