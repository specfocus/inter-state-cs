namespace XState
{
    internal static partial class Utils
    {
        public static IEnumerable<T> Flatten<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            foreach (var sequence in sequences)
            {
                foreach (var item in sequence)
                {
                    yield return item;
                }
            }
        }
    }
}
