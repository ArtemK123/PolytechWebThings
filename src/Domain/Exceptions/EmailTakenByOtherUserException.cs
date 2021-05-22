namespace Domain.Exceptions
{
    public class EmailTakenByOtherUserException : NotUniqueEntityException
    {
        public EmailTakenByOtherUserException(string userEmail)
            : base($"Email {userEmail} is already taken by other user")
        {
        }
    }
}