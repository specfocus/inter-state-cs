namespace XState
{
    public class TransitionConfigOrTarget<TContext, TExpressionEvent, TEvent> : SingleOrArray<TransitionConfig<TContext, TExpressionEvent, TEvent>>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
    }
}
