using System;

namespace Domain.Exceptions
{
    public class BrokenGatewayCommunicationException : Exception
    {
        public BrokenGatewayCommunicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BrokenGatewayCommunicationException(string message)
            : base(message)
        {
        }
    }
}