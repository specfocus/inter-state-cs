namespace XState
{
    public class DelayedTransitions<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public Dictionary<string, List<TransitionConfig<TContext, TEvent, TEvent>>> DelayedTransitionsDictionary { get; set; }
        public List<DelayedTransition<TContext, TEvent>> DelayedTransitionsList { get; set; }
    }
}
