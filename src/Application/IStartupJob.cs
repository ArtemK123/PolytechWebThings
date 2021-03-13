using System;

namespace Application
{
    public interface IStartupJob
    {
        // Smaller number means higher priority. The job with smaller number will be executed earlier
        uint Priority { get; }

        void Execute(IServiceProvider serviceProvider);
    }
}