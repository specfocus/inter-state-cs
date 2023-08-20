namespace XState
{
    using XState.Actions;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private List<ActionEntry<TContext, TEvent>> GetActions(
            HashSet<StateNode<object, object, object, object, object, object>> resolvedConfig,
            bool isDone,
            StateTransition<TContext, TEvent> transition,
            TContext currentContext,
            SCXML.Event<TEvent> _event,
            State<TContext, object, object, object, object> prevState = null,
            PredictableActionArgumentsExec predictableExec = null
        )
        {
            List<ActionEntry<TContext, TEvent>> actions = new List<ActionEntry<TContext, TEvent>>();

            List<StateNode<object, object, object, object, object, object>> prevConfig = prevState != null
                ? GetConfiguration(
                    new List<StateNode<object, object, object, object, object, object>>(),
                    GetStateNodes(prevState.Value))
                : new List<StateNode<object, object, object, object, object, object>>();

            var entrySet = new HashSet<StateNode<object, object, object, object, object, object>>();

            foreach (var sn in resolvedConfig.OrderBy(node => node.Order))
            {
                if (!prevConfig.Contains(sn) ||
                    transition.ExitSet.Contains(sn) ||
                    (sn.Parent != null && entrySet.Contains(sn.Parent)))
                {
                    entrySet.Add(sn);
                }
            }

            foreach (var sn in prevConfig)
            {
                if (!resolvedConfig.Contains(sn) || transition.ExitSet.Contains(sn.Parent))
                {
                    transition.ExitSet.Add(sn);
                }
            }

            transition.ExitSet.Sort((a, b) => b.Order.CompareTo(a.Order));

            var entryStates = entrySet.OrderBy(node => node.Order).ToList();
            var exitStates = new HashSet<StateNode<object, object, object, object, object, object>>(transition.ExitSet);

            var doneEvents = entryStates.Select(sn =>
            {
                if (sn.Type != "final") return null;

                var parent = sn.Parent;
                var events = new List<DoneEventObject>();

                if (parent != null && parent.Parent != null)
                {
                    events.Add(done(sn.Id, sn.DoneData));
                    events.Add(done(parent.Id, sn.DoneData != null
                        ? mapContext(sn.DoneData, currentContext, _event)
                        : null));
                }

                var grandparent = parent?.Parent;

                if (grandparent?.Type == "parallel")
                {
                    if (GetChildren(grandparent).All(parentNode =>
                        IsInFinalState(transition.Configuration, parentNode)))
                    {
                        events.Add(done(grandparent.Id));
                    }
                }

                return events;
            })
            .Where(events => events != null)
            .SelectMany(events => events)
            .ToList();

            var entryActions = entryStates.Select(stateNode =>
            {
                var entryActions = stateNode.OnEntry;
                var invokeActions = stateNode.Activities.Select(activity => start(activity)).ToList();
                return new ActionEntry<TContext, TEvent>
                {
                    Type = "entry",
                    Actions = ToActionObjects(predictableExec
                        ? entryActions.Concat(invokeActions)
                        : invokeActions.Concat(entryActions),
                        this.Machine.Options.Actions)
                };
            })
            .Concat(new ActionEntry<TContext, TEvent>
            {
                Type = "state_done",
                Actions = doneEvents.Select(doneEvent => raise(doneEvent as object)).ToList()
            })
            .ToList();

            var exitActions = exitStates.Select(stateNode => new ActionEntry<TContext, TEvent>
            {
                Type = "exit",
                Actions = ToActionObjects(
                    stateNode.OnExit.Concat(stateNode.Activities.Select(activity => stop(activity as object))),
                    this.Machine.Options.Actions)
            })
            .ToList();

            actions.AddRange(exitActions);
            actions.Add(new ActionEntry<TContext, TEvent>
            {
                Type = "transition",
                Actions = ToActionObjects(
                    transition.Actions,
                    this.Machine.Options.Actions)
            });
            actions.AddRange(entryActions);

            if (isDone)
            {
                var stopActions = ToActionObjects(
                    resolvedConfig
                        .OrderBy(node => node.Order)
                        .SelectMany(stateNode => stateNode.OnExit),
                    this.Machine.Options.Actions)
                    .Where(action => !IsRaisableAction(action))
                    .ToList();

                actions.Add(new ActionEntry<TContext, TEvent>
                {
                    Type = "stop",
                    Actions = stopActions
                });
            }

            return actions;
        }

    }
}
