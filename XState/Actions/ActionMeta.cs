namespace XState.Actions
{
    /// <summary>
    /// Represents metadata associated with an action.
    /// </summary>
    public interface ActionMeta<TContext, TEvent, TAction>
        where TContext : class
        where TEvent : Event
        where TAction : BaseActionObject
    {
        /// <summary>
        /// The metadata associated with the current state.
        /// </summary>
        StateMeta<TContext, TEvent> StateMeta { get; set; }

        /// <summary>
        /// The action being performed.
        /// </summary>
        TAction Action { get; set; }

        /// <summary>
        /// The event associated with the action.
        /// </summary>
        SCXML.Event<TEvent> Event { get; set; }
    }
}
