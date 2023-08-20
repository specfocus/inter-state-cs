namespace XState
{
    internal static partial class Utils
    {
        public static Dictionary<TKey, TValueOutput> MapValues<TKey, TValueInput, TValueOutput>(
            Dictionary<TKey, TValueInput> dictionary,
            Func<TValueInput, TValueOutput> mapFunc
        )
            where TKey : notnull
        {
            return dictionary.ToDictionary(
                kvp => kvp.Key,
                kvp => mapFunc(kvp.Value)
            );
        }

        public static Dictionary<string, object> MapValues(
            Dictionary<string, object> collection,
            Func<object, string, Dictionary<string, object>, int, object> iteratee
        )
        {
            var result = new Dictionary<string, object>();

            var collectionKeys = collection.Keys;
            int i = 0;
            foreach (var key in collectionKeys)
            {
                result[key] = iteratee(collection[key], key, collection, i);
                i++;
            }

            return result;
        }
    }
}