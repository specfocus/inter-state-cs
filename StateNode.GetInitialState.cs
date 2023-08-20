namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        public State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> GetInitialState(
            StateValue stateValue,
            TContext context = default
        )
        {
            this.Init(); // TODO: this should be in the constructor (see note in constructor)
            var configuration = this.GetStateNodes(stateValue);

            return this.ResolveTransition(
                new TransitionConfig<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta>
                {
                    Configuration = configuration,
                    ExitSet = new List<State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta>>(),
                    Transitions = new List<TransitionDefinition<TContext, TEvent>>(),
                    Source = default,
                    Actions = new List<ActionObject<TContext, TEvent>>()
                },
                default,
                context != null ? context : this.Machine.Context,
                default
            );
        }
    }
}
