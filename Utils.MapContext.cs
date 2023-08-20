namespace XState
{
    internal static partial class Utils
    {
        public static Dictionary<string, TResult> MapContext<TContext, TEvent, TResult>(
            Dictionary<string, Func<TContext, TEvent, TResult>> mapper,
            TContext context,
            SCXML.Event<TEvent> @event
        )
            where TContext : class
            where TEvent : Event
        {
            var result = new Dictionary<string, TResult>();

            foreach (var key in mapper.Keys)
            {
                var subMapper = mapper[key];
                result[key] = subMapper(context, @event.Data);
            }

            return result;
        }
    }
}
