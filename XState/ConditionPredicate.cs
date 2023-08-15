namespace XState
{
    /// <summary>
    /// Represents a condition predicate function.
    /// </summary>
    public delegate bool ConditionPredicate<TContext, TEvent>(
        TContext context,
        TEvent @event,
        GuardMeta<TContext, TEvent> meta
    )
        where TContext : class
        where TEvent : Event;
}
