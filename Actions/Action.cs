using XState.Dynamic;

namespace XState.Actions
{
    public interface IAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        /// <summary>
        /// The type of the action.
        /// </summary>
        ActionType Type { get; }
    }

    public abstract class Action<TContext, TExpressionEvent, TEvent> : Record
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        public static implicit operator string(Action<TContext, TExpressionEvent, TEvent> action) => action.Type;

        public abstract ActionType Type { get; }
    }

    public class Actions<TContext, TExpressionEvent, TEvent> : List<Action<TContext, TExpressionEvent, TEvent>>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
    }

    public class BaseActionObject : Action<object, Event, Event>
    {
        public static implicit operator BaseActionObject(ActionType type) => new(type);

        public BaseActionObject(ActionType type) => Type = type;

        public override ActionType Type { get; }
    }
}
