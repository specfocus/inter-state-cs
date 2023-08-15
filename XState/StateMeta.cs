namespace XState
{
    /// <summary>
    /// Represents metadata associated with a state.
    /// </summary>
    public interface StateMeta<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        /// <summary>
        /// The state associated with the metadata.
        /// </summary>
        State<TContext, TEvent, object, object, object> State { get; set; }

        /// <summary>
        /// The event associated with the state.
        /// </summary>
        SCXML.Event<TEvent> Event { get; set; }
    }
}
