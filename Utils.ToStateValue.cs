namespace XState
{
    internal static partial class Utils
    {
        public static StateValue ToStateValue(object stateValue, string delimiter)
        {
            if (stateValue is StateLike<object> stateLike)
            {
                return stateLike.Value;
            }

            if (stateValue is string[] arrayStateValue)
            {
                return PathToStateValue(arrayStateValue.ToList());
            }

            if (stateValue is StateValue directStateValue)
            {
                return directStateValue;
            }

            if (stateValue is string stringStateValue)
            {
                List<string> statePath = ToStatePath(stringStateValue, delimiter);
                return PathToStateValue(statePath);
            }

            throw new ArgumentException("Unsupported state value type");
        }
    }
}
