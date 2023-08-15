namespace XState
{
    using XState.Actions;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /*
        constructor(
        public config: StateNodeConfig<TContext, TStateSchema, TEvent>,
      options?: MachineOptions<TContext, TEvent>,
      private _context:
        | Readonly<TContext>
        | (() => Readonly<TContext>) = ('context' in config
        ? (config as any).context
        : undefined) as any, // TODO: this is unsafe, but we're removing it in v5 anyway
      _stateInfo?: {
        parent: StateNode<any, any, any, any, any, any>;
        key: string;
      }
    ) {
      this.options = Object.assign(createDefaultOptions<TContext>(), options);
      this.parent = _stateInfo?.parent;
      this.key =
        this.config.key || _stateInfo?.key || this.config.id || '(machine)';
      this.machine = this.parent ? this.parent.machine : (this as any);
      this.path = this.parent ? this.parent.path.concat(this.key) : [];
      this.delimiter =
        this.config.delimiter ||
        (this.parent ? this.parent.delimiter : STATE_DELIMITER);
      this.id =
        this.config.id || [this.machine.key, ...this.path].join(this.delimiter);
      this.version = this.parent
        ? this.parent.version
        : (this.config as MachineConfig<TContext, TStateSchema, TEvent>).version;
      this.type =
        this.config.type ||
        (this.config.parallel
          ? 'parallel'
          : this.config.states && Object.keys(this.config.states).length
          ? 'compound'
          : this.config.history
          ? 'history'
          : 'atomic');
      this.schema = this.parent
        ? this.machine.schema
        : (this.config as MachineConfig<TContext, TStateSchema, TEvent>).schema ??
          ({ } as this['schema']);
      this.description = this.config.description;
  
      if (!IS_PRODUCTION) {
        warn(
          !('parallel' in this.config),
          `The "parallel" property is deprecated and will be removed in version 4.1. ${
            this.config.parallel
              ? `Replace with \`type: 'parallel'\``
              : `Use \`type: '${this.type}'\``
          } in the config for state node '${this.id}' instead.`
        ) ;
    }
  
      this.initial = this.config.initial;
  
      this.states = (
        this.config.states
          ? mapValues(
              this.config.states,
              (stateConfig: StateNodeConfig<TContext, any, TEvent>, key) => {
        const stateNode = new StateNode(stateConfig, { }, undefined, {
        parent: this,
                  key: key as string
                });
        Object.assign(this.idMap, {
            [stateNode.id]: stateNode,
                  ...stateNode.idMap
                });
        return stateNode;
    }
            )
          : EMPTY_OBJECT
      ) as StateNodesConfig<TContext, TStateSchema, TEvent>;

    // Document order
    let order = 0;

    function dfs(
        stateNode: StateNode<TContext, any, TEvent, any, any, any>
      ): void {
        stateNode.order = order++;

        for (const child of getAllChildren(stateNode)) {
            dfs(child);
        }
    }

    dfs(this);
  
      // History config
      this.history =
        this.config.history === true ? 'shallow' : this.config.history || false;
  
      this._transient =
        !!this.config.always ||
        (!this.config.on
          ? false
          : Array.isArray(this.config.on)
          ? this.config.on.some(({ event }: { event: string }) => {
        return event === NULL_EVENT;
    })
          : NULL_EVENT in this.config.on);
      this.strict = !!this.config.strict;
  
      // TODO: deprecate (entry)
      this.onEntry = toArray(this.config.entry || this.config.onEntry).map(
        (action) => toActionObject(action as any)
      );
      // TODO: deprecate (exit)
      this.onExit = toArray(this.config.exit || this.config.onExit).map(
        (action) => toActionObject(action as any)
      );
      this.meta = this.config.meta;
      this.doneData =
        this.type === 'final'
          ? (this.config as FinalStateNodeConfig<TContext, TEvent>).data
          : undefined;
      this.invoke = toArray(this.config.invoke).map((invokeConfig, i) => {
        if (isMachine(invokeConfig))
        {
            const invokeId = createInvokeId(this.id, i);
            this.machine.options.services = {
                [invokeId]: invokeConfig,
            ...this.machine.options.services
            };

            return toInvokeDefinition({
            src: invokeId,
            id: invokeId
            });
        }
        else if (isString(invokeConfig.src))
        {
            const invokeId = invokeConfig.id || createInvokeId(this.id, i);
            return toInvokeDefinition({
                ...invokeConfig,
            id: invokeId,
            src: invokeConfig.src as string
            });
        }
        else if (isMachine(invokeConfig.src) || isFunction(invokeConfig.src))
        {
            const invokeId = invokeConfig.id || createInvokeId(this.id, i);
            this.machine.options.services = {
                [invokeId]: invokeConfig.src as InvokeCreator<TContext, TEvent>,
            ...this.machine.options.services
            } as any;

            return toInvokeDefinition({
            id: invokeId,
            ...invokeConfig,
            src: invokeId
            });
        }
        else
        {
            const invokeSource = invokeConfig.src as InvokeSourceDefinition;

            return toInvokeDefinition({
            id: createInvokeId(this.id, i),
            ...invokeConfig,
            src: invokeSource
            });
        }
    });
      this.activities = toArray(this.config.activities)
        .concat(this.invoke)
        .map((activity) => toActivityDefinition(activity));
      this.transition = this.transition.bind(this);
      this.tags = toArray(this.config.tags);

    // TODO: this is the real fix for initialization once
    // state node getters are deprecated
    // if (!this.parent) {
    //   this._init();
    // }
    }
    */
        public StateNode(
            ContextProvider<TContext> context,
            // The raw config used to create the machine.
            StateNodeConfig<TContext, TStateSchema, TEvent, BaseActionObject> config,
            // The initial extended state
            MachineOptions<TContext, TEvent> options = null,
            StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> parent = null,
            string key = null
        )
        {
            Config = config;
            Parent = parent;
            _context = context;
        }

        public StateNodeConfig<TContext, TStateSchema, TEvent, BaseActionObject> Config { get; }

        // The parent state node.
        public StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> Parent { get; set; }
    }
}
