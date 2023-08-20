namespace XState
{
    /// <summary>
    /// Represents a guard predicate.
    /// </summary>
    public class GuardPredicate<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        /// <summary>
        /// The type of the guard predicate.
        /// </summary>
        public DefaultGuardType Type { get; set; }

        /// <summary>
        /// The name associated with the guard predicate.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The predicate function for the guard.
        /// </summary>
        public ConditionPredicate<TContext, TEvent> Predicate { get; set; }
    }
}
