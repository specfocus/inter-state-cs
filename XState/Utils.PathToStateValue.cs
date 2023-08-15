namespace XState
{
    internal static partial class Utils
    {
        public static StateValue PathToStateValue(List<string> statePath)
        {
            if (statePath.Count == 1)
            {
                return statePath[0];
            }

            StateValueMap marker = new();

            for (int i = 0; i < statePath.Count - 1; i++)
            {
                if (i == statePath.Count - 2)
                {
                    marker[statePath[i]] = statePath[i + 1];
                }
                else
                {
                    marker = marker[statePath[i]] = new StateValueMap();
                }
            }

            return marker;
        }
    }
}
