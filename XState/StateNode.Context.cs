namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private readonly ContextProvider<TContext> _context;

        public TContext Context => _context;
    }
}
