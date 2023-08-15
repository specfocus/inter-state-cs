namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// All the transitions that can be taken from this state node.
        /// </summary>
        public List<TransitionDefinition<TContext, TEvent>> Transitions => __cache.Transitions ??= FormatTransitions();
    }
}
