namespace XState
{
    internal static partial class Utils
    {
        public static List<string> ToStatePath(object stateId, string delimiter)
        {
            try
            {
                if (stateId is List<string> listStateId)
                {
                    return listStateId;
                }

                string stringStateId = stateId.ToString();
                return new List<string>(stringStateId.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception e)
            {
                throw new Exception($"'{stateId}' is not a valid state path.", e);
            }
        }
    }
}
