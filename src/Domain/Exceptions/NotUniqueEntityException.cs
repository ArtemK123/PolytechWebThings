using System;

namespace Domain.Exceptions
{
    public class NotUniqueEntityException : Exception
    {
        public NotUniqueEntityException(string message)
            : base(message)
        {
        }
    }
}