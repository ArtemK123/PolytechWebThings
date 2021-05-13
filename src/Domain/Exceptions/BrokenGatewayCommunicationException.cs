using System;

namespace Domain.Exceptions
{
    public class BrokenGatewayCommunicationException : Exception
    {
        public BrokenGatewayCommunicationException(Exception innerException)
            : base("Error in communication with gateway. Contact the administrator", innerException)
        {
        }
    }
}