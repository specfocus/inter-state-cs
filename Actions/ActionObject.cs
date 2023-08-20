namespace XState.Actions
{
    /// <summary>
    /// Represents the full definition of an action, including its type and execution logic.
    /// </summary>
    public interface IActionObject<TContext, TExpressionEvent, TEvent, TAction> : IAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
        where TAction : BaseActionObject
    {

        /// <summary>
        /// The implementation for executing the action.
        /// </summary>
        ActionFunction<TContext, TExpressionEvent, TEvent, BaseActionObject>? Exec { get; }

        /// <summary>
        /// An internal signature that doesn"t exist at runtime, but helps with inference.
        /// </summary>
        void DeprecatedSignature(TContext arg, TExpressionEvent ev, ActionMeta<TContext, TEvent, TAction> meta);
    }

    /// <summary>
    /// Represents the full definition of an action, including its type and execution logic.
    /// </summary>
    public class ActionObject<TContext, TExpressionEvent, TEvent, TAction>
        : Action<TContext, TExpressionEvent, TEvent>, IActionObject<TContext, TExpressionEvent, TEvent, TAction>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
        where TAction : BaseActionObject
    {
        public ActionObject(ActionType type) => Type = type;

        /// <summary>
        /// The type of the action.
        /// </summary>
        public override ActionType Type { get; }

        /// <summary>
        /// The implementation for executing the action.
        /// </summary>
        public ActionFunction<TContext, TExpressionEvent, TEvent, BaseActionObject>? Exec { get; set; }

        /// <summary>
        /// An internal signature that doesn"t exist at runtime, but helps with inference.
        /// </summary>
        public void DeprecatedSignature(TContext arg, TExpressionEvent ev, ActionMeta<TContext, TEvent, TAction> meta)
        {
            throw new NotImplementedException();
        }
    }
}