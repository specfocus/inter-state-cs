namespace XState
{
    using XState.Actions;

    public interface IStateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
        where TContext : class
        where TStateSchema : IStateSchema<TContext>
        where TEvent : Event
        where TTypestate : Typestate<TContext>
        where TServiceMap : ServiceMap
        where TResolvedTypesMeta : TypegenFlag // TypegenDisabled
    {
        // The relative key of the state node, which represents its location in the overall state value.
        string Key { get; }

        // The unique ID of the state node.
        string Id { get; }

        // The machine"s own version.
        string Version { get; }

        // The type of this state node.
        string Type { get; }

        // The string path from the root machine node to this node.
        List<string> Path { get; }

        // The initial state node key.
        string Initial { get; }

        // The child state nodes.
        List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> States { get; }

        // The type of history on this state node.
        public string History { get; set; }

        // The action(s) to be executed upon entering the state node.
        List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> OnEntry { get; }

        // The action(s) to be executed upon exiting the state node.
        List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> OnExit { get; }

        /// <summary>
        /// The activities to be started upon entering the state node, and stopped upon exiting the state node.
        /// </summary>
        List<ActivityDefinition<TContext, TEvent>> Activities { get; }

        /// <summary>
        /// Whether the state node is strict.
        /// </summary>
        bool Strict { get; }

        /// <summary>
        /// The parent state node.
        /// </summary>
        StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> Parent { get; }

        // The root machine node.
        StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> Machine { get; }

        // The meta data associated with this state node.
        object Meta { get; }

        // The data sent with the "done.state._id_" event if this is a final state node.
        object DoneData { get; }

        // The string delimiter for serializing the path to a string.
        string Delimiter { get; }

        // The order this state node appears.
        int Order { get; }

        // The services invoked by this state node.
        List<InvokeDefinition<TContext, TEvent>> Invoke { get; }

        // The machine options associated with this state node.
        MachineOptions<TContext, TEvent> Options { get; }

        // The machine schema associated with this state node.
        MachineSchema<TContext, TEvent, TServiceMap> Schema { get; }

        // A flag indicating this is a StateNode.
        bool XStateNode { get; }

        // The description of this state node.
        string Description { get; }
    }

    public partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
        : IStateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
       where TContext : class
       where TStateSchema : IStateSchema<TContext>
       where TEvent : Event
       where TTypestate : Typestate<TContext>
       where TServiceMap : ServiceMap
       where TResolvedTypesMeta : TypegenFlag // TypegenDisabled
    {
    }
}
