namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private StateValue InitialStateValue
        {
            get
            {
                if (this.__cache.InitialStateValue != null)
                {
                    return this.__cache.InitialStateValue;
                }

                StateValue initialStateValue = null;

                if (Type == "parallel")
                {
                    initialStateValue = MapFilterValues(
                        States as Dictionary<string, StateNode<TContext, dynamic, TEvent>>,
                        state => state.InitialStateValue ?? new StateValue(),
                        stateNode => !(stateNode.Type == "history")
                    );
                }
                else if (Initial != null)
                {
                    if (!States.ContainsKey(Initial as string))
                    {
                        throw new Exception($"Initial state '{Initial as string}' not found on '{Key}'");
                    }

                    initialStateValue = (
                        IsLeafNode(States[Initial as string])
                        ? Initial
                        : new StateValue { { Initial, States[Initial as string].InitialStateValue } }
                    );
                }
                else
                {
                    // The finite state value of a machine without child states is just an empty object
                    initialStateValue = new StateValue();
                }

                this.__cache.InitialStateValue = initialStateValue;

                return this.__cache.InitialStateValue;
            }
        }

    }
}
