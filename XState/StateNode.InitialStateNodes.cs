namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        public List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> InitialStateNodes
        {
            get
            {
                if (IsLeafNode(this))
                {
                    return new List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> { this };
                }

                // Case when state node is compound but no initial state is defined
                if (Type == "compound" && Initial == null)
                {
                    if (!Environment.IS_PRODUCTION)
                    {
                        Utils.Warn(false, $"Compound state node '{Id}' has no initial state.");
                    }
                    return new List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> { this };
                }

                var initialStateNodePaths = ToStatePaths(InitialStateValue);
                return initialStateNodePaths.SelectMany(initialPath =>
                    GetFromRelativePath(initialPath)
                ).ToList();
            }
        }
    }
}
