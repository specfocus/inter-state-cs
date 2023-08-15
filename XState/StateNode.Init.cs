namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private void _Init()
        {
            if (__cache.Transitions != null)
            {
                return;
            }

            foreach (var stateNode in GetAllStateNodes(this))
            {
                var transitions = stateNode.On;
            }
        }
    }
}
