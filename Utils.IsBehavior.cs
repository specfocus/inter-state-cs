namespace XState
{
    internal static partial class Utils
    {
        public static bool IsBehavior(object value)
        {
            if (value == null || !(value is Behavior))
            {
                return false;
            }

            var behavior = (Behavior)value;
            return behavior.Transition != null && behavior.Transition is Delegate;
        }
    }
}
