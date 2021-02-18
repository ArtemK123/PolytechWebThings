using PolytechWebThings.Application.Common.Interfaces;
using System;

namespace PolytechWebThings.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
