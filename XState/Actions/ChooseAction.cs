using System;

namespace XState.Actions
{
    public interface ChooseCondition<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        Condition<TContext, TExpressionEvent>? Cond { get; set; }

        Actions<TContext, TExpressionEvent, TEvent> Actions { get; set; }
    }

    public interface IChooseAction<TContext, TExpressionEvent, TEvent>
        : IAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        List<ChooseCondition<TContext, TExpressionEvent, TEvent>> Conds { get; }
    }

    public class ChooseAction<TContext, TExpressionEvent, TEvent> : Action<TContext, TExpressionEvent, TEvent>, IChooseAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        public override ActionType Type => ActionTypes.Choose.Value;

        public List<ChooseCondition<TContext, TExpressionEvent, TEvent>> Conds { get; } = new();
    }
}
