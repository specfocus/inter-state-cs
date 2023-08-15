namespace XState
{
    using XState.Actions;
    using static XState.SCXML;

    public class TransitionData<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public State<TContext, TEvent> Source { get; set; }
        public State<TContext, TEvent> Target { get; set; }
        public Event<TEvent> Event { get; set; }
        public List<ActionObject<TContext, TEvent>> Actions { get; set; }
    }
}
