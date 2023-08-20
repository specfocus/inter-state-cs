namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Retrieves state nodes from a relative path to this state node.
        /// </summary>
        /// <param name="relativePath">The relative path from this state node</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<StateNode<TContext, dynamic, TEvent, dynamic, dynamic, dynamic>> GetFromRelativePath(List<string> relativePath)
        {
            if (relativePath.Count == 0)
            {
                return new List<StateNode<TContext, dynamic, TEvent, dynamic, dynamic, dynamic>> { this };
            }

            var stateKey = relativePath.First();
            var childStatePath = relativePath.Skip(1).ToList();

            if (States == null)
            {
                throw new Exception($"Cannot retrieve subPath '{stateKey}' from node with no states");
            }

            var childStateNode = GetStateNode(stateKey);

            if (childStateNode.Type == "history")
            {
                return childStateNode.ResolveHistory();
            }

            if (!States.ContainsKey(stateKey))
            {
                throw new Exception($"Child state '{stateKey}' does not exist on '{Id}'");
            }

            return States[stateKey].GetFromRelativePath(childStatePath);
        }
    }
}
