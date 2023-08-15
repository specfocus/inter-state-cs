namespace XState
{
    static partial class StateNodeExtensions
    {
        public static IEnumerable<TEvent> NextEvents<TContext, TEvent>(
            IEnumerable<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, TypegenFlag>> configuration
        )
            where TContext : class
            where TEvent : Event
        {
            return configuration.SelectMany(sn => sn.OwnEvents.Select(e => e.Type)).Distinct();
        }
    }
}
