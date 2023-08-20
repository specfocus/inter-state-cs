namespace XState
{
    internal static partial class Utils
    {
        public static T[] ToArray<T>(object value)
        {
            if (value == null)
            {
                return new T[0];
            }
            return ToArrayStrict<T>(value);
        }
    }
}
