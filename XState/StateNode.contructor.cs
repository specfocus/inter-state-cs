namespace XState
{
    using XState.Actions;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        public StateNode(
            StateNodeConfig<TContext, TStateSchema, TEvent, BaseActionObject> config,
            MachineOptions<TContext, TEvent>? options = null,
            ContextProvider<TContext>? context = null,
            StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>? parent = null,
            string? key = null
        )
        {
            Config = config;
            Options = options ?? CreateDefaultOptions<TContext>();
            _context = context ?? ('context' in config ? (TContext)(config as dynamic).context : default(TContext));
            Parent = parent;
            Key = key ?? config.Key ?? config.Id ?? "(machine)";

            if (!Environment.IS_PRODUCTION)
            {
                Utils.Warn(!config.ContainsKey("parallel"), $"The \"parallel\" property is deprecated and will be removed in version 4.1. {(config.ContainsKey("parallel") ? "Replace with `type: 'parallel'`" : "Use `type: '{this.type}'`")} in the config for state node '{this.id}' instead.");
            }

            States = Config.States?.ToDictionary(
                kvp => kvp.Key,
                kvp => new StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>(
                    kvp.Value,
                    null,
                    null,
                    this,
                    kvp.Key
                )
            ) ?? new Dictionary<string, StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>>();

            // Document order
            int order = 0;

            void DFS(AnyStateNode stateNode)
            {
                stateNode.Order = order++;

                foreach (var child in stateNode.GetAllChildren())
                {
                    DFS(child);
                }
            }

            DFS(this);

            this._transient = Config.Always != null || (Config.On != null && (Config.On is List<TransitionConfig<TContext, TEvent>> onList && onList.Any(on => on.Event == Constants.NULL_EVENT)) || (Config.On is Dictionary<string, TransitionConfig<TContext, TEvent>> onDict && onDict.ContainsKey(Constants.NULL_EVENT)));
            
            // TODO: this is the real fix for initialization once
            // state node getters are deprecated
            // if (!Parent) {
            //   this._Init();
            // }
        }
    }
}
