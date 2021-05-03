using System;

namespace Domain.Providers
{
    public interface IGuidProvider
    {
        Guid CreateGuid();
    }
}