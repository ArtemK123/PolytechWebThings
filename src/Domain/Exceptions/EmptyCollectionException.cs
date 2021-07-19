using System;

namespace Domain.Exceptions
{
    public class EmptyCollectionException : Exception
    {
        public EmptyCollectionException(string message)
            : base(message)
        {
        }
    }
}