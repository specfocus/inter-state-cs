namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Resolves a partial state value with its full representation in this machine.
        /// </summary>
        /// <param name="stateValue">The partial state value to resolve.</param>
        /// <returns></returns>
        public StateValue Resolve(StateValue stateValue)
        {
            if (stateValue == null)
            {
                return InitialStateValue ?? new StateValue(); // TODO: type-specific properties
            }

            switch (Type)
            {
                case "parallel":
                    return MapValues(
                        InitialStateValue as Dictionary<string, StateValue>,
                        (subStateValue, subStateKey) =>
                        {
                            return subStateValue != null
                                ? GetStateNode(subStateKey).Resolve(stateValue.ContainsKey(subStateKey) ? stateValue[subStateKey] : subStateValue)
                                : new StateValue();
                        }
                    );

                case "compound":
                    if (stateValue is string strValue)
                    {
                        var subStateNode = GetStateNode(strValue);

                        if (subStateNode.Type == "parallel" || subStateNode.Type == "compound")
                        {
                            return new StateValue { { strValue, subStateNode.InitialStateValue ?? new StateValue() } };
                        }

                        return strValue;
                    }

                    if (stateValue.Count == 0)
                    {
                        return InitialStateValue ?? new StateValue();
                    }

                    return MapValues(stateValue, (subStateValue, subStateKey) =>
                    {
                        return subStateValue != null
                            ? GetStateNode(subStateKey).Resolve(subStateValue)
                            : new StateValue();
                    });

                default:
                    return stateValue ?? new StateValue();
            }
        }

    }
}
