namespace XState
{
    using static XState.SCXML;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Determines the next state given the current `state` and sent `event`.
        /// </summary>
        /// <param name="state">The current State instance or state value</param>
        /// <param name="event">The event that was sent at the current state</param>
        /// <param name="context">The current context(extended state) of the current state</param>
        /// <param name="exec"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> Transition(
            StateValue state = null,
            Event<TEvent> @event = null,
            TContext context = default,
            PredictableActionArgumentsExec exec = null
        )
        {
            var _event = Utils.ToSCXMLEvent(@event);

            State<TContext, TEvent, object, TTypestate, TResolvedTypesMeta> currentState;

            if (state is State<TContext, TEvent, object, TTypestate, TResolvedTypesMeta> stateObject)
            {
                currentState = context == null
                    ? stateObject
                    : ResolveState(State.From(stateObject, context));
            }
            else
            {
                var resolvedStateValue = state is string
                    ? Resolve(PathToStateValue(GetResolvedPath((string)state)))
                    : Resolve(state);
                var resolvedContext = context ?? Machine.Context;

                currentState = ResolveState(State.From(resolvedStateValue, resolvedContext));
            }

            if (!Environment.IsProduction && _event.Name == Constants.WILDCARD)
            {
                throw new Exception($"An event cannot have the wildcard type ('{Constants.WILDCARD}')");
            }

            if (Strict)
            {
                if (!Events.Contains(_event.Name) && !IsBuiltInEvent(_event.Name))
                {
                    throw new Exception($"Machine '{Id}' does not accept event '{_event.Name}'");
                }
            }

            var stateTransition = _Transition(
                currentState.Value,
                currentState,
                _event
            ) ?? new StateTransition<TContext, TEvent>
            {
                Transitions = new List<TransitionDefinition<TContext, TEvent>>(),
                Configuration = new List<StateNode<TContext, object, TEvent, object, object, object>>(),
                ExitSet = new List<StateNode<TContext, object, TEvent, object, object, object>>(),
                Source = currentState,
                Actions = new List<ActionObject<TContext, TEvent>>()
            };

            var prevConfig = GetConfiguration(
                new List<StateNode<TContext, object, TEvent, object, object, object>>(),
                GetStateNodes(currentState.Value)
            );
            var resolvedConfig = stateTransition.Configuration.Count > 0
                ? GetConfiguration(prevConfig, stateTransition.Configuration)
                : prevConfig;

            stateTransition.Configuration.Clear();
            stateTransition.Configuration.AddRange(resolvedConfig);

            return ResolveTransition(
                stateTransition,
                currentState,
                currentState.Context,
                exec,
                _event
            );
        }
    }
}
