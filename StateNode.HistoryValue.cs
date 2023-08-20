namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private HistoryValue HistoryValue(StateValue relativeStateValue = null)
        {
            if (States.Count == 0)
            {
                return null;
            }

            return new HistoryValue
            {
                Current = relativeStateValue ?? InitialStateValue,
                States = MapFilterValues<StateNode<TContext, dynamic, TEvent>, HistoryValue>(
                    States,
                    (stateNode, key) =>
                    {
                        if (relativeStateValue == null)
                        {
                            return stateNode.HistoryValue();
                        }

                        var subStateValue = relativeStateValue is string
                            ? null
                            : ((StateValue)relativeStateValue)[key];

                        return stateNode.HistoryValue(
                            subStateValue ?? stateNode.InitialStateValue
                        );
                    },
                    stateNode => !stateNode.History
                )
            };
        }
    }
}
