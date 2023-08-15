namespace XState
{
    using XState.Actions;

    public interface StateTransition<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        IList<TransitionDefinition<TContext, TEvent>> Transitions { get; }

        IList<StateNode<TContext, object, TEvent, object, object, object>> Configuration { get; }

        IList<StateNode<TContext, object, TEvent, object, object, object>> ExitSet { get; }

        State<TContext, object, object, object, object> Source { get; }

        IList<ActionObject<TContext, TEvent>> Actions { get; }
    }
}
