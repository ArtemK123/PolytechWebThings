using System;

namespace Application.Converters
{
    public static class NullableConverter
    {
        public static TValue GetOrThrow<TValue>(TValue? value)
            where TValue : class
            => value ?? throw new NullReferenceException();

        public static TValue GetOrThrow<TValue>(TValue? value)
            where TValue : struct
            => value ?? throw new NullReferenceException();
    }
}