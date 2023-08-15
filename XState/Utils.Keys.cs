namespace XState
{
    internal static partial class Utils
    {
        public static List<string> GetKeys<T>(T value)
            where T : class
        {
            return value.GetType().GetProperties().Select(property => property.Name).ToList();
        }
    }
}
