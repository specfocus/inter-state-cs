namespace XState
{
    static partial class StateNodeExtensions
    {
        private static StateValue<TContext, TEvent> GetValueFromAdj<TContext, TEvent>(
            StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag> baseNode,
            Dictionary<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>, List<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>>> adjList
        )
            where TContext : class
            where TEvent : Event
        {
            var childStateNodes = adjList.ContainsKey(baseNode) ? adjList[baseNode] : null;

            if (childStateNodes == null)
            {
                return new StateValue<TContext, TEvent>(); // todo: fix?
            }

            if (baseNode.Type == "compound")
            {
                var childStateNode = childStateNodes.Count > 0 ? childStateNodes[0] : null;
                if (childStateNode != null)
                {
                    if (IsLeafNode(childStateNode))
                    {
                        return new StateValue<TContext, TEvent> { StateKey = childStateNode.Key };
                    }
                }
                else
                {
                    return new StateValue<TContext, TEvent>();
                }
            }

            var stateValue = new StateValue<TContext, TEvent>();
            foreach (var csn in childStateNodes)
            {
                stateValue.StateValues[csn.Key] = GetValueFromAdj(csn, adjList);
            }

            return stateValue;
        }
    }
}
