namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        public List<DelayedTransitionDefinition<TContext, TEvent>> After => __cache.DelayedTransitions ??= GetDelayedTransitions();
    }
}
