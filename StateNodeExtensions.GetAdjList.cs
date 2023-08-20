namespace XState
{
    static partial class StateNodeExtensions
    {
        private class AdjList<TContext, TEvent> : Dictionary<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>, List<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>>>
            where TContext : class
            where TEvent : Event
        {
            public AdjList() : base() { }
        }

        private static AdjList<TContext, TEvent> GetAdjList<TContext, TEvent>(
            Configuration<TContext, TEvent> configuration
        )
            where TContext : class
            where TEvent : Event
        {
            var adjList = new AdjList<TContext, TEvent>();

            foreach (var s in configuration)
            {
                if (!adjList.ContainsKey(s))
                {
                    adjList[s] = new List<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>>();
                }

                if (s.Parent != null)
                {
                    if (!adjList.ContainsKey(s.Parent))
                    {
                        adjList[s.Parent] = new List<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>>();
                    }

                    adjList[s.Parent].Add(s);
                }
            }

            return adjList;
        }
    }
}
