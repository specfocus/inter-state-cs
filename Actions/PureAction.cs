namespace XState.Actions
{
    public interface IPureAction<TContext, TExpressionEvent, TEvent>
        : IAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
    }

    public class PureAction<TContext, TExpressionEvent, TEvent> : Action<TContext, TExpressionEvent, TEvent>, IPureAction<TContext, TExpressionEvent, TEvent>
       where TContext : class
       where TExpressionEvent : Event
       where TEvent : Event
    {
        public override ActionType Type => ActionTypes.Pure.Value;
    }
}
