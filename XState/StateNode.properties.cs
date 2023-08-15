using System.Collections.Generic;

namespace XState
{
    using XState.Actions;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        // Whether the state node is "transient".
        private bool _transient;

        // The relative key of the state node, which represents its location in the overall state value.
        public string Key { get; set; }

        // The unique ID of the state node.
        public string Id { get; set; }

        // The machine"s own version.
        public string Version { get; set; }

        // The type of this state node.
        public string Type { get; set; }

        // The string path from the root machine node to this node.
        public List<string> Path { get; set; }

        // The initial state node key.
        public string Initial { get; set; }

        // The child state nodes.
        public List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> States { get; set; }

        // The type of history on this state node.
        public string History { get; set; }

        // The action(s) to be executed upon entering the state node.
        public List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> OnEntry { get; set; }

        // The action(s) to be executed upon exiting the state node.
        public List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> OnExit { get; set; }

        /// <summary>
        /// The activities to be started upon entering the state node, and stopped upon exiting the state node.
        /// </summary>
        public List<ActivityDefinition<TContext, TEvent>> Activities { get; set; }

        // Whether the state node is strict.
        public bool Strict { get; set; }

        

        // The root machine node.
        public StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> Machine { get; set; }

        /// <summary>
        /// The meta data associated with this state node, which will be returned in State instances.
        /// </summary>
        public object Meta { get; set; }

        // The data sent with the "done.state._id_" event if this is a final state node.
        public object DoneData { get; set; }

        // The string delimiter for serializing the path to a string.
        public string Delimiter { get; set; }

        // The order this state node appears.
        public int Order { get; set; } = -1;

        // The services invoked by this state node.
        public List<InvokeDefinition<TContext, TEvent>> Invoke { get; set; }

        // The machine options associated with this state node.
        public MachineOptions<TContext, TEvent> Options { get; set; }

        // The machine schema associated with this state node.
        public MachineSchema<TContext, TEvent, TServiceMap> Schema { get; set; }

        // A flag indicating this is a StateNode.
        public bool XStateNode { get; set; } = true;

        // The description of this state node.
        public string Description { get; set; }

        // ID map for state nodes
        private Dictionary<string, StateNode<TContext, object, TEvent, object, object, object>> idMap = new Dictionary<string, StateNode<TContext, object, TEvent, object, object, object>>();

        // Tags associated with the state node
        public List<string> Tags { get; set; } = new List<string>();
    }
}
