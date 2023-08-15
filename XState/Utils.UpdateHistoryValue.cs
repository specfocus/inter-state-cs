namespace XState
{
    internal static partial class Utils
    {
        public static HistoryValue UpdateHistoryValue(HistoryValue hist, StateValue stateValue)
        {
            return new HistoryValue
            {
                Current = stateValue,
                States = UpdateHistoryStates(hist.States, stateValue)
            };
        }
    }
}
