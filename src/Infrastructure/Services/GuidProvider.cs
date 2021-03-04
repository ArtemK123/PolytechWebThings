using System;
using Domain.Services;

namespace PolytechWebThings.Infrastructure.Services
{
    internal class GuidProvider : IGuidProvider
    {
        public Guid CreateGuid()
        {
            return Guid.NewGuid();
        }
    }
}