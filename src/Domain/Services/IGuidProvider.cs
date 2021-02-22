using System;

namespace Domain.Services
{
    public interface IGuidProvider
    {
        Guid CreateGuid();
    }
}