namespace XState
{
    internal static partial class Utils
    {
        public static Guard<TContext, TEvent>? ToGuard<TContext, TEvent>(
            Condition<TContext, TEvent> condition,
            Dictionary<string, ConditionPredicate<TContext, TEvent>> guardMap
        )
            where TContext : class
            where TEvent : Event
        {
            if (condition == null)
            {
                return null;
            }

            if (condition is string conditionString)
            {
                dynamic guard = new Guard<TContext, TEvent>(Constants.DEFAULT_GUARD_TYPE);
                guard.Name = conditionString;
                guard.Predicate = guardMap.ContainsKey(conditionString) ? guardMap[conditionString] : null;
                return guard;
            }

            if (condition is Delegate conditionDelegate)
            {
                dynamic guard = new Guard<TContext, TEvent>(Constants.DEFAULT_GUARD_TYPE);
                guard.Name = conditionDelegate.Method.Name;
                guard.Predicate = conditionDelegate;
                return guard;
            }

            return condition;
        }
    }
}
