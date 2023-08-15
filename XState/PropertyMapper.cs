namespace XState
{
    public delegate TValue PropertyGetter<TContext, TEvent, TValue>(TContext context, TEvent @event)
        where TContext : class
        where TEvent : Event;

    public class PropertyMapper<TContext, TEvent, TParams> : Dictionary<string, object>
        where TContext : class
        where TEvent : Event
        where TParams : class, new()
    {
        public Dictionary<string, PropertyGetter<TContext, TEvent, object>> Getters { get; } = new();
    }
}
