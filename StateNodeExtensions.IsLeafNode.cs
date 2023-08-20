namespace XState
{
    public static partial class StateNodeExtensions
    {
        public static bool IsLeafNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>(
            this StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> stateNode
        )
            where TContext : class
            where TStateSchema : IStateSchema<TContext>
            where TEvent : Event
            where TTypestate : Typestate<TContext>
            where TServiceMap : ServiceMap
            where TResolvedTypesMeta : TypegenFlag // TypegenDisabled
        {
            return stateNode.Type == "atomic" || stateNode.Type == "final";
        }
    }
}
