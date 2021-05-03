using System;

namespace Domain.Exceptions
{
    public class CanNotConnectToGatewayException : Exception
    {
        public CanNotConnectToGatewayException()
            : base("Can not connect to gateway using the provided url and access token")
        {
        }
    }
}