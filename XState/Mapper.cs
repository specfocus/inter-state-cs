namespace XState
{
    public delegate TParams Mapper<TContext, TEvent, TParams>(TContext context, TEvent @event)
        where TContext : class
        where TEvent : Event
        where TParams : class, new();
}
