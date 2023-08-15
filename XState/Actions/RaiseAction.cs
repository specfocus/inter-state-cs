namespace XState.Actions
{
    public interface IRaiseAction<TContext, TExpressionEvent, TEvent>
        : IAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        TEvent Event { get; set; }

        SendExpr<TContext, TExpressionEvent, TEvent> EventExpression { get; set; }

        Delay<TContext, TEvent>? Delay { get; set; }

        string? Id { get; set; }
    }

    public class RaiseAction<TContext, TExpressionEvent, TEvent> : Action<TContext, TExpressionEvent, TEvent>, IRaiseAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        public override ActionType Type => ActionTypes.Raise.Value;

        public TEvent Event { get; set; }

        public SendExpr<TContext, TExpressionEvent, TEvent> EventExpression { get; set; }

        public Delay<TContext, TEvent>? Delay { get; set; }

        public string? Id { get; set; }
    }

    public class RaiseActionObject<TContext, TExpressionEvent, TEvent> : RaiseAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        internal SCXML.Event<TEvent> _event;
    }
}
