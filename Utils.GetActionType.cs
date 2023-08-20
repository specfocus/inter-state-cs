namespace XState
{
    internal static partial class Utils
    {
        public static string GetActionType<T1, T2>(Action<T1, T2> action)
        {
            try
            {
                if (action is string || action is int)
                {
                    return action.ToString();
                }
                else if (action is Delegate delegateAction)
                {
                    return delegateAction.Method.Name;
                }
                else
                {
                    return action.Type;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Actions must be strings or objects with a string action.type property.", e);
            }
        }
    }
}
