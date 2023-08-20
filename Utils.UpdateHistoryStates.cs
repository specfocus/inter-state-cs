namespace XState
{
    internal static partial class Utils
    {
        public static Dictionary<string, IHistoryValue?> UpdateHistoryStates(
            Dictionary<string, IHistoryValue?> hist,
            StateValue stateValue
        )
        {
            return hist.ToDictionary(
                kvp => kvp.Key,
                kvp =>
                {
                    if (kvp.Value == null)
                    {
                        return null;
                    }

                    var subHist = kvp.Value;
                    var key = kvp.Key;
                    var subStateValue = (stateValue is string
                        ? null
                        : ((StateValueMap)stateValue).ContainsKey(key)
                            ? ((StateValueMap)stateValue)[key]
                            : subHist?.Current) ?? null;

                    if (subStateValue == null)
                    {
                        return null;
                    }

                    return new HistoryValue
                    {
                        Current = subStateValue,
                        States = UpdateHistoryStates(subHist, subStateValue)
                    };
                });
        }
    }
}
