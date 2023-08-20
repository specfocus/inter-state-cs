namespace XState
{
    internal static partial class Utils
    {
        public static void ReportUnhandledExceptionOnInvocation(
            Exception originalError,
            Exception currentError,
            string id
        )
        {
            if (!Environment.IS_PRODUCTION)
            {
                var originalStackTrace = originalError.StackTrace != null
                    ? $" Stacktrace was '{originalError.StackTrace}'"
                    : "";
                if (originalError == currentError)
                {
                    Console.Error.WriteLine(
                        $"Missing onError handler for invocation '{id}', error was '{originalError}'.{originalStackTrace}");
                }
                else
                {
                    var stackTrace = currentError.StackTrace != null
                        ? $" Stacktrace was '{currentError.StackTrace}'"
                        : "";
                    Console.Error.WriteLine(
                        $"Missing onError handler and/or unhandled exception/promise rejection for invocation '{id}'. " +
                        $"Original error: '{originalError}'. {originalStackTrace} Current error is '{currentError}'.{stackTrace}");
                }
            }
        }
    }
}
