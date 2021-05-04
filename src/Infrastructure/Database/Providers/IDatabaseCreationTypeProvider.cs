using PolytechWebThings.Infrastructure.Database.Enums;

namespace PolytechWebThings.Infrastructure.Database.Providers
{
    internal interface IDatabaseCreationTypeProvider
    {
        DatabaseCreationType GetDatabaseCreationType();
    }
}