namespace XState
{
    public class Condition<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public static implicit operator Condition<TContext, TEvent>(string type)
        {
            return new Condition<TContext, TEvent>(type);
        }

        public static implicit operator string(Condition<TContext, TEvent> condition)
        {
            return condition.Type;
        }

        public static implicit operator Condition<TContext, TEvent>(ConditionPredicate<TContext, TEvent> predicate)
        {
            return new Condition<TContext, TEvent>(predicate);
        }

        public static implicit operator ConditionPredicate<TContext, TEvent>(Condition<TContext, TEvent> condition)
        {
            return condition.Predicate;
        }

        public Condition(ConditionPredicate<TContext, TEvent>? predicate) => Predicate = predicate;

        public Condition(DefaultGuardType type) => Type = type;

        /// <summary>
        /// The predicate function for the condition.
        /// </summary>
        public ConditionPredicate<TContext, TEvent> Predicate { get; set; }

        public DefaultGuardType Type { get; } = DefaultGuardType.xstate_guard;
    }
}
