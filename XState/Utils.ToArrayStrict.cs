namespace XState
{
    internal static partial class Utils
    {
        public static T[] ToArrayStrict<T>(object value)
        {
            if (value is T[] arrayValue)
            {
                return arrayValue;
            }
            return new T[] { (T)value };
        }
    }
}
