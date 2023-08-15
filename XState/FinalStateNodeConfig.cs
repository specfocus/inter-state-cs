namespace XState
{
    public interface FinalStateNodeConfig<TContext, TEvent>
        : AtomicStateNodeConfig<TContext, TEvent>
        where TContext : class
        where TEvent : EventObject
    {
        string Type { get; }
        Func<TContext, TEvent, object> Data { get; }
    }
}
