namespace XState
{
    using XState.Actions;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private StateTransition<TContext, TEvent> Next(
            State<TContext, TEvent> state,
            SCXML.Event<TEvent> _event
        )
        {
            string eventName = _event.name;
            List<ActionObject<TContext, TEvent>> actions = new List<ActionObject<TContext, TEvent>>();

            List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> nextStateNodes = new();
            TransitionDefinition<TContext, TEvent> selectedTransition = null;

            foreach (TransitionDefinition<TContext, TEvent> candidate in GetCandidates(eventName))
            {
                var cond = candidate.cond;
                var stateIn = candidate.In;

                var resolvedContext = state.Context;

                bool isInState = true;
                if (stateIn != null)
                {
                    if (stateIn is string && IsStateId(stateIn))
                    {
                        isInState = state.Matches(
                            ToStateValue(GetStateNodeById(stateIn).Path, this.Delimiter));
                    }
                    else
                    {
                        isInState = MatchesState(
                            ToStateValue(stateIn, this.Delimiter),
                            Path(this.Path.GetRange(0, this.Path.Count - 2))(state.Value));
                    }
                }

                bool guardPassed = false;

                try
                {
                    guardPassed = cond == null || EvaluateGuard(this.Machine, cond, resolvedContext, _event, state);
                }
                catch (Exception err)
                {
                    throw new Exception($"Unable to evaluate guard '{cond?.Name ?? cond?.Type}' in transition for event '{eventName}' in state node '{this.Id}':\n{err.Message}");
                }

                if (guardPassed && isInState)
                {
                    if (candidate.Target != null)
                    {
                        nextStateNodes = candidate.Target;
                    }
                    actions.AddRange(candidate.Actions);
                    selectedTransition = candidate;
                    break;
                }
            }

            if (selectedTransition == null)
            {
                return null;
            }

            if (nextStateNodes.Count == 0)
            {
                return new StateTransition<TContext, TEvent>
                {
                    Transitions = new List<TransitionDefinition<TContext, TEvent>> { selectedTransition },
                    ExitSet = new List<StateNode<TContext, object, TEvent>>(),
                    Configuration = state.Value != null ? new List<StateNode<TContext, object, TEvent>> { this } : new List<StateNode<TContext, object, TEvent>>(),
                    Source = state,
                    Actions = actions
                };
            }

            List<StateNode<TContext, object, TEvent>> allNextStateNodes = new List<StateNode<TContext, object, TEvent>>();
            foreach (StateNode<TContext, object, TEvent> stateNode in nextStateNodes)
            {
                List<StateNode<TContext, object, TEvent>> relativeStateNodes = GetRelativeStateNodes(stateNode, state.HistoryValue);
                allNextStateNodes.AddRange(relativeStateNodes);
            }

            bool isInternal = selectedTransition.Internal == true;

            return new StateTransition<TContext, TEvent>
            {
                Transitions = new List<TransitionDefinition<TContext, TEvent>> { selectedTransition },
                ExitSet = isInternal ? new List<StateNode<TContext, object, TEvent>>() :
                    nextStateNodes.Select(targetNode => GetPotentiallyReenteringNodes(targetNode)).SelectMany(x => x).ToList(),
                Configuration = allNextStateNodes,
                Source = state,
                Actions = actions
            };
        }

    }
}
