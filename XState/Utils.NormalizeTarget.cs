namespace XState
{
    internal static partial class Utils
    {
        public static List<object> NormalizeTarget<TContext, TEvent>(
            SingleOrArray<object> target
        )
            where TContext : class
            where TEvent : Event
        {
            var normalizedTargets = new List<object>();

            foreach (var item in target.ToArray())
            {
                if (item is string || item is StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, object>)
                {
                    normalizedTargets.Add(item);
                }
            }

            return normalizedTargets;
        }
    }
}
