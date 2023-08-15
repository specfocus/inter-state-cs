namespace XState
{
    using XState.Dynamic;

    public class Guard<TContext, TEvent> : Record
        where TContext : class
        where TEvent : Event
    {
        public static implicit operator Guard<TContext, TEvent>(string type)
        {
            return new Guard<TContext, TEvent>(type);
        }

        public static implicit operator string(Guard<TContext, TEvent> guard)
        {
            return guard.Type;
        }

        public static implicit operator Guard<TContext, TEvent>(GuardPredicate<TContext, TEvent>? predicate)
        {
            return new Guard<TContext, TEvent>(predicate);
        }

        public static implicit operator GuardPredicate<TContext, TEvent>?(Guard<TContext, TEvent> guard)
        {
            return guard.Predicate;
        }

        public Guard(GuardPredicate<TContext, TEvent>? predicate) => Predicate = predicate;

        public Guard(DefaultGuardType type) => Type = type;

        public GuardPredicate<TContext, TEvent>? Predicate { get; }

        public DefaultGuardType Type { get; } = DefaultGuardType.xstate_guard;
    }
}
