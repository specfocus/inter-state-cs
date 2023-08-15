namespace XState
{
    using XState.Actions;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> ResolveTransition(
            StateTransition<TContext, TEvent> stateTransition,
            State<TContext, TEvent, object, object, object> currentState,
            TContext context,
            PredictableActionArgumentsExec predictableExec = null,
            SCXML.Event<TEvent> _event = null
        )
        {
            var configuration = stateTransition.Configuration;

            var willTransition = currentState == null || stateTransition.Transitions.Count > 0;

            var resolvedConfiguration = willTransition
                ? stateTransition.Configuration
                : currentState != null
                    ? currentState.Configuration
                    : new List<StateNode<TContext, object, TEvent, object, object, object>>();

            var isDone = IsInFinalState(resolvedConfiguration, this);

            var resolvedStateValue = willTransition
                ? GetValue(this.Machine, configuration)
                : default;

            HistoryValue historyValue = null;
            if (currentState != null)
            {
                historyValue = currentState.HistoryValue ??
                    (stateTransition.Source != null
                        ? (this.Machine.HistoryValue(currentState.Value) as HistoryValue)
                        : null);
            }

            var actionBlocks = GetActions(
                new HashSet<StateNode<TContext, object, TEvent, object, object, object>>(resolvedConfiguration),
                isDone,
                stateTransition,
                context,
                _event,
                currentState,
                predictableExec
            );

            var activities = currentState != null
                ? new Dictionary<string, ActivityDefinition<TContext, TEvent>> { ...currentState.Activities }
                : new Dictionary<string, ActivityDefinition<TContext, TEvent>>();

            foreach (var block in actionBlocks)
            {
                foreach (var action in block.Actions)
                {
                    if (action.Type == ActionTypes.Start)
                    {
                        var activityAction = action as ActivityActionObject<TContext, TEvent>;
                        activities[activityAction.Activity.Id ?? activityAction.Activity.Type] = activityAction;
                    }
                    else if (action.Type == ActionTypes.Stop)
                    {
                        var activityAction = action as ActivityActionObject<TContext, TEvent>;
                        activities[activityAction.Activity.Id ?? activityAction.Activity.Type] = null;
                    }
                }
            }

            var (resolvedActions, updatedContext) = ResolveActions(
                this,
                currentState,
                context,
                _event,
                actionBlocks,
                predictableExec,
                this.Machine.Config.PredictableActionArguments ||
                this.Machine.Config.PreserveActionOrder
            );

            var (raisedEvents, nonRaisedActions) = resolvedActions
                .Partition(action => IsRaisableAction(action));

            var invokeActions = resolvedActions
                .Where(action =>
                    action.Type == ActionTypes.Start &&
                    (action as ActivityActionObject<TContext, TEvent>)?.Activity?.Type == ActionTypes.Invoke)
                .Cast<InvokeActionObject<TContext, TEvent>>()
                .ToList();

            var children = invokeActions.Aggregate(
                currentState != null
                    ? new Dictionary<string, ActorRef<object>>(currentState.Children)
                    : new Dictionary<string, ActorRef<object>>(),
                (acc, action) =>
                {
                    acc[action.Activity.Id] = CreateInvocableActor(
                        action.Activity,
                        this.Machine as dynamic,
                        updatedContext,
                        _event
                    );
                    return acc;
                });

            var nextState = new State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta>(
                value: resolvedStateValue ?? currentState.Value,
                context: updatedContext,
                _event: _event,
                _sessionid: currentState != null ? currentState._sessionid : null,
                historyValue: resolvedStateValue != null
                    ? historyValue != null
                        ? UpdateHistoryValue(historyValue, resolvedStateValue)
                        : null
                    : currentState?.HistoryValue,
                history: !resolvedStateValue || stateTransition.Source
                    ? currentState
                    : null,
                actions: resolvedStateValue != null ? nonRaisedActions : new List<object>(),
                activities: resolvedStateValue != null
                    ? activities
                    : currentState?.Activities ?? new Dictionary<string, ActivityDefinition<TContext, TEvent>>(),
                events: new List<TEvent>(),
                configuration: resolvedConfiguration,
                transitions: stateTransition.Transitions,
                children: children,
                done: isDone,
                tags: GetTagsFromConfiguration(resolvedConfiguration),
                machine: this
            );

            var didUpdateContext = !EqualityComparer<TContext>.Default.Equals(context, updatedContext);

            nextState.Changed = _event.Name == ActionTypes.Update || didUpdateContext;

            var history = nextState.History;
            if (history != null)
            {
                history.History = null;
            }

            var hasAlwaysTransitions = !isDone &&
                (this._transient || configuration.Any(stateNode => stateNode._transient));

            if (!willTransition &&
                (!hasAlwaysTransitions || _event.Name == NULL_EVENT))
            {
                return nextState;
            }

            var maybeNextState = nextState;

            if (!isDone)
            {
                if (hasAlwaysTransitions)
                {
                    maybeNextState = this.ResolveRaisedTransition(
                        maybeNextState,
                        new SCXML.Event<TEvent> { Name = ActionTypes.NullEvent },
                        _event,
                        predictableExec
                    );
                }

                while (raisedEvents.Count > 0)
                {
                    var raisedEvent = raisedEvents.Dequeue();
                    maybeNextState = this.ResolveRaisedTransition(
                        maybeNextState,
                        raisedEvent._event as dynamic,
                        _event,
                        predictableExec
                    );
                }
            }

            var changed = maybeNextState.Changed ||
                (history != null
                    ? maybeNextState.Actions.Count > 0 ||
                      didUpdateContext ||
                      history.Value?.GetType() != maybeNextState.Value?.GetType() ||
                      !StateValuesEqual(maybeNextState.Value, history.Value)
                    : false);

            maybeNextState.Changed = changed;
            maybeNextState.History = history;

            return maybeNextState;
        }
    }
}
