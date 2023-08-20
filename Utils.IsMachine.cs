namespace XState
{
    internal static partial class Utils
    {
        public static bool IsMachine(object value)
        {
            return value is AnyStateMachine stateMachine && stateMachine.__xstatenode != null;
        }
    }
}
