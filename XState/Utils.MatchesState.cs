namespace XState
{
    internal static partial class Utils
    {
        public static bool MatchesState(StateValue parentStateId, StateValue childStateId, string delimiter = Constants.STATE_DELIMITER)
        {
            StateValue parentStateValue = ToStateValue(parentStateId, delimiter);
            StateValue childStateValue = ToStateValue(childStateId, delimiter);

            if (childStateValue is string childStringState)
            {
                if (parentStateValue is string parentStringState)
                {
                    return childStringState == parentStringState;
                }

                // Parent more specific than child
                return false;
            }

            if (parentStateValue is string parentStringState2)
            {
                return childStateValue.ContainsKey(parentStringState2);
            }

            return ((IDictionary<string, StateValue>)parentStateValue).Keys.All(key =>
            {
                if (!childStateValue.ContainsKey(key))
                {
                    return false;
                }

                return MatchesState(parentStateValue[key], childStateValue[key]);
            });
        }
    }
}
