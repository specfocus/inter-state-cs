namespace XState
{
    internal static partial class Utils
    {
        public static List<List<string>> ToStatePaths(StateValue stateValue)
        {
            if (stateValue == null)
            {
                return new List<List<string>> { new List<string>() };
            }

            if (stateValue is string stringStateValue)
            {
                return new List<List<string>> { new List<string> { stringStateValue } };
            }

            var result = Utils.Flatten(
                stateValue.SelectMany(kv =>
                {
                    var key = kv.Key;
                    var subStateValue = kv.Value;

                    if (subStateValue == null ||
                        (subStateValue is StateValue subStateDict && !subStateDict.Any()))
                    {
                        return new List<List<string>> { new List<string> { key } };
                    }

                    return ToStatePaths(subStateValue).Select(subPath => new List<string> { key }.Concat(subPath).ToList());
                })
            ).ToList();

            return result;
        }
    }
}
