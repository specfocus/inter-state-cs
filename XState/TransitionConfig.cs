namespace XState
{
    /// <summary>
    /// Configuration for a transition between states.
    /// </summary>
    public interface TransitionConfig<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        /// <summary>
        /// The condition that must be satisfied for this transition to be taken.
        /// </summary>
        Condition<TContext, TExpressionEvent> Cond { get; set; }

        /// <summary>
        /// The actions to be executed when this transition is taken.
        /// </summary>
        BaseActions<TContext, TExpressionEvent, TEvent, Actions.BaseActionObject> Actions { get; set; }

        /// <summary>
        /// The state value in which the transition is defined to be taken.
        /// </summary>
        StateValue In { get; set; }

        /// <summary>
        /// Indicates whether the transition is an internal transition.
        /// </summary>
        bool Internal { get; set; }

        /// <summary>
        /// The target state or states of the transition.
        /// </summary>
        TransitionTarget<TContext, TEvent> Target { get; set; }

        /// <summary>
        /// Additional metadata associated with this transition.
        /// </summary>
        Dictionary<string, object> Meta { get; set; }

        /// <summary>
        /// A description of the transition.
        /// </summary>
        string Description { get; set; }
    }
}
