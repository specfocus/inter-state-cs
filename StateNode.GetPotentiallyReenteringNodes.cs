namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> GetPotentiallyReenteringNodes(
            StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> targetNode
        )
        {
            if (this.Order < targetNode.Order)
            {
                return new List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> { this };
            }

            List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> nodes =
                new List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>>();
            StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> marker = this;
            StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> possibleAncestor = targetNode;

            while (marker != null && marker != possibleAncestor)
            {
                nodes.Add(marker);
                marker = marker.Parent;
            }

            if (marker != possibleAncestor)
            {
                // We never reached `possibleAncestor`, so the initial `marker` is outside it
                // It's in a different part of the tree, so no states will be reentered for such an external transition
                return new List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>>();
            }

            nodes.Add(possibleAncestor);
            return nodes;
        }
    }
}
