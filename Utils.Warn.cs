namespace XState
{
    internal static partial class Utils
    {
        private static Action<bool, string, Exception?> WarnDelegate = (condition, message, error) =>
        {
            Console.WriteLine(message);
        };

        public static void Warn(bool condition, string message)
        {
            if (!Environment.IS_PRODUCTION)
            {
                WarnDelegate(condition, message, null);
            }
        }

        public static void Warn(bool condition, string message, Exception error)
        {
            if (!Environment.IS_PRODUCTION)
            {
                WarnDelegate(condition, message, error);
            }
        }
    }
}
