using XState.Actions;

namespace XState
{
    public class BaseActions<TContext, TExpressionEvent, TEvent, TAction>
        : SingleOrArray<BaseAction<TContext, TExpressionEvent, TEvent, TAction>>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : EventObject
        where TAction : BaseActionObject
    { }
}
