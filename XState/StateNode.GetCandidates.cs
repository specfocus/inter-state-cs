namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private List<TransitionDefinition<TContext, TEvent>> GetCandidates(string eventName)
        {
            if (__cache.Candidates.ContainsKey(eventName))
            {
                return __cache.Candidates[eventName];
            }

            bool transient = eventName == Constants.NULL_EVENT;

            List<TransitionDefinition<TContext, TEvent>> candidates = Transitions
                .Where(transition =>
                    (transient && transition.EventType == eventName) ||
                    (!transient && (transition.EventType == eventName || transition.EventType == Constants.WILDCARD)))
                .ToList();

            __cache.Candidates[eventName] = candidates;
            return candidates;
        }
    }
}
