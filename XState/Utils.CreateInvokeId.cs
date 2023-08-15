namespace XState
{
    internal static partial class Utils
    {
        public static string CreateInvokeId(string stateNodeId, int index)
        {
            return $"{stateNodeId}:invocation[{index}]";
        }
    }
}
