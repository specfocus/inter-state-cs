namespace XState
{
    using static XState.SCXML;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private StateTransition<TContext, TEvent> TransitionParallelNode(
            StateValueMap stateValue,
            State<TContext, TEvent> state,
            Event<TEvent> _event)
        {
            Dictionary<string, StateTransition<TContext, TEvent>> transitionMap =
                new Dictionary<string, StateTransition<TContext, TEvent>>();

            foreach (string subStateKey in stateValue.Keys)
            {
                StateValue subStateValue = stateValue[subStateKey];

                if (subStateValue == null)
                {
                    continue;
                }

                StateNode<TContext, TEvent> subStateNode = GetStateNode(subStateKey);
                StateTransition<TContext, TEvent> next =
                    subStateNode._Transition(subStateValue, state, _event);

                if (next != null)
                {
                    transitionMap[subStateKey] = next;
                }
            }

            List<StateTransition<TContext, TEvent>> stateTransitions =
                transitionMap.Values.ToList();
            List<StateTransition<TContext, TEvent>> enabledTransitions =
                stateTransitions.SelectMany(st => st.Transitions).ToList();

            bool willTransition = stateTransitions.Any(st => st.Transitions.Count > 0);

            if (!willTransition)
            {
                return Next(state, _event);
            }

            List<State<TContext, TEvent>> configuration =
                transitionMap.Values.SelectMany(st => st.Configuration).ToList();

            List<ActionObject<TContext, TEvent>> actions =
                transitionMap.Values.SelectMany(st => st.Actions).ToList();

            return new StateTransition<TContext, TEvent>
            {
                Transitions = enabledTransitions,
                ExitSet = stateTransitions.SelectMany(t => t.ExitSet).ToList(),
                Configuration = configuration,
                Source = state,
                Actions = actions
            };
        }
    }
}
