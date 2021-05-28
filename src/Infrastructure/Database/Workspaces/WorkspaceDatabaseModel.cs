using PolytechWebThings.Infrastructure.Database.Users;

namespace PolytechWebThings.Infrastructure.Database.Workspaces
{
    internal class WorkspaceDatabaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string GatewayUrl { get; set; } = null!;

        public string AccessToken { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public UserDatabaseModel User { get; set; } = null!;
    }
}