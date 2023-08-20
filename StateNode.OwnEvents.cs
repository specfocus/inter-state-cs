namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// All the events that have transitions directly from this state node.
        /// Excludes any inert events.
        /// </summary>
        public List<TEvent["type"]> OwnEvents
        {
            get
            {
                var events = new HashSet<TEvent["type"]>(
                    Transitions
                        .Where(transition =>
                            !(transition.Target == null && !transition.Actions.Any() && transition.Internal)
                        )
                        .Select(transition => transition.EventType)
                );

                return events.ToList();
            }
        }
    }
}
