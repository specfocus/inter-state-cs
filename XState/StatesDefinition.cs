namespace XState
{
    public class StatesDefinition<TContext, TStateSchema, TEvent>
        where TContext : class
        where TStateSchema : StateSchema<TContext>
        where TEvent : EventObject
    {
        public Dictionary<string, StateNodeDefinition<TContext, TStateSchema, TEvent>> StateNodes { get; set; }
    }
}
