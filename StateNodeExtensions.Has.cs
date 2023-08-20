namespace XState
{
    static partial class StateNodeExtensions
    {
        public static bool Has<T>(IEnumerable<T> collection, T item)
        {
            if (collection is T[] array)
            {
                return array.Contains(item);
            }

            if (collection is HashSet<T> hashSet)
            {
                return hashSet.Contains(item);
            }

            return false; // TODO: Fix this part
        }
    }
}
