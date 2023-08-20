namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// All the event types accepted by this state node and its descendants.
        /// </summary>
        public List<TEvent["type"]> Events
        {
            get
            {
                if (this.__cache.Events != null)
                {
                    return this.__cache.Events;
                }

                var events = new HashSet<TEvent["type"]>(OwnEvents);

                if (States != null)
                {
                    foreach (var state in States.Values)
                    {
                        if (state.States != null)
                        {
                            foreach (var stateEvent in state.Events)
                            {
                                events.Add($"{stateEvent}");
                            }
                        }
                    }
                }

                return (this.__cache.Events = events.ToList());
            }
        }
    }
}
