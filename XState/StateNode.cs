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
        /// <summary>
        /// The activities to be started upon entering the state node, and stopped upon exiting the state node.
        /// </summary>
        List<ActivityDefinition<TContext, TEvent>> Activities { get; }

        StateNodeConfig<TContext, TStateSchema, TEvent, BaseActionObject> Config { get; }

        /// <summary>
        /// The string delimiter for serializing the path to a string.
        /// </summary>
        string Delimiter { get; }

        /// <summary>
        /// The description of this state node.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The data sent with the "done.state._id_" event if this is a final state node.
        /// </summary>
        object? DoneData { get; }

        /// <summary>
        /// The type of history on this state node.
        /// </summary>
        string History { get; }

        /// <summary>
        /// The unique ID of the state node.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The initial state node key.
        /// </summary>
        string Initial { get; }

        /// <summary>
        /// The services invoked by this state node.
        /// </summary>
        List<InvokeDefinition<TContext, TEvent>> Invoke { get; }

        /// <summary>
        /// The relative key of the state node, which represents its location in the overall state value.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// The root machine node.
        /// </summary>
        StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> Machine { get; }

        /// <summary>
        /// The meta data associated with this state node, which will be returned in State instances.
        /// </summary>
        object Meta { get; }

        /// <summary>
        /// The action(s) to be executed upon entering the state node.
        /// </summary>
        List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> OnEntry { get; }

        /// <summary>
        /// The action(s) to be executed upon exiting the state node.
        /// </summary>
        List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> OnExit { get; }

        /// <summary>
        /// The machine options associated with this state node.
        /// </summary>
        MachineOptions<TContext, TEvent> Options { get; }

        /// <summary>
        /// The order this state node appears.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// The string path from the root machine node to this node.
        /// </summary>
        List<string> Path { get; }

        /// <summary>
        /// The parent state node.
        /// </summary>
        StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> Parent { get; }

        /// <summary>
        /// The child state nodes.
        /// </summary>
        Dictionary<string, StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> States { get; }

        /// <summary>
        /// The machine schema associated with this state node.
        /// </summary>
        IMachineSchema<TContext, TEvent, TServiceMap> Schema { get; }

        /// <summary>
        /// Whether the state node is strict.
        /// </summary>
        bool Strict { get; }

        /// <summary>
        /// Tags associated with the state node
        /// </summary>
        List<string> Tags { get; }

        /// <summary>
        /// The type of this state node.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// The machine"s own version.
        /// </summary>
       string Version { get; }

        /// <summary>
        /// A flag indicating this is a StateNode.
        /// </summary>
        bool XStateNode { get; }
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
        // ID map for state nodes
        private Dictionary<string, StateNode<TContext, object, TEvent, object, object, object>> idMap = new Dictionary<string, StateNode<TContext, object, TEvent, object, object, object>>();

        /// <summary>
        /// Whether the state node is "transient".
        /// </summary>
        private bool _transient;

        /// <summary>
        /// The activities to be started upon entering the state node, and stopped upon exiting the state node.
        /// </summary>
        public List<ActivityDefinition<TContext, TEvent>> Activities => (Config.Activities ?? new List<ActivityDefinition<TContext, TEvent>>()).Concat(Invoke).Select(activity => ToActivityDefinition(activity)).ToList();

        public StateNodeConfig<TContext, TStateSchema, TEvent, BaseActionObject> Config { get; }

        /// <summary>
        /// The string delimiter for serializing the path to a string.
        /// </summary>
        public string Delimiter => Config.Delimiter ?? Parent?.Delimiter ?? Constants.STATE_DELIMITER;

        /// <summary>
        /// The description of this state node.
        /// </summary>
        public string Description => Config.Description;

        /// <summary>
        /// The data sent with the "done.state._id_" event if this is a final state node.
        /// </summary>
        public object? DoneData => Type == "final" ? ((FinalStateNodeConfig<TContext, TEvent>)Config).Data : null;

        /// <summary>
        /// The type of history on this state node.
        /// </summary>
        public string History => Config.History == true ? "shallow" : Config.History ?? false;

        /// <summary>
        /// The unique ID of the state node.
        /// </summary>
        public string Id => Config.Id ?? string.Join(Delimiter, new[] { Machine.Key }.Concat(Path));

        /// <summary>
        /// The initial state node key.
        /// </summary>
        public string Initial => Config.Initial;

        /// <summary>
        /// The services invoked by this state node.
        /// </summary>
        public List<InvokeDefinition<TContext, TEvent>> Invoke => (Config.Invoke ?? new List<IInvokeConfig<TContext, TEvent>>()).Select((invokeConfig, i) =>
        {
            if (invokeConfig is AnyStateMachine invokeMachine)
            {
                string invokeId = CreateInvokeId(Id, i);
                Machine.Options.Services[invokeId] = invokeMachine;
                return new InvokeConfig<TContext, TEvent> { Src = invokeId, Id = invokeId };
            }
            else if (invokeConfig.src is string srcString)
            {
                string invokeId = invokeConfig.id ?? CreateInvokeId(Id, i);
                return new InvokeConfig<TContext, TEvent> { Src = srcString, Id = invokeId };
            }
            else if (invokeConfig.src is AnyStateMachine || invokeConfig.src is Delegate)
            {
                string invokeId = invokeConfig.id ?? CreateInvokeId(Id, i);
                Machine.Options.Services[invokeId] = (InvokeCreator<TContext, TEvent>)invokeConfig.src;
                return new InvokeConfig<TContext, TEvent> { Src = invokeId, Id = invokeId };
            }
            else if (invokeConfig.src is InvokeSourceDefinition invokeSource)
            {
                return new InvokeConfig<TContext, TEvent> { Id = CreateInvokeId(Id, i), Src = invokeSource };
            }
            return default;
        }).ToList();

        /// <summary>
        /// The relative key of the state node, which represents its location in the overall state value.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// The root machine node.
        /// </summary>
        public StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> Machine => Parent != null ? Parent.Machine as StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> : this;

        /// <summary>
        /// The meta data associated with this state node, which will be returned in State instances.
        /// </summary>
        public object Meta => Config.Meta;

        /// <summary>
        /// The action(s) to be executed upon entering the state node.
        /// </summary>
        public List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> OnEntry => (Config.Entry ?? Config.OnEntry).Select(action => ToActionObject(action)).ToList();

        /// <summary>
        /// The action(s) to be executed upon exiting the state node.
        /// </summary>
        public List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> OnExit => (Config.Exit ?? Config.OnExit).Select(action => ToActionObject(action)).ToList();

        /// <summary>
        /// The machine options associated with this state node.
        /// </summary>
        public MachineOptions<TContext, TEvent> Options { get; }

        /// <summary>
        /// The order this state node appears.
        /// </summary>
        public int Order { get; private set; } = -1;

        /// <summary>
        /// The string path from the root machine node to this node.
        /// </summary>
        public List<string> Path => Parent != null ? Parent.Path.Concat(new List<string> { Key }).ToList() : new List<string>();

        /// <summary>
        /// The parent state node.
        /// </summary>
        public StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>? Parent { get; }

        /// <summary>
        /// The child state nodes.
        /// </summary>
        public Dictionary<string, StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> States { get; }

        /// <summary>
        /// The machine schema associated with this state node.
        /// </summary>
        public IMachineSchema<TContext, TEvent, TServiceMap> Schema => Parent != null ? Machine.Schema : (Config as MachineConfig<TContext, TStateSchema, TEvent, BaseActionObject, TServiceMap, object>).Schema;

        /// <summary>
        /// Whether the state node is strict.
        /// </summary>
        public bool Strict => Config.Strict == true;

        /// <summary>
        /// Tags associated with the state node
        /// </summary>
        public List<string> Tags => (Config.Tags ?? new List<string>()).ToList();

        /// <summary>
        /// The type of this state node.
        /// </summary>
        public string Type => Config.Type ?? (Config.Parallel ? "parallel" : (Config.States?.Count > 0 ? "compound" : (Config.History != null ? "history" : "atomic")));

        /// <summary>
        /// The machine"s own version.
        /// </summary>
        public string Version => Parent != null ? Parent.Version : (Config as MachineConfig<TContext, TStateSchema, TEvent, BaseActionObject, TServiceMap, object>).Version;

        /// <summary>
        /// A flag indicating this is a StateNode.
        /// </summary>
        public bool XStateNode { get; } = true;
    }
}
