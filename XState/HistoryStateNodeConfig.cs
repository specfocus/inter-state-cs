namespace XState
{
    public interface HistoryStateNodeConfig<TContext, TEvent>
        : AtomicStateNodeConfig<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        string History { get; }

        StateValue Target { get; }
    }
}
