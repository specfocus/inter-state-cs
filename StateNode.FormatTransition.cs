namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private TransitionDefinition<TContext, TEvent> FormatTransition(TransitionConfig<TContext, TEvent> transitionConfig)
        {
            var normalizedTarget = NormalizeTarget(transitionConfig.Target);
            var internalTransition = transitionConfig.Internal ?? (normalizedTarget?.Any(_target => _target is string _strTarget && _strTarget[0] == this.Delimiter) ?? true);
            var guards = this.Machine.Options.Guards;

            var target = ResolveTarget(normalizedTarget);

            var transition = new TransitionDefinition<TContext, TEvent>
            {
                Event = transitionConfig.Event,
                Actions = ToActionObjects(ToArray(transitionConfig.Actions)),
                Condition = ToGuard(transitionConfig.Condition, guards), // Assuming 'guards' is a dictionary
                Target = target,
                Source = this,
                Internal = internalTransition,
                EventType = transitionConfig.Event,
                ToJSON = () => new
                {
                    Event = transitionConfig.Event,
                    Actions = transitionConfig.Actions?.Select(action => action.ToString()),
                    Condition = transitionConfig.Condition?.ToString(),
                    Target = target?.Select(t => $"#{t.Id}"),
                    Source = $"#{this.Id}"
                }
            };

            return transition;
        }
    }
}
