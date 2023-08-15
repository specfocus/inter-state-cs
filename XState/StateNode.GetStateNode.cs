namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Returns the child state node from its relative `stateKey`, or throws.
        /// </summary>
        /// <param name="stateKey"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public StateNode<TContext, dynamic, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> GetStateNode(string stateKey)
        {
            if (IsStateId(stateKey))
            {
                return Machine.GetStateNodeById(stateKey);
            }

            if (States == null)
            {
                throw new Exception($"Unable to retrieve child state '{stateKey}' from '{Id}'; no child states exist.");
            }

            if (!States.ContainsKey(stateKey))
            {
                throw new Exception($"Child state '{stateKey}' does not exist on '{Id}'");
            }

            return States[stateKey];
        }
    }
}
