using XState.Actions;
using XState.State;

namespace XState
{
    public static class StateMachineExtensions
    {
        private static bool warned = false;

        public static StateMachine<TContext, TStateSchema, TEvent> Machine<TContext, TStateSchema, TEvent>(
            ContextProvider<TContext> initialContext,
            MachineConfig<TContext, TStateSchema, TEvent> config,
            InternalMachineOptions<TContext, TEvent, ResolveTypegenMeta<TypegenDisabled, TEvent, BaseActionObject, ServiceMap>> options
        )
        {
            return new StateMachine<TContext, TStateSchema, TEvent>(initialContext, config, options);
        }

        public static StateMachine<TContext, TStateSchema, TEvent, TTypestate, BaseActionObject, TServiceMap, ResolveTypegenMeta<TTypesMeta, TEvent, BaseActionObject, TServiceMap>> CreateMachine<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TTypesMeta>(
            MachineConfig<TContext, TStateSchema, TEvent, BaseActionObject, TServiceMap, TTypesMeta> config,
            InternalMachineOptions<TContext, TEvent, ResolveTypegenMeta<TTypesMeta, TEvent, BaseActionObject, TServiceMap>> options
        )
        {
            if (!IS_PRODUCTION && !config.PredictableActionArguments && !warned)
            {
                warned = true;
                Console.WriteLine("It is highly recommended to set `predictableActionArguments` to `true` when using `createMachine`. https://xstate.js.org/docs/guides/actions.html");
            }

            return new StateNode(config, options);
        }

        public static StateMachine<TContext, TStateSchema, TEvent, TTypestate, BaseActionObject, TServiceMap, TTypesMeta> CreateMachine<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TTypesMeta>(
            MachineConfig<TContext, TStateSchema, TEvent, BaseActionObject, TServiceMap, TTypesMeta> config,
            MachineOptions<TContext, TEvent, BaseActionObject, TServiceMap, TTypesMeta> options
        )
        {
            if (!IS_PRODUCTION && !config.PredictableActionArguments && !warned)
            {
                warned = true;
                Console.WriteLine("It is highly recommended to set `predictableActionArguments` to `true` when using `createMachine`. https://xstate.js.org/docs/guides/actions.html");
            }

            return new StateNode(config, options);
        }
    }
}
