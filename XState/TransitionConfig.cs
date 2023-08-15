namespace XState
{
    /// <summary>
    /// Configuration for a transition between states.
    /// </summary>
    public interface ITransitionConfig<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        /// <summary>
        /// The condition that must be satisfied for this transition to be taken.
        /// </summary>
        Condition<TContext, TExpressionEvent> Cond { get; }

        /// <summary>
        /// The actions to be executed when this transition is taken.
        /// </summary>
        BaseActions<TContext, TExpressionEvent, TEvent, Actions.BaseActionObject> Actions { get; }

        /// <summary>
        /// The state value in which the transition is defined to be taken.
        /// </summary>
        StateValue In { get; set; }

        /// <summary>
        /// Indicates whether the transition is an internal transition.
        /// </summary>
        bool Internal { get; }

        /// <summary>
        /// The target state or states of the transition.
        /// </summary>
        TransitionTarget<TContext, TEvent> Target { get; }

        /// <summary>
        /// Additional metadata associated with this transition.
        /// </summary>
        Dictionary<string, object> Meta { get; }

        /// <summary>
        /// A description of the transition.
        /// </summary>
        string Description { get; }
    }

    /// <summary>
    /// Configuration for a transition between states.
    /// </summary>
    public class TransitionConfig<TContext, TExpressionEvent, TEvent> : ITransitionConfig<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        /// <summary>
        /// The condition that must be satisfied for this transition to be taken.
        /// </summary>
        public Condition<TContext, TExpressionEvent> Cond { get; set; }

        /// <summary>
        /// The actions to be executed when this transition is taken.
        /// </summary>
        public BaseActions<TContext, TExpressionEvent, TEvent, Actions.BaseActionObject> Actions { get; set; }

        /// <summary>
        /// The state value in which the transition is defined to be taken.
        /// </summary>
        public StateValue In { get; set; }

        /// <summary>
        /// Indicates whether the transition is an internal transition.
        /// </summary>
        public bool Internal { get; set; }

        /// <summary>
        /// The target state or states of the transition.
        /// </summary>
        public TransitionTarget<TContext, TEvent> Target { get; set; }

        /// <summary>
        /// Additional metadata associated with this transition.
        /// </summary>
        public Dictionary<string, object> Meta { get; set; }

        /// <summary>
        /// A description of the transition.
        /// </summary>
        public string Description { get; set; }
    }

    public class TransitionConfig<TContext, TEvent> where TEvent : EventObject
    {
        public string Event { get; set; } // Use appropriate type for event
                                          // Other properties of TransitionConfig
    }

    public class TransitionsConfigMap<TContext, TEvent> : Dictionary<string, TransitionConfigOrTarget<TContext, TEvent, TEvent>>
        where TContext : class
        where TEvent : Event
    {
    }

    public class TransitionsConfigArray<TContext, TEvent> : List<TransitionConfigOrTarget<TContext, TEvent, TEvent>>
        where TContext : class
        where TEvent : Event
    {
    }

    public class TransitionsConfig<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public TransitionsConfigMap<TContext, TEvent> Map { get; set; }

        public TransitionsConfigArray<TContext, TEvent> Array { get; set; }
    }
}
