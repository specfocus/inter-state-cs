using XState.Actions;
using XState.State;

namespace XState
{
    public static class StateMachineExtensions
    {
        public static StateMachine<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TTypesMeta>
            CreateMachine<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TTypesMeta>(
            MachineConfig<TContext, TStateSchema, TEvent, BaseActionObject, TServiceMap, TTypesMeta> config,
            object options // Update with correct type for options
        )
        {
            if (!Environment.IS_PRODUCTION && !config.PredictableActionArguments && !Warned)
            {
                Warned = true;
                Console.WriteLine(
                    """It is highly recommended to set "predictableActionArguments" to "true" when using "createMachine". "https://xstate.js.org/docs/guides/actions.html" """
                );
            }

            return new StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TTypesMeta>(
                config,
                options,
                default // Update with appropriate initial context
            );
        }

        private static bool Warned = false;
    }
}
