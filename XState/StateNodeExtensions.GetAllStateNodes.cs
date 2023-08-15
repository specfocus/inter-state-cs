namespace XState
{
    public static partial class StateNodeExtensions
    {
        public static List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> GetAllStateNodes<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>(
            this StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> stateNode
        )
            where TContext : class
            where TStateSchema : IStateSchema<TContext>
            where TEvent : Event
            where TTypestate : Typestate<TContext>
            where TServiceMap : ServiceMap
            where TResolvedTypesMeta : TypegenFlag // TypegenDisabled
        {
            List<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> stateNodes = new() { stateNode };

            if (IsLeafNode(stateNode))
            {
                return stateNodes;
            }

            return stateNodes.Concat(
                GetChildren(stateNode).SelectMany(GetAllStateNodes)
            ).ToList();
        }
    }
}