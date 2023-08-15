namespace XState.Extensions
{
    using System;

    public static class DateTimeOffsetExtensions
    {
        public static string ToFormattedString(this DateTimeOffset? dateTimeOffset)
            => dateTimeOffset?.ToString("r");
    }
}
