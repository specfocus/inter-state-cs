namespace XState
{
    internal static partial class Utils
    {
        public static bool IsFunction(object value)
        {
            return value is Delegate;
        }
    }
}
