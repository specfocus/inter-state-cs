namespace XState
{
    using XState.Actions;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Clones this state machine with custom options and context.
        /// </summary>
        /// <param name="options">Options(actions, guards, activities, services) to recursively merge with the existing options.</param>
        /// <param name="context">Custom context(will override predefined context)</param>
        /// <returns></returns>
        public StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> WithConfig(
            InternalMachineOptions<TContext, TEvent, TResolvedTypesMeta, true> options,
            TContext context = default
        )
        {
            var actions = this.Options.Actions;
            var activities = this.Options.Activities;
            var guards = this.Options.Guards;
            var services = this.Options.Services;
            var delays = this.Options.Delays;

            return new StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>(
                this.Config,
                new MachineOptions<TContext, TEvent, TTypestate, TResolvedTypesMeta>
                {
                    Actions = MergeDictionary(actions, options.Actions),
                    Activities = MergeDictionary(activities, (options as dynamic).Activities),
                    Guards = MergeDictionary(guards, options.Guards),
                    Services = MergeDictionary(services, options.Services),
                    Delays = MergeDictionary(delays, options.Delays)
                },
                context ?? this.Context
            );
        }

        private static Dictionary<string, TValue> MergeDictionary<TValue>(
            Dictionary<string, TValue> dict1,
            Dictionary<string, TValue> dict2)
        {
            var mergedDict = new Dictionary<string, TValue>(dict1);

            foreach (var kvp in dict2)
            {
                mergedDict[kvp.Key] = kvp.Value;
            }

            return mergedDict;
        }

    }
}
