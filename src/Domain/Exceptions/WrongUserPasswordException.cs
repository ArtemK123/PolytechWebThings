using System;

namespace Domain.Exceptions
{
    public class WrongUserPasswordException : Exception
    {
        public WrongUserPasswordException(string email)
            : base($"Wrong password for user with email=${email}")
        {
        }
    }
}