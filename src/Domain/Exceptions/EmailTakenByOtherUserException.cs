using System;

namespace Domain.Exceptions
{
    public class EmailTakenByOtherUserException : Exception
    {
        public EmailTakenByOtherUserException(string userEmail, string otherUserId)
            : base($"Email {userEmail} is already taken by user with id=${otherUserId}")
        {
        }
    }
}