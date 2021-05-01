using System;

namespace Domain.Exceptions
{
    public class UserNotFoundByEmailException : Exception
    {
        public UserNotFoundByEmailException(string email)
            : base($"User with email=${email} is not found")
        {
        }
    }
}