namespace XState
{
    /// <summary>
    /// Represents metadata associated with a guard.
    /// </summary>
    public interface GuardMeta<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        /// <summary>
        /// The metadata associated with the current state.
        /// </summary>
        StateMeta<TContext, TEvent> StateMeta { get; set; }

        /// <summary>
        /// The guard associated with the metadata.
        /// </summary>
        Guard<TContext, TEvent> Condition { get; set; }
    }
}
