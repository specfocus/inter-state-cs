namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// The well-structured state node definition.
        /// </summary>
        public StateNodeDefinition<TContext, TStateSchema, TEvent> Definition
        {
            get
            {
                return new StateNodeDefinition<TContext, TStateSchema, TEvent>
                {
                    Id = Id,
                    Key = Key,
                    Version = Version,
                    Context = Context,
                    Type = Type,
                    Initial = Initial,
                    History = History,
                    States = States.ToDictionary(
                        state => state.Key,
                        state => state.Definition
                    ),
                    On = On,
                    Transitions = Transitions,
                    Entry = OnEntry,
                    Exit = OnExit,
                    Activities = Activities ?? new List<ActivityDefinition<TContext, TEvent>>(),
                    Meta = Meta,
                    Order = Order ?? -1,
                    Data = DoneData,
                    Invoke = Invoke,
                    Description = Description,
                    Tags = Tags
                };
            }
        }
    }
}
