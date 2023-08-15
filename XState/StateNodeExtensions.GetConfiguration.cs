using System.Collections.ObjectModel;

namespace XState
{
    static partial class StateNodeExtensions
    {
        private class Configuration<TContext, TEvent> : Collection<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>>
            where TContext : class
            where TEvent : Event
        {
            // Define your Configuration class here
        }

        private static AdjList<TContext, TEvent> GetConfiguration<TContext, TEvent>(
            IEnumerable<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>> prevStateNodes,
            IEnumerable<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>> stateNodes
        )
            where TContext : class
            where TEvent : Event
        {
            var prevConfiguration = new HashSet<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>>(prevStateNodes);
            var prevAdjList = GetAdjList<TContext, TEvent>(prevConfiguration);

            var configuration = new HashSet<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>>(stateNodes);

            // Add all ancestors
            foreach (var s in configuration.ToList())
            {
                var m = s.Parent;

                while (m != null && !configuration.Contains(m))
                {
                    configuration.Add(m);
                    m = m.Parent;
                }
            }

            var adjList = GetAdjList<TContext, TEvent>(configuration);

            // Add descendants
            foreach (var s in configuration.ToList())
            {
                if (s.Type == "compound" && (!adjList.ContainsKey(s) || adjList[s].Count == 0))
                {
                    if (prevAdjList.ContainsKey(s))
                    {
                        foreach (var sn in prevAdjList[s])
                        {
                            configuration.Add(sn);
                        }
                    }
                    else
                    {
                        foreach (var sn in s.InitialStateNodes)
                        {
                            configuration.Add(sn);
                        }
                    }
                }
                else if (s.Type == "parallel")
                {
                    foreach (var child in GetChildren(s))
                    {
                        if (!configuration.Contains(child))
                        {
                            configuration.Add(child);

                            if (prevAdjList.ContainsKey(child))
                            {
                                foreach (var sn in prevAdjList[child])
                                {
                                    configuration.Add(sn);
                                }
                            }
                            else
                            {
                                foreach (var sn in child.InitialStateNodes)
                                {
                                    configuration.Add(sn);
                                }
                            }
                        }
                    }
                }
            }

            // Add all ancestors
            foreach (var s in configuration.ToList())
            {
                var m = s.Parent;

                while (m != null && !configuration.Contains(m))
                {
                    configuration.Add(m);
                    m = m.Parent;
                }
            }

            return configuration;
        }
    }
}