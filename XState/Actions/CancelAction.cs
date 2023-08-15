using static XState.SCXML;

namespace XState.Actions
{
    public interface ICancelAction<TContext, TExpressionEvent, TEvent>
        : IAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        string SendId { get; }
    }

    public class CancelAction<TContext, TExpressionEvent, TEvent> : Action<TContext, TExpressionEvent, TEvent>, ICancelAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        public CancelAction(string sendId) => SendId = sendId;

        public override ActionType Type => ActionTypes.Cancel.Value;

        public string SendId { get; }
    }
}
