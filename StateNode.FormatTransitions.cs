namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private List<TransitionDefinition<TContext, TEvent>> formatTransitions()
        {
            List<TransitionConfig<TContext, EventObject>> onConfig;

            if (this.config.on == null)
            {
                onConfig = new List<TransitionConfig<TContext, EventObject>>();
            }
            else if (this.config.on is List<TransitionConfig<TContext, EventObject>> onList)
            {
                onConfig = onList;
            }
            else
            {
                var wildcardConfigs = new List<TransitionConfig<TContext, EventObject>>();
                var strictTransitionConfigs = this.config.on
                    .Where(pair => pair.Key != WILDCARD)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);

                onConfig = strictTransitionConfigs.Keys
                    .Select(key =>
                    {
                        if (!IS_PRODUCTION && key == NULL_EVENT)
                        {
                            Console.WriteLine($"Empty string transition configs (e.g., {{ on: {{ '': ... }} }}) for transient transitions are deprecated. Specify the transition in the {{ always: ... }} property instead. Please check the 'on' configuration for \"#{this.id}\".");
                        }

                        var transitionConfigArray = ToTransitionConfigArray(key, strictTransitionConfigs[key]);
                        if (!IS_PRODUCTION)
                        {
                            ValidateArrayifiedTransitions(this, key, transitionConfigArray);
                        }
                        return transitionConfigArray;
                    })
                    .Concat(ToTransitionConfigArray(WILDCARD, wildcardConfigs as SingleOrArray<TransitionConfig<TContext, EventObject>>))
                    .ToList();
            }

            var eventlessConfig = this.config.always != null
                ? ToTransitionConfigArray("", this.config.always)
                : new List<TransitionConfig<TContext, EventObject>>();

            var doneConfig = this.config.onDone != null
                ? ToTransitionConfigArray(done(this.id).ToString(), this.config.onDone)
                : new List<TransitionConfig<TContext, EventObject>>();

            if (!IS_PRODUCTION)
            {
                if (this.config.onDone != null && this.parent == null)
                {
                    Console.WriteLine($"Root nodes cannot have an \".onDone\" transition. Please check the config of \"{this.id}\".");
                }
            }

            var invokeConfig = this.invoke.Select(invokeDef =>
            {
                var settleTransitions = new List<TransitionConfig<TContext, EventObject>>();

                if (invokeDef.onDone != null)
                {
                    settleTransitions.AddRange(ToTransitionConfigArray(String(doneInvoke(invokeDef.id)), invokeDef.onDone));
                }

                if (invokeDef.onError != null)
                {
                    settleTransitions.AddRange(ToTransitionConfigArray(String(error(invokeDef.id)), invokeDef.onError));
                }

                return settleTransitions;
            }).SelectMany(x => x).ToList();

            var delayedTransitions = this.after;

            var formattedTransitions = new List<TransitionDefinition<TContext, TEvent>>();
            formattedTransitions.AddRange(doneConfig.SelectMany(transitionConfig => toArray(transitionConfig).Select(transition => this.formatTransition(transition))));
            formattedTransitions.AddRange(invokeConfig.SelectMany(transition => toArray(transition).Select(item => this.formatTransition(item))));
            formattedTransitions.AddRange(onConfig.SelectMany(transitionConfig => toArray(transitionConfig).Select(transition => this.formatTransition(transition))));
            formattedTransitions.AddRange(eventlessConfig.SelectMany(transitionConfig => toArray(transitionConfig).Select(transition => this.formatTransition(transition))));

            formattedTransitions.AddRange(delayedTransitions.Cast<TransitionDefinition<TContext, TEvent>>());

            return formattedTransitions;
        }
    }
}
