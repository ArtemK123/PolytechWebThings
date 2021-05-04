namespace PolytechWebThings.Infrastructure.Database.Workspaces
{
    internal class WorkspaceDatabaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string GatewayUrl { get; set; }

        public string AccessToken { get; set; }

        public string UserEmail { get; set; }
    }
}