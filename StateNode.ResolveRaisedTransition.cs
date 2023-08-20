namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> ResolveRaisedTransition(
            State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> state,
            SCXML.Event<TEvent> _event,
            SCXML.Event<TEvent> originalEvent,
            PredictableActionArgumentsExec predictableExec = null
        )
        {
            var currentActions = state.Actions;

            state = Transition(
                state,
                _event,
                null,
                predictableExec
            );

            // Save original event to state
            // TODO: this should be the raised event! Delete in V5 (breaking)
            state._event = originalEvent;
            state.Event = originalEvent.Data;

            currentActions.InsertRange(0, state.Actions);
            return state;
        }
    }
}
