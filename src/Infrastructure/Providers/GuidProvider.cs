using System;
using Domain.Providers;

namespace PolytechWebThings.Infrastructure.Providers
{
    internal class GuidProvider : IGuidProvider
    {
        public Guid CreateGuid()
        {
            return Guid.NewGuid();
        }
    }
}