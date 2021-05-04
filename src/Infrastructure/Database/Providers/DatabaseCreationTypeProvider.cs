using Microsoft.Extensions.Configuration;
using PolytechWebThings.Infrastructure.Database.Enums;

namespace PolytechWebThings.Infrastructure.Database.Providers
{
    internal class DatabaseCreationTypeProvider : IDatabaseCreationTypeProvider
    {
        private readonly IConfiguration configuration;

        public DatabaseCreationTypeProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DatabaseCreationType GetDatabaseCreationType()
        {
            return configuration.GetValue<DatabaseCreationType>("DatabaseCreationType");
        }
    }
}