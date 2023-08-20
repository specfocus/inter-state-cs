namespace XState
{
    using Actions;

    public interface TransitionDefinition<TContext, TEvent> : TransitionConfig<TContext, TEvent, TEvent>
        where TContext : class
        where TEvent : Event
    {
        StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, object>[] Target { get; set; }

        StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, object> Source { get; set; }

        ActionObject<TContext, TEvent, TEvent, BaseActionObject>[] Actions { get; set; }

        Guard<TContext, TEvent> Cond { get; set; }

        string eventType { get; set; }

        // Define the toJSON method
        Dictionary<string, object> toJSON();
    }

    public class TransitionDefinitionMap<TContext, TEvent> : Dictionary<string, List<TransitionDefinition<TContext, TEvent>>>
        where TContext : class
        where TEvent : Event
    {
        // Additional methods or properties if needed
    }

    public interface DelayedTransitionDefinition<TContext, TEvent> : TransitionDefinition<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        Delay<TContext, TEvent> Delay { get; set; } // You can replace 'object' with a more specific type if needed
    }
}
