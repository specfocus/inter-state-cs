namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Clones this state machine with custom context.
        /// </summary>
        /// <param name="context">Custom context(will override predefined context, not recursive)</param>
        /// <returns></returns>
        public StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> WithContext(
            ContextProvider<TContext> context
        )
        {
            return new StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>(
                Config,
                Options,
                context
            );
        }

    }
}
