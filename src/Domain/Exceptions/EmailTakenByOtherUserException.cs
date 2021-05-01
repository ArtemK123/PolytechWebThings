using System;

namespace Domain.Exceptions
{
    public class EmailTakenByOtherUserException : Exception
    {
        public EmailTakenByOtherUserException(string userEmail)
            : base($"Email {userEmail} is already taken by other user ")
        {
        }
    }
}