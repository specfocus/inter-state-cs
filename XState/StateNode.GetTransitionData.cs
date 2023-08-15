namespace XState
{
    using static XState.SCXML;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        public TransitionData<TContext, TEvent> GetTransitionData(
            State<TContext, TEvent> state,
            Event<TEvent> @event
        )
        {
            SCXML.Event<TEvent> scxmlEvent = Utils.ToSCXMLEvent(@event);
            return _Transition(state.Value, state, scxmlEvent);
        }

    }
}
