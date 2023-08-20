namespace XState
{
    using Any = Object;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Resolves the given `state` to a new `State` instance relative to this machine.
        /// This ensures that `.events` and `.nextEvents` represent the correct values.
        /// </summary>
        /// <param name="state">The state to resolve</param>
        /// <returns></returns>
        public State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> ResolveState(
            State<TContext, TEvent, Any, Any> state
        )
        {
            State<TContext, TEvent, Any, Any> stateFromConfig = state;

            if (!(state is State<TContext, TEvent, Any, Any>))
            {
                stateFromConfig = State<TContext, TEvent>.Create(state);
            }
            var configuration = GetConfiguration(new List<StateNode<TContext, Any, TEvent, Any, Any, Any>>(), this.GetStateNodes(stateFromConfig.Value)).ToList();
            return new State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta>(
            stateFromConfig with
            {
                Value = Resolve(stateFromConfig.Value),
                Configuration = configuration,
                Done = IsInFinalState(configuration, this),
                Tags = GetTagsFromConfiguration(configuration),
                    Machine = this.Machine as AnyStateMachine
                });
        }
    }
}
