using System;

namespace Domain.Exceptions
{
    public class CircularReferenceException : Exception
    {
        public CircularReferenceException(string message)
            : base(message)
        {
        }
    }
}