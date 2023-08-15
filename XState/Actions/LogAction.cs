namespace XState.Actions
{
    public interface ILogAction<TContext, TExpressionEvent, TEvent>
        : IAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
    }

    public class LogAction<TContext, TExpressionEvent, TEvent> : Action<TContext, TExpressionEvent, TEvent>, ILogAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        public override ActionType Type => ActionTypes.Log.Value;
    }
}
