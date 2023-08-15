namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Returns the state node with the given `stateId`, or throws.
        /// </summary>
        /// <param name="stateId">The state ID.The prefix "#" is removed.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> GetStateNodeById(string stateId)
        {
            var resolvedStateId = IsStateId(stateId)
                ? stateId.Substring(Constants.STATE_IDENTIFIER.Length)
                : stateId;

            if (resolvedStateId == Id)
            {
                return this;
            }

            var stateNode = Machine.IdMap.GetValueOrDefault(resolvedStateId);

            if (stateNode == null)
            {
                throw new Exception($"Child state node '#{resolvedStateId}' does not exist on machine '{Id}'");
            }

            return stateNode;
        }
    }
}
