using XState.Actions;

namespace XState
{
    public static class Machine
    {
        public static StateMachine<TContext, TEvent, TTypestate, TServiceMap, TTypesMeta> CreateMachine<TContext, TEvent, TTypestate, TServiceMap, TTypesMeta>(
        MachineConfig<TContext, object, TEvent, BaseActionObject, TServiceMap, TTypesMeta> config,
        InternalMachineOptions<TContext, TEvent, ResolveTypegenMeta<TTypesMeta, TEvent, BaseActionObject, TServiceMap>> options = null
    )
        {
            if (!Environment.IS_PRODUCTION && !config.PredictableActionArguments && !warned)
            {
                warned = true;
                Console.WriteLine("It is highly recommended to set "predictableActionArguments" to "true" when using "createMachine". https://xstate.js.org/docs/guides/actions.html");
            }

            return new StateNode<TContext, TEvent, TTypestate, TServiceMap, TTypesMeta>(config, options);
        }

        public static StateMachine<TContext, TEvent, TTypestate, TServiceMap, TTypesMeta> CreateMachine<TContext, TEvent, TTypestate, TServiceMap, TTypesMeta>(
            MachineConfig<TContext, object, TEvent, BaseActionObject, TServiceMap, TTypesMeta> config,
            MachineOptions<TContext, TEvent, BaseActionObject, TServiceMap, TTypesMeta> options = null
        )
        {
            if (!Environment.IS_PRODUCTION && !config.PredictableActionArguments && !warned)
            {
                warned = true;
                Console.WriteLine("""It is highly recommended to set "predictableActionArguments" to "true" when using "createMachine". https://xstate.js.org/docs/guides/actions.html""");
            }

            return new StateNode<TContext, TEvent, TTypestate, TServiceMap, TTypesMeta>(config, options);
        }

        private static bool warned = false;
    }
}