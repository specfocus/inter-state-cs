namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private StateTransition<TContext, TEvent> TransitionCompoundNode(
            StateValueMap stateValue,
            State<TContext, TEvent> state,
            SCXML.Event<TEvent> @event
        )
        {
            string[] subStateKeys = stateValue.Keys.ToArray();

            StateNode<TContext, TEvent> stateNode = GetStateNode(subStateKeys[0]);
            StateTransition<TContext, TEvent> next = stateNode._Transition(
                stateValue[subStateKeys[0]],
                state,
                @event);

            if (next == null || next.Transitions.Count == 0)
            {
                return Next(state, @event);
            }

            return next;
        }
    }
}
