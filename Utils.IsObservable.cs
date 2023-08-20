namespace XState
{
    internal static partial class Utils
    {
        public static bool IsObservable<T>(object value)
        {
            try
            {
                if (value is Subscribable<T> subscribable)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
