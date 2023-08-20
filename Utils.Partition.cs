namespace XState
{
    internal static partial class Utils
    {
        public static (List<A>, List<B>) Partition<T, A, B>(List<T> items, Func<T, bool> predicate)
            where A : T
            where B : T
        {
            var truthy = new List<A>();
            var falsy = new List<B>();

            foreach (var item in items)
            {
                if (predicate(item))
                {
                    truthy.Add((A)item);
                }
                else
                {
                    falsy.Add((B)item);
                }
            }

            return (truthy, falsy);
        }
    }
}
