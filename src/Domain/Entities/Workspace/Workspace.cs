namespace Domain.Entities.Workspace
{
    internal record Workspace : IWorkspace
    {
        public Workspace(int id, string name, string gatewayUrl, string accessToken, string userEmail)
        {
            Id = id;
            Name = name;
            GatewayUrl = gatewayUrl;
            AccessToken = accessToken;
            UserEmail = userEmail;
        }

        public int Id { get; }

        public string Name { get; private init; }

        public string GatewayUrl { get; private init; }

        public string AccessToken { get; private init; }

        public string UserEmail { get; private init; }

        public MutableWorkspace ToMutable()
        {
            return new MutableWorkspace
            {
                Name = Name,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken,
                UserEmail = UserEmail
            };
        }

        public IWorkspace Mutate(MutableWorkspace mutableWorkspace)
        {
            return this with
            {
                Name = mutableWorkspace.Name,
                GatewayUrl = mutableWorkspace.GatewayUrl,
                AccessToken = mutableWorkspace.AccessToken,
                UserEmail = mutableWorkspace.UserEmail,
            };
        }
    }
}