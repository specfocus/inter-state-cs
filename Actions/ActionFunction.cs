namespace XState.Actions
{
    /// <summary>
    /// Represents the signature of an action function.
    /// </summary>
    public delegate void ActionFunction<TContext, TExpressionEvent, TEvent, TAction>(
        TContext context,
        TExpressionEvent @event,
        ActionMeta<TContext, TEvent, TAction> meta
    )
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
        where TAction : BaseActionObject;
}
