namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// All the state node IDs of this state node and its descendant state nodes.
        /// </summary>
        public List<string> StateIds
        {
            get
            {
                var childStateIds = States.Values.SelectMany(state => state.StateIds).ToList();
                return new List<string> { Id }.Concat(childStateIds).ToList();
            }
        }
    }
}
