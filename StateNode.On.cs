namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// The mapping of events to transitions.
        /// </summary>
        public TransitionDefinitionMap<TContext, TEvent> On
        {
            get
            {
                if (__cache.On != null)
                {
                    return __cache.On;
                }

                var transitions = Transitions;
                __cache.On = transitions.Aggregate(
                    new TransitionDefinitionMap<TContext, TEvent>(),
                    (map, transition) =>
                    {
                        if (!map.ContainsKey(transition.EventType))
                        {
                            map[transition.EventType] = new List<TransitionDefinition<TContext, TEvent>>();
                        }

                        map[transition.EventType].Add(transition);
                        return map;
                    });

                return __cache.On;
            }
        }
    }
}
