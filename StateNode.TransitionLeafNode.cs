namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private StateTransition<TContext, TEvent> TransitionLeafNode(
            string stateValue,
            State<TContext, TEvent> state,
            SCXML.Event<TEvent> _event
        )
        {
            StateNode<TContext, TEvent> stateNode = GetStateNode(stateValue);
            StateTransition<TContext, TEvent> next = stateNode.Next(state, _event);

            if (next == null || next.Transitions.Count == 0)
            {
                return Next(state, _event);
            }

            return next;
        }
    }
}
