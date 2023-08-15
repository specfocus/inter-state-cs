namespace XState
{
    internal static partial class Utils
    {
        public static Dictionary<string, P> MapFilterValues<T, P>(
            Dictionary<string, T> collection,
            Func<T, string, Dictionary<string, T>, P> iteratee,
            Func<T, bool> predicate
        )
        {
            var result = new Dictionary<string, P>();

            foreach (var key in collection.Keys)
            {
                var item = collection[key];

                if (!predicate(item))
                {
                    continue;
                }

                result[key] = iteratee(item, key, collection);
            }

            return result;
        }
    }
}
