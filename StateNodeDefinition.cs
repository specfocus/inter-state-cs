namespace XState
{
    using XState.Actions;

    public interface StateNodeDefinition<TContext, TStateSchema, TEvent>
        where TContext : class
        where TStateSchema : StateSchema<TContext>
        where TEvent : EventObject
    {
        string Id { get; }
        string Version { get; }
        string Key { get; }
        TContext Context { get; }
        string Type { get; }
        StateNodeConfig<TContext, TStateSchema, TEvent> Initial { get; }
        object History { get; }
        StatesDefinition<TContext, TStateSchema, TEvent> States { get; }
        TransitionDefinitionMap<TContext, TEvent> On { get; }
        IEnumerable<TransitionDefinition<TContext, TEvent>> Transitions { get; }
        IEnumerable<ActionObject<TContext, TEvent>> Entry { get; }
        IEnumerable<ActionObject<TContext, TEvent>> Exit { get; }
        IEnumerable<ActivityDefinition<TContext, TEvent>> Activities { get; }
        object Meta { get; }
        int Order { get; }
        FinalStateNodeConfig<TContext, TEvent> Data { get; }
        IEnumerable<InvokeDefinition<TContext, TEvent>> Invoke { get; }
        string Description { get; }
        IEnumerable<string> Tags { get; }
    }

}
