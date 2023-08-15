namespace XState
{
    static partial class StateNodeExtensions
    {
        public static StateValue GetValue<TContext, TEvent>(
            this StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag> rootNode,
            Configuration<TContext, TEvent> configuration
        )
            where TContext : class
            where TEvent : Event
        {
            var config = GetConfiguration(new[] { rootNode }, configuration);
            var adjList = GetAdjList<TContext, TEvent>(config);

            return GetValueFromAdj<TContext, TEvent>(rootNode, adjList);
        }
    }
}
