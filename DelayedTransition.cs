namespace XState
{
    using XState.Actions;

    public class DelayedTransition<TContext, TEvent> : TransitionConfig<TContext, TEvent, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public Delay<TContext, TEvent> Delay { get; set; } // Can be number, string, or expression
    }
}
