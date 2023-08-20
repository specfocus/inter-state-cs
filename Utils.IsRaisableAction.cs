namespace XState
{
    using Actions;
    using XState;
    using XState.Actions;

    internal static partial class Utils
    {
        public static bool IsRaisableAction<TContext, TExpressionEvent, TEvent>(
            ActionObject<TContext, TExpressionEvent, TEvent, BaseActionObject> action
        )
            where TContext : class
            where TExpressionEvent : Event
            where TEvent : Event
        {
            if (action.Type == ActionTypes.Raise.Value && action is RaiseActionObject<TContext, TExpressionEvent, TEvent> raise) {
                return raise.Delay == null;
            }
            if (action.Type == ActionTypes.Send.Value && action is SendActionObject<TContext, TExpressionEvent, TEvent> send) {
                return send.To == "Internal" && send.Delay == null;
            }
            return false;
        }
    }
}
