namespace Domain.Exceptions
{
    public class GatewayAlreadyRegisteredException : NotUniqueEntityException
    {
        public GatewayAlreadyRegisteredException(string url)
            : base($"Gateway is already assigned to workspace. Gateway url is {url}")
        {
        }
    }
}