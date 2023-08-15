namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Returns the state nodes represented by the current state value.
        /// </summary>
        /// <param name="state">The state value or State instance</param>
        /// <returns></returns>
        public List<StateNode<TContext, IStateSchema<TContext>, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> GetStateNodes(
            StateValue state
        )
        {
            if (state == null)
            {
                return new List<StateNode<TContext, IStateSchema<TContext>, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>>();
            }

            StateValue stateValue;
            if (state is State<TContext, TEvent, IStateSchema<TContext>, TTypestate, TResolvedTypesMeta> stateInstance)
            {
                stateValue = stateInstance.Value;
            }
            else
            {
                stateValue = ToStateValue(state, Delimiter);
            }

            if (stateValue is string stateStringValue)
            {
                var initialStateValue = GetStateNode(stateStringValue).Initial;

                if (initialStateValue != null)
                {
                    return GetStateNodes(new StateValue { { stateStringValue, initialStateValue } });
                }
                else
                {
                    return new List<StateNode<TContext, object, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>>
            {
                this,
                States[stateStringValue]
            };
                }
            }

            var subStateKeys = stateValue.Keys.ToList();
            var subStateNodes = new List<StateNode<TContext, object, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>>
            {
                this
            };

            subStateNodes.AddRange(subStateKeys.SelectMany(subStateKey =>
                GetStateNode(subStateKey).GetStateNodes(stateValue[subStateKey])));

            return subStateNodes;
        }
    }
}
