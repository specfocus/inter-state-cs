namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        public List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> GetRelativeStateNodes(
            StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> relativeStateId,
            HistoryValue historyValue = null,
            bool resolve = true
        )
        {
            return resolve
                ? relativeStateId.Type == "history"
                    ? relativeStateId.ResolveHistory(historyValue)
                    : relativeStateId.InitialStateNodes
                : new List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> { relativeStateId };
        }
    }
}
